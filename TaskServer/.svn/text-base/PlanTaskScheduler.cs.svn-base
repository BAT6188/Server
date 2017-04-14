using AlarmAndPlan.Model;
using HttpClientEx;
using Infrastructure.Model;
using Infrastructure.Utility;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TaskServer
{
    /// <summary>
    /// 预案任务定时器
    /// 主要实现任务的启动和运行检测（任务生命周期）
    /// </summary>
    public class PlanTaskScheduler
    {
        ILogger<PlanTaskScheduler> _logger = Logger.CreateLogger<PlanTaskScheduler>();

        /// <summary>
        /// 定时任务列表
        /// </summary>
        private List<TimerTask> m_timerTaskList;

        private object m_lockObj = new Object();

        private static PlanTaskScheduler m_instance = null;

        private AutoResetEvent m_planExecutorWaiter = new AutoResetEvent(false);

        /// <summary>
        /// 当前运行的预案
        /// </summary>
        private List<PlanExecutor> CurrentRunningPlans = new List<PlanExecutor>();

        private PlanTaskScheduler()
        {
            m_timerTaskList = new List<TimerTask>();
        }

        /// <summary>
        /// 启动预案任务
        /// </summary>
        public async void Start()
        {
            try
            {
                await Task.Delay(5000);
                //初始化任务，从应用服务获取预案
                string url = string.Format("{0}/Plan/TimerTask", GlobalSetting.AppServerBaseUrl);
                IEnumerable<TimerTask> timerTasks = HttpClientHelper.Get<TimerTask>(url);
                //过滤无效的预案
                timerTasks = timerTasks.Where(t => IsWaitingSchedule(t.TaskSchedule)).ToList();
                m_timerTaskList.AddRange(timerTasks);

                m_timerTaskList.ForEach(t => SetPlanNextExecute(t.TaskSchedule, DateTime.Now));
                //初始化，同时启动报警联动任务
                Task.Factory.StartNew(() => Run());
                _logger.LogInformation("------------------------Start plan task------------------------------");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("启动预案任务异常,Message:{0}\r\nStackTrace:{1}..",ex.Message,ex.StackTrace);
            }
        }

        /// <summary>
        /// 是否为将要运行的排程
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        private bool IsWaitingSchedule(Schedule schedule)
        {
            return schedule != null && schedule.ScheduleCycle != null && schedule.ScheduleCycle.DayPeriods != null && schedule.ScheduleCycle.DayPeriods.Count > 0;
        }

        public static PlanTaskScheduler Instance
        {
            get
            {
                if (m_instance == null)
                    m_instance = new PlanTaskScheduler();
                return m_instance;
            }
        }

        /// <summary>
        /// 添加预案动作
        /// </summary>
        /// <param name="task"></param>
        public void AddPlanTask(TimerTask task)
        {
            if (Monitor.TryEnter(m_lockObj))
            {
                try
                {
                    if (IsWaitingSchedule(task.TaskSchedule))
                    {
                        m_timerTaskList.Add(task);
                        m_planExecutorWaiter.Set();
                    }
                }
                finally
                {
                    Monitor.Exit(m_lockObj);
                }
            }
        }

        /// <summary>
        /// 移除预案动作
        /// </summary>
        /// <param name="taskId"></param>
        public void RemovePlanTask(Guid taskId)
        {
            if (Monitor.TryEnter(m_lockObj))
            {
                try
                {
                    TimerTask plan = m_timerTaskList.FirstOrDefault(t => t.TimerTaskId.Equals(taskId));
                    if (plan != null && !plan.PlanId.Equals(taskId))
                        m_timerTaskList.Remove(plan);
                }
                finally
                {
                    Monitor.Exit(m_lockObj);
                }
            }
        }

        private void Run()
        {
            while (true)
            {
                try
                {
                    TimerTask executeTask = m_timerTaskList.OrderBy(t => t.TaskSchedule.ScheduleCycle.NextExecute).FirstOrDefault(t=>!t.Running);
                    if (executeTask == null)
                        m_planExecutorWaiter.WaitOne();
                    //休眠
                    Schedule planSchedule = executeTask.TaskSchedule;
                    DateTime currTime = DateTime.Now;
                    //if (planSchedule.ScheduleCycle.NextExecute == null || !executePlan.Running) //任务服务重启的情况
                    //{
                    //    //更新执行时间
                    //    SetPlanNextExecute(planSchedule, currTime);
                    //}
                    if (planSchedule.ScheduleCycle.NextExecute != null)
                    {
                        int waitTimeout = (int)((planSchedule.ScheduleCycle.NextExecute.Value - currTime).TotalMilliseconds);
                        //误差2分钟内，有效执行
                        if (waitTimeout < 0 && waitTimeout > -2 * 60 * 1000)
                            waitTimeout = 0;
                        if (waitTimeout >= 0)
                        {
                            _logger.LogInformation("等待{0}ms后将执行预案....", waitTimeout);
                            TimePeriod tp = ScheduleUtility.GetPlanTaskTimePeriod(planSchedule, planSchedule.ScheduleCycle.NextExecute.Value);
                            // 运行预案
                            PlanTaskWrapper task = new PlanTaskWrapper()
                            {
                                Plan = executeTask.Plan,
                                RunTimePeriod = tp,
                                TaskId = executeTask.TimerTaskId
                            };
                            PlanExecutor executor = new PlanExecutor(task);
                            executor.OnPlanExecutedEnd += Executor_OnPlanExecutedEnd;
                            executeTask.Running = true;
                            var planTask = Task.Delay(waitTimeout).ContinueWith((runTask) => executor.Start());
                            AddPlanExecutorToCache(executor);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("执行预案异常：Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                }
            }
        }

        private void Executor_OnPlanExecutedEnd(object sender, EventArgs e)
        {
            PlanExecutor executor = sender as PlanExecutor;
            executor.OnPlanExecutedEnd -= Executor_OnPlanExecutedEnd;
            //executor.PlanTask.Plan.Running = false;
            m_planExecutorWaiter.Set();

            CurrentRunningPlans.Remove(executor);

            //已完成/停止，更新下一执行时间
            TimerTask task = m_timerTaskList.FirstOrDefault(t => t.TimerTaskId.Equals(executor.PlanTask.TaskId));
            task.Running = false;
            SetPlanNextExecute(task.TaskSchedule, DateTime.Now);
        }

        /// <summary>
        /// 设置预案运行时间
        /// </summary>
        /// <param name="planSchedule"></param>
        /// <returns></returns>
        private void SetPlanNextExecute(Schedule planSchedule,DateTime startTime)
        {
            try
            {
                //更新任务下执行时间
                DateTime? executeTime = ScheduleUtility.GetExecuteTime(planSchedule, startTime);
                planSchedule.ScheduleCycle.LastExecute = planSchedule.ScheduleCycle.NextExecute;
                planSchedule.ScheduleCycle.NextExecute = executeTime;
                //更新任务执行时间
                string url = string.Format("{0}/Infrastructure/Schedule", GlobalSetting.AppServerBaseUrl);
                string result = string.Empty;
                HttpClientHelper.Put<Schedule>(planSchedule, url);
                _logger.LogInformation("更新预案下一运行时间....");
            }
            catch (Exception ex)
            {
                _logger.LogError("更新预案运行时间异常：Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 保存运行的预案到缓存
        /// </summary>
        /// <param name="executor"></param>
        internal void AddPlanExecutorToCache(PlanExecutor executor)
        {
            CurrentRunningPlans.Add(executor);
        }

        /// <summary>
        /// 从缓存中移除正在执行的预案
        /// </summary>
        /// <param name="planId"></param>
        internal void RemovePlanExecutorFromCache(Guid planId)
        {
            PlanExecutor executor = CurrentRunningPlans.FirstOrDefault(t => t.PlanTask.Plan.PlanId.Equals(planId));
            if (executor != null)
            {
                executor.Stop();
                CurrentRunningPlans.Remove(executor);
            }
            else
            {
                _logger.LogInformation("预案已停止或者位执行，不执行停止动作.");
            }
        }

        /// <summary>
        /// 判断预案是否正在运行
        /// </summary>
        /// <returns></returns>
        internal bool PlanIsRunning(Guid planId)
        {
            PlanExecutor executor = CurrentRunningPlans.FirstOrDefault(t => t.PlanTask.Plan.PlanId.Equals(planId));
            return executor != null;
        }

        /// <summary>
        /// 预案任务重置，适用于报警复位，停止报警动作
        /// </summary>
        internal void Reset()
        {
            int count = CurrentRunningPlans.Count - 1;
            for (int i = count; i >= 0; i--)
            {
                var executor = CurrentRunningPlans[i];
                if (executor.PlanTask.Plan.PlanTrigger != null)
                {
                    executor.Stop();
                    CurrentRunningPlans.RemoveAt(i);
                }
            }
        }
    }
}
