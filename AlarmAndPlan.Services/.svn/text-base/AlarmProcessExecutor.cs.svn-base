/***
 * 2016-12-16 zhrx  报警关闭，下发报警关闭通知到哨位中心服务
 * 2016-12-22 zhrx  哨位设备编号调整，已哨位节点为准
 */
using AlarmAndPlan.Model;
using HttpClientEx;
using Infrastructure.Model;
using Infrastructure.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PAPS.Data;
using Resources.Model;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AlarmAndPlan.Services
{
    /// <summary>
    /// 报警处理任务器
    /// </summary>
    public class AlarmProcessExecutor
    {
        private ILogger<AlarmProcessExecutor> _logger = Logger.CreateLogger<AlarmProcessExecutor>();

        /// <summary>
        /// 报警日志队列
        /// </summary>
        private ConcurrentQueue<AlarmLog> m_alarmLogQueue;

        private static AlarmProcessExecutor m_instance = null;

        /// <summary>
        /// 报警队列线程信号
        /// </summary>
        private ManualResetEvent m_queueThreadSemaphore = null;

        private WaitHandle[] m_WaitHandles = null;

        private AlarmProcessExecutor()
        {
            m_alarmLogQueue = new ConcurrentQueue<AlarmLog>();
            m_queueThreadSemaphore = new ManualResetEvent(true);
            m_WaitHandles = new WaitHandle[] { m_queueThreadSemaphore };

            //初始化，同时启动报警处理任务
            Task.Factory.StartNew(() => Run());
        }

        public static AlarmProcessExecutor Instance
        {
            get
            {
                if (m_instance == null)
                    m_instance = new AlarmProcessExecutor();
                return m_instance;
            }
        }

        public void AddAlarmLogAction(AlarmLog alarmLog)
        {
            m_alarmLogQueue.Enqueue(alarmLog);
            m_queueThreadSemaphore.Set();
        }

        private void Run()
        {
            while (WaitHandle.WaitAny(m_WaitHandles) == 0)
            {
                if (m_alarmLogQueue.Count == 0)
                    m_queueThreadSemaphore.Reset();

                //出列，处理
                AlarmLog alarmLog = null;
                bool dequeueOk = m_alarmLogQueue.TryDequeue(out alarmLog);
                if (dequeueOk)
                {
                    //报警联动
                    using (var db = new AllInOneContext.AllInOneContext())
                    {
                        try
                        {
                            var setting = GetAlarmSetting(db, alarmLog);
                            if (setting != null)
                            {
                                int code = 0;
                                if (Int32.TryParse(setting.AlarmSource.Organization.OrganizationCode, out code))
                                    setting.AlarmSource.IPDeviceCode = code;
                                Plan plan = null;
                                DeviceAlarmStatus status = null;
                                //本地广播报警消息
                                MQPulish.PublishMessage("AlarmLog", alarmLog);
                                if (alarmLog.AlarmStatusId.Equals(Guid.Parse("A0002016-E009-B019-E001-ABCD13100001"))) //执行预案，状态未确认
                                {
                                    if (ScheduleUtility.IsValidSchedule(setting.Schedule, DateTime.Now))  //符合当前排班的情况
                                    {
                                        ////本地广播报警消息
                                        //MQPulish.PublishMessage("AlarmLog", alarmLog);
                                        plan = db.Plan.Include(t => t.Actions).ThenInclude(t => t.PlanActions).ThenInclude(t=>t.Action).FirstOrDefault(t => t.PlanId.Equals(setting.BeforePlanId));
                                    }
                                }
                                else if ( alarmLog.AlarmStatusId.Equals(Guid.Parse("A0002016-E009-B019-E001-ABCD13100002"))) //执行应急预案
                                {
                                    //预案确认
                                    plan = db.Plan.Include(t => t.Actions).ThenInclude(t => t.PlanActions).ThenInclude(t=>t.Action).FirstOrDefault(t => t.PlanId.Equals(setting.EmergencyPlanId));

                                    //哨位报警，确认需推送报警确认/取消到哨位中心服务
                                    status = GetDeviceAlarmStatus(db, setting, true);
                                }
                                else if (alarmLog.AlarmStatusId.Equals(Guid.Parse("A0002016-E009-B019-E001-ABCD13100003"))) //报警取消，停止联动动作
                                {
                                    //哨位报警，取消需推送报警确认/取消到哨位中心服务
                                    status = GetDeviceAlarmStatus(db, setting, false);
                                    StopPlan(setting);
                                }
                                else if (alarmLog.AlarmStatusId.Equals(Guid.Parse("A0002016-E009-B019-E001-ABCD13100004"))) //停止联动动作,已关闭
                                {
                                    status = GetDeviceAlarmStatus(db, setting, false);
                                    StopPlan(setting);
                                }

                                //找到满足条件的预案，发送到任务服务，启动预案
                                if (plan != null)
                                {
                                    plan.PlanTrigger = new PlanTriggerSource() {
                                        AlarmSource = setting.AlarmSource,
                                        AlarmType = setting.AlarmType,
                                        BeforePlanId = setting.BeforePlanId,
                                        EmergencyPlanId = setting.EmergencyPlanId,
                                        AlarmLogId = alarmLog.AlarmLogId
                                    };
                                    StartPlan(plan);
                                }
                                if (status != null)
                                    SendAlarmStatusToASCS(status, setting.AlarmSource.OrganizationId);
                            }
                            else
                            {
                                _logger.LogInformation("未配置报警，没有任务联动响应!!!!");
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError("报警处理异常，Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取设备报警配置
        /// </summary>
        /// <param name="db"></param>
        /// <param name="alarmLog"></param>
        /// <returns></returns>
        private AlarmSetting GetAlarmSetting(AllInOneContext.AllInOneContext db, AlarmLog alarmLog)
        {
            AlarmSetting setting = db.AlarmSetting.Include(t => t.Schedule).ThenInclude(t => t.ScheduleCycle).ThenInclude(t => t.DayPeriods).ThenInclude(t=>t.TimePeriods).
                            Include(t => t.Schedule).ThenInclude(t => t.ScheduleCycle).ThenInclude(t => t.CycleType).
                            Include(t => t.AlarmSource).ThenInclude(t=>t.Organization).Include(t => t.AlarmType).
                            FirstOrDefault(p => p.AlarmSourceId.Equals(alarmLog.AlarmSourceId) &&
                                p.AlarmTypeId.Equals(alarmLog.AlarmTypeId));
            return setting;
        }


        /// <summary>
        /// 获取报警推送到哨位中心的状态
        /// </summary>
        /// <param name="db"></param>
        /// <param name="setting"></param>
        /// <param name="alarmStatus"></param>
        /// <returns></returns>
        private DeviceAlarmStatus GetDeviceAlarmStatus(AllInOneContext.AllInOneContext db, AlarmSetting setting, bool alarmStatus)
        {
            DeviceAlarmStatus status = null;
            var deviceType = db.SystemOption.FirstOrDefault(t => t.SystemOptionId.Equals(setting.AlarmSource.DeviceTypeId));
            if (deviceType != null)
            {
                if (deviceType.ParentSystemOptionId.Equals(Guid.Parse("A0002016-E009-B019-E001-ABCD11000004")))
                {
                    var sentinel = db.Sentinel.Include(t => t.DeviceInfo).FirstOrDefault(t => t.DeviceInfoId.Equals(setting.AlarmSource.IPDeviceInfoId));
                    status = new DeviceAlarmStatus()
                    {
                        DeviceCode = sentinel.DeviceInfo.IPDeviceCode,
                        SentinelCode = sentinel.DeviceInfo.IPDeviceCode,
                        //DeviceCode = Int32.Parse(sentinel.DeviceInfo.Organization.OrganizationCode),
                        //SentinelCode = Int32.Parse(sentinel.DeviceInfo.Organization.OrganizationCode),
                        AlarmType = setting.AlarmTypeId,
                        AlarmStatus = alarmStatus
                    };
                }
                else if (deviceType.ParentSystemOptionId.Equals(Guid.Parse("A0002016-E009-B019-E001-ABCDEF000110")))
                {
                    //获取防区设备关联的哨位台
                    var defenceDevice = db.DefenseDevice.Include(t => t.Sentinel).ThenInclude(t => t.DeviceInfo).ThenInclude(t=>t.Organization)
                        .Include(t=>t.DeviceInfo)
                        .First(t => t.DeviceInfoId.Equals(setting.AlarmSource.IPDeviceInfoId));
                    status = new DeviceAlarmStatus()
                    {
                        DeviceCode = defenceDevice.DeviceInfo.IPDeviceCode,
                       // SentinelCode = defenceDevice.Sentinel.DeviceInfo.IPDeviceCode,
                        SentinelCode = Int32.Parse(defenceDevice.Sentinel.DeviceInfo.Organization.OrganizationCode),
                        AlarmType = setting.AlarmTypeId,
                        AlarmStatus = alarmStatus
                    };
                }
            }
            return status;
        }

        /// <summary>
        /// 发送报警状态到哨位中心服务
        /// </summary>
        /// <param name="db"></param>
        /// <param name="status"></param>
        /// <param name="deviceOrgId"></param>
        private void SendAlarmStatusToASCS(DeviceAlarmStatus status, Guid deviceOrgId)
        {
            Task.Run(new Action(() =>
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    _logger.LogInformation("推送报警状态到哨位心...");
                    Guid ascsServerType = Guid.Parse("A0002016-E009-B019-E001-ABCD11300206");

                    //哨位台挂在哨位节点上面
                    var org = db.Organization.FirstOrDefault(t => t.OrganizationId.Equals(deviceOrgId));
                    var service = db.ServiceInfo.Include(t => t.ServerInfo).
                        FirstOrDefault(t => t.ServiceTypeId.Equals(ascsServerType) && (
                            t.ServerInfo.OrganizationId.Equals(org.ParentOrganizationId) || t.ServerInfo.OrganizationId.Equals(deviceOrgId)));

                    //var service = new ServiceInfo() {
                    //    EndPointsJson = "[{\"IPAddress\":\"192.168.18.76\",\"Port\":5002}]"
                    //};
                    if (service != null)
                    {
                        try
                        {
                            ASCSApi ascs = new ASCSApi(service);
                            var r = ascs.SendDeviceAlarmStatus(status);
                            _logger.LogInformation("推送报警状态到哨位中心结果：{0}...", r.Success);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError("发送报警状态到哨位中心异常：{0}\r\n{1}", ex.Message, ex.StackTrace);
                        }
                        return;
                    }
                }
                _logger.LogInformation("未配置哨位中心服务，取消推送....");
            }));
        }


        private void StopPlan(AlarmSetting setting)
        {
            Task.Run(new Action(() =>
            {
                if (setting.BeforePlanId != null)
                    StopPlan(setting.BeforePlanId.Value);
                if (setting.EmergencyPlanId != null)
                    StopPlan(setting.EmergencyPlanId.Value);
            }));
        }

        private void StopPlan(Guid planId)
        {
            try
            {
                string url = string.Format("{0}/Task/PlanAction/Stop/planId={1}", GlobalSetting.TaskServerBaseUrl, planId);
                string error = "";
                bool b = HttpClientHelper.Delete(url, ref error);
                _logger.LogInformation("调用{0}停止预案，Error:{1}", url, error);
            }
            catch (Exception ex)
            {
                _logger.LogError("停止预案异常：Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
            }
        }

        private void StartPlan(Plan plan)
        {
            Task.Run(new Action(() =>
            {
                string url = string.Format("{0}/Task/PlanAction/Start", GlobalSetting.TaskServerBaseUrl);
                _logger.LogInformation("调用{0}启动预案，Begin...", url);
                var result = HttpClientHelper.Post<Plan>(plan, url);
                _logger.LogInformation("调用{0}启动预案，End.Result:{1}", url, result);
            }));
        }

    }
}
