using Resources.Model;
using System;
using System.Collections.Generic;

namespace AlarmAndPlan.Model
{
    public class PlanAction
    {
        public Guid PlanActionId
        {
            get;set;
        }

        public Guid PlanDeviceId
        {
            get;set;
        }

        /// <summary>
        /// 联动响应设备
        /// </summary>
        public virtual IPDeviceInfo PlanDevice
        {
            get;set;
        }


        public List<PredefinedAction> PlanActions
        {
            get;set;
        }

    }
}
