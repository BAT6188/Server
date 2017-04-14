
using AllInOneContext;
using HttpClientEx;
using Infrastructure.Model;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PAPS.Model;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace PAPS.Services
{
    /// <summary>
    /// 查勤包生成运行类
    /// </summary>
    public class DutyCheckPackageRunner
    {
        ILogger<DutyCheckPackageRunner> _logger = Logger.CreateLogger<DutyCheckPackageRunner>();

        /// <summary>
        ///查勤计划列表
        /// </summary>
        private List<DutyCheckPackageTimePlan> m_timerList;

        private AutoResetEvent m_planExecutorWaiter = new AutoResetEvent(false);

        private static DutyCheckPackageRunner m_instance = null;

        public static DutyCheckPackageRunner Instance
        {
            get
            {
                if (m_instance == null)
                    m_instance = new DutyCheckPackageRunner();
                return m_instance;
            }
        }

        /// <summary>
        /// 启动
        /// </summary>
        /// <returns></returns>
        public void Start()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {

                    m_planExecutorWaiter.WaitOne(30 * 1000);

                    IEnumerable<DutyCheckPackageTimePlan> timePlans = GetDutyCheckPackage();
                    m_timerList = new List<DutyCheckPackageTimePlan>();
                    m_timerList.AddRange(timePlans);
                    m_timerList.ForEach(t => SetPlanNextExecute(t.Schedule, DateTime.Now));
                    Run();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("运行查勤计划生成，Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                }
            });

        }

        private static IEnumerable<DutyCheckPackageTimePlan> GetDutyCheckPackage()
        {
            //初始化查勤计划，从应用服务获取查勤计划
            string url = string.Format("{0}/Paps/DutyCheckPackageTimePlan", GlobalSetting.AppServerBaseUrl);
            IEnumerable<DutyCheckPackageTimePlan> timePlans = HttpClientHelper.Get<DutyCheckPackageTimePlan>(url);
            return timePlans;
        }


        private void Run()
        {
            while (true)
            {
                try
                {
                    DutyCheckPackageTimePlan executeTask = m_timerList.OrderBy(t => t.Schedule.ScheduleCycle.NextExecute).FirstOrDefault(t => !t.Running);
                    if (executeTask == null)
                        m_planExecutorWaiter.WaitOne();
                    //休眠
                    Schedule planSchedule = executeTask.Schedule;
                    DateTime currTime = DateTime.Now;
                    if (planSchedule.ScheduleCycle.NextExecute != null && !executeTask.Running)
                    {
                        int waitTimeout = (int)((planSchedule.ScheduleCycle.NextExecute.Value - currTime).TotalMilliseconds);
                        //误差2分钟内，有效执行
                        if (waitTimeout < 0 && waitTimeout > -2 * 60 * 1000)
                            waitTimeout = 0;
                        if (waitTimeout >= 0)
                        {
                            TimePeriod tp = ScheduleUtility.GetPlanTaskTimePeriod(planSchedule, planSchedule.ScheduleCycle.NextExecute.Value);
                            executeTask.Running = true;
                            bool isGenerate = DutyCheckPackageHelper.CheckDutyPackageExist(executeTask.OrganizationId);
                            if (isGenerate)
                            {
                                DutyCheckPackageHelper.AllocationDutychekPackage(executeTask.OrganizationId, DateTime.Now);
                            }
                            var planTask = Task.Delay(waitTimeout).ContinueWith((runTask) => GetDutyCheckPackage());
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("执行查勤包异常：Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                }
            }
        }


        /// <summary>
        /// 设置查勤包运行时间
        /// </summary>
        /// <param name="planSchedule"></param>
        /// <returns></returns>
        private void SetPlanNextExecute(Schedule planSchedule, DateTime startTime)
        {
            try
            {
                //更新查勤计划下执行时间
                DateTime? executeTime = ScheduleUtility.GetExecuteTime(planSchedule, startTime);
                planSchedule.ScheduleCycle.LastExecute = planSchedule.ScheduleCycle.NextExecute;
                planSchedule.ScheduleCycle.NextExecute = executeTime;
                //更新查勤计划执行时间
                string url = string.Format("{0}/Infrastructure/schedule", GlobalSetting.AppServerBaseUrl);
                string result = string.Empty;
                HttpClientHelper.Put<Schedule>(planSchedule, url);
                _logger.LogInformation("更新查勤计划下一运行时间....");
            }
            catch (Exception ex)
            {
                _logger.LogError("更新查勤计划运行时间异常：Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
            }
        }

    }
}
