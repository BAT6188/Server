using AlarmAndPlan.Model;
using Infrastructure.Utility;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TaskServer.Action;
using TaskServer.Argument;

namespace TaskServer
{
    /// <summary>
    /// 预案动作执行器
    /// </summary>
    internal class PlanExecutor : IDisposable
    {
        ILogger<PlanExecutor> _logger = Logger.CreateLogger<PlanExecutor>();

        private List<PlanActionProvider> m_actionProviders = new List<PlanActionProvider>();

        //private Priority

        /// <summary>
        /// 预案执行终止事件
        /// </summary>
        public event EventHandler OnPlanExecutedEnd;

        private CancellationTokenSource m_TaskWaitTokenSource = null;

        public PlanExecutor(PlanTaskWrapper task)
        {
            PlanTask = task;
        }
        public PlanTaskWrapper PlanTask
        {
            get; set;
        }

        /// <summary>
        /// 启动预案
        /// </summary>
        public void Start()
        {
            //Run(PlanTask.Plan);
            Task.Factory.StartNew(() =>
            {
                Run(PlanTask.Plan);
            });
        }

        private async void Run(Plan plan)
        {
            if ((plan.Actions != null && plan.Actions.Count > 0) || plan.TvVideoRoundSceneId != null)
            {
                //预案动作分类
                _logger.LogInformation("开始执行预案{0}，按动作分类Begin..",plan.PlanId);
                IEnumerable<IGrouping<string, PlanActionArgument>> actionGroup = GroupPlanAction(plan);
                SoundLightAction.RemoveActionArgument(plan.PlanId);
                foreach (var action in actionGroup)
                {
                    if (GlobalSetting.PlanActionOptions.ContainsKey(action.Key))
                    {
                        string actionName = GlobalSetting.PlanActionOptions[action.Key];
                        PlanActionProvider executor = GetPlanActionExecutor("TaskServer.Action." + actionName);
                        executor.PlanTrigger = plan.PlanTrigger;
                        executor.PlanId = plan.PlanId;
                        action.ToList().ForEach(t =>
                        {
                            try
                            {
                                executor.AddPlanActionItem(t);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError("预案动作分类异常：Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                            }
                        });
                    }
                    else
                    {
                        _logger.LogInformation("Not define action:{0}", action.Key);
                    }
                }
                //上墙动作
                if (plan.TvVideoRoundSceneId != null)
                {
                    TvAction tvAction = new TvAction();
                    tvAction.PlanTrigger = plan.PlanTrigger;
                    tvAction.PlanId = plan.PlanId;
                    tvAction.AddPlanActionItem(new TvVideoPlayArgument()
                    {
                        VideoRoundSceneId = plan.TvVideoRoundSceneId.Value
                    });
                    m_actionProviders.Add(tvAction);
                }
                _logger.LogInformation("预案{0}，按动作分类End..", plan.PlanId);

                //执行，动作异步执行
                m_actionProviders.ForEach(t => Task.Run(new System.Action(() => t.Execute())));
                Task.Run(() => SoundLightAction.Execute(plan));

                //等待停止
                bool result = await Wait();
                if (OnPlanExecutedEnd != null)
                    OnPlanExecutedEnd(this, new EventArgs());
                //正常停止
                if (result)
                {
                    Stop();
                }
            }
        }

        /// <summary>
        /// 预案运行等待,true正常停止，返回false,令牌取消，终止
        /// </summary>
        /// <returns></returns>
        private async Task<bool> Wait()
        {
            var result = await Task.Run<bool>(async () =>
            {
                m_TaskWaitTokenSource = new CancellationTokenSource();
                try
                {
                    if (PlanTask.RunTimePeriod.EndTime == DateTime.MaxValue)
                    {
                        await Task.Delay(-1, m_TaskWaitTokenSource.Token);
                    }
                    else
                    {
                        await Task.Delay(PlanTask.RunTimePeriod.EndTime - PlanTask.RunTimePeriod.StartTime, m_TaskWaitTokenSource.Token);
                    }
                    _logger.LogInformation("预案任务结束时间已到,正常停止预案...");
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError("任务等待异常：Exception{0}\r\nStackTrace{1}", ex.Message, ex.StackTrace);
                    return false;
                }
            });
            return result;
        }

        /// <summary>
        /// 获取预案动作运行对象
        /// </summary>
        /// <param name="fullName">PlanActionProvider子类型全名，包含命名空间</param>
        /// <returns></returns>
        private PlanActionProvider GetPlanActionExecutor(string fullName)
        {
            PlanActionProvider executor = Assembly.Load(new AssemblyName("TaskServer")).CreateInstance(fullName) as PlanActionProvider;
            m_actionProviders.Add(executor);
            return executor;
        }

        /// <summary>
        /// 停止执行动作 ,进行停止
        /// </summary>
        public void Stop()
        {
            if (!m_TaskWaitTokenSource.IsCancellationRequested)
                m_TaskWaitTokenSource.Cancel();
            foreach (PlanActionProvider action in m_actionProviders)
            {
                Task.Run(new System.Action(() => action.Stop()));
            }
            //停止声光报警动作
            Task.Run(new System.Action(() => SoundLightAction.Stop(PlanTask.Plan)));
            //同事更新预案的下一执行时间
            //PlanTask.Plan.Running = false;
        }

        /// <summary>
        /// 预案数据整理分组
        /// </summary>
        /// <param name="plan"></param>
        private IEnumerable<IGrouping<string, PlanActionArgument>> GroupPlanAction(Plan plan)
        {
            var data = new List<PlanActionArgument>();
            plan.Actions.ForEach(t => t.PlanActions.ForEach(f => data.Add(
                new PlanActionArgument
                {
                    DeviceInfo = t.PlanDevice,
                    ActionCode = f.Action.SystemOptionCode,
                    Argument = f.ActionArgument,
                    //AlarmLogId = plan.AlarmLogId == null ? plan.AlarmLogId.Value : Guid.Empty
                })));
            return data.GroupBy(t => t.ActionCode, t => t);
        }

        public void Dispose()
        {
            
        }
    }
}