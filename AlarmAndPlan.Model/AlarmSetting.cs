using Infrastructure.Model;
using Resources.Model;
using System;

namespace AlarmAndPlan.Model
{
    public class AlarmSetting
    {
        /// <summary>
        /// 报警配置Id
        /// </summary>
        public Guid AlarmSettingId
        {
            get;set;
        }

        /// <summary>
        /// 报警源设备id
        /// </summary>
        public Guid AlarmSourceId
        {
            get;set;
        }

        /// <summary>
        /// 应急预案id
        /// </summary>
        public Guid? EmergencyPlanId
        {
            get;set;
        }

        public Guid? BeforePlanId
        {
            get;set;
        }

        public Guid ScheduleId
        {
            get;set;
        }

        /// <summary>
        /// 报警源设备
        /// </summary>
        public virtual IPDeviceInfo AlarmSource
        {
            get;set;
        }

        /// <summary>
        /// 布防排程
        /// </summary>
        public virtual Schedule Schedule
        {
            get;set;
        }

        /// <summary>
        /// 应急预案
        /// </summary>
        public  virtual Plan EmergencyPlan
        {
            get;set;
        }

        /// <summary>
        /// 确认前预案
        /// </summary>
        public virtual Plan BeforePlan
        {
            get; set;
        }

        public Guid AlarmTypeId
        {
            get; set;
        }

        public Guid AlarmLevelId
        {
            get;set;
        }

        /// <summary>
        /// 报警类型
        /// </summary>
        public virtual SystemOption AlarmType
        {
            get;set;
        }

        public virtual SystemOption AlarmLevel
        {
            get;set;
        }
    }
}
