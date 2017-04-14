using AlarmAndPlan.Model;
using HttpClientEx;
using Infrastructure.Model;
using Infrastructure.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AlarmAndPlan.Services
{
    /// <summary>
    /// 报警上传任务器,单例模式
    /// </summary>
    public class ForwardAlarmLogTask
    {
        ILogger<ForwardAlarmLogTask> _logger = Logger.CreateLogger<ForwardAlarmLogTask>();

        /// <summary>
        /// 推送报警列表
        /// </summary>
        List<AlarmLog> m_alarmLogList = new List<AlarmLog>();

        /// <summary>
        /// 推送报警列表锁对象
        /// </summary>
        private object m_logLockObj = new object();

        AutoResetEvent m_forwardWaiter = new AutoResetEvent(false);

        /// <summary>
        /// 新增要上传到上级系统的报警消息
        /// </summary>
        /// <param name="log"></param>
        public void ForwardAlarmLog(AlarmLog log)
        {
            lock (m_logLockObj)
            {
                m_alarmLogList.Add(log);
                m_forwardWaiter.Set();
            }
        }

        private static ForwardAlarmLogTask s_instance;

        public static ForwardAlarmLogTask Instance
        {
            get
            {
                if (s_instance == null)
                    s_instance = new ForwardAlarmLogTask();
                return s_instance;
            }
        }

        private ForwardAlarmLogTask()
        {
            //获取未上传的预案
            using (var db = new AllInOneContext.AllInOneContext())
            {
                lock (m_logLockObj)
                {
                    var alarmStatusId = Guid.Parse("A0002016-E009-B019-E001-ABCD13100002");
                    var logs = db.AlarmLog.Include(t => t.AlarmType).Include(t => t.AlarmSource).
                                  Where(t => t.UploadStatus == 0 && alarmStatusId.Equals(t.AlarmStatusId) &&
                                        (t.TimeCreated - DateTime.Now).Seconds < 60 * 60).ToList();
                    if (logs != null && logs.Count > 0)
                        m_alarmLogList.AddRange(logs);
                }
            }
        }

        public void Run()
        {
            Task.Factory.StartNew(() =>
            {
                _logger.LogInformation("Start forward alarmlog task......");
                while (true)
                {
                    if (m_alarmLogList.Count == 0)
                        m_forwardWaiter.WaitOne(-1);
                    using (var db = new AllInOneContext.AllInOneContext())
                    {
                        //默认第一个为本地应用中心节点()
                        var topApplicationCenter = db.Organization.Include(t => t.Center).
                                    OrderBy(t => t.OrganizationFullName).Select(t => t.Center).
                                    FirstOrDefault();

                        if (topApplicationCenter == null || topApplicationCenter.EndPoints == null || topApplicationCenter.EndPoints.Count == 0)
                        {
                            _logger.LogInformation("未配置上级服务器IP，报警上传等待10 min后再检测......");
                            m_forwardWaiter.WaitOne(10 * 60000);
                            continue;
                        }

                        EndPointInfo endPoint = topApplicationCenter.EndPoints.First();
                        string url = string.Format("http://{0}:{1}/Alarm/AlarmLog/Publish", endPoint.IPAddress, endPoint.Port);
                        //上传报警记录
                        string error = "";
                        lock (m_logLockObj)
                        {
                            for (int i = m_alarmLogList.Count - 1; i >= 0; i--)
                            {
                                AlarmLog log = m_alarmLogList[i];
                                if (log.UploadCount < 10)
                                {
                                    var result  = HttpClientHelper.Post<AlarmLog>(log, url);
                                    if (result.Success)
                                        log.UploadStatus = 1;  //上传完成
                                    else
                                        log.UploadCount++;
                                    db.AlarmLog.Update(log);
                                    db.SaveChanges();
                                    m_alarmLogList.RemoveAt(i);
                                }
                                else if (log.UploadCount == 10)
                                {
                                    //上传次数达到10次，记录....
                                    log.UploadCount++;
                                    db.AlarmLog.Update(log);
                                    db.SaveChanges();
                                    m_alarmLogList.Remove(log);
                                    //广播消息
                                    error = string.Format("多次尝试推送报警失败，取消推送{0}发生的{1}报警消息到上级系统!",
                                            log.AlarmSource.IPDeviceName, log.AlarmType.SystemOptionName);
                                    ForwardAlarmLogError forwardErr = new ForwardAlarmLogError()
                                    {
                                        ErrorDesc = error,
                                        CreateTime = DateTime.Now
                                    };
                                    MQPulish.PublishMessage("ForwardAlarmLogError", forwardErr);
                                }
                            }
                        }
                    }
                    // m_uploadWaiter.WaitOne(10 * 1000);
                }
            });
        }

    }
}
