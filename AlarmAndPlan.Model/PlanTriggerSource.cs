using Infrastructure.Model;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlarmAndPlan.Model
{
    /// <summary>
    /// 预案触发源
    /// </summary>
    public class PlanTriggerSource
    {
        public Guid? BeforePlanId { get; set; }

        public Guid? EmergencyPlanId { get; set; }

        /// <summary>
        /// 报警源设备
        /// </summary>
        public virtual IPDeviceInfo AlarmSource
        {
            get; set;
        }

        /// <summary>
        /// 报警类型
        /// </summary>
        public virtual SystemOption AlarmType
        {
            get; set;
        }

        public Guid? AlarmLogId { get; set; }
        //public virtual SystemOption AlarmLevel
        //{
        //    get; set;
        //}
    }
}
