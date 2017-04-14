using AlarmAndPlan.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskServer.Action
{
    /// <summary>
    /// 预案预定义动作执行
    /// </summary>
    abstract class  PlanActionProvider
    {
        /// <summary>
        /// 预案执行触发源
        /// </summary>
         public PlanTriggerSource PlanTrigger { get; set; }

        public Guid PlanId { get; set; }


        /// <summary>
        ///  动作执行
        /// </summary>
       public  abstract void Execute();

        /// <summary>
        /// 
        /// </summary>
       public abstract void Stop();

        /// <summary>
        /// 添加预案动作参数
        /// </summary>
        /// <param name="argument"></param>
        public abstract void AddPlanActionItem(PlanActionArgument argument);
    }
}
