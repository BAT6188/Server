using System;

namespace Infrastructure.Model
{
    /// <summary>
    /// 任务排程
    /// </summary>
    public class Schedule
    {
        /// <summary>
        /// 任务排程ID
        /// </summary>
        public Guid ScheduleId
        {
            get;set;
        }

        /// <summary>
        /// 排程名称
        /// </summary>
        public string ScheduleName
        {
            get;set;
        }

        /// <summary>
        /// 排程类型选项id
        /// </summary>
        public Guid ScheduleTypeId
        {
            get;set;
        }

        /// <summary>
        /// 排程类型
        /// </summary>
        public virtual SystemOption ScheduleType
        {
            get;
            set;
        }

        /// <summary>
        /// 执行周期
        /// </summary>
        public ScheduleCycle ScheduleCycle
        {
            get;set;
        }

        /// <summary>
        /// 生效时间
        /// </summary>
        public DateTime EffectiveTime
        {
            get;set;
        }

        /// <summary>
        ///  失效时间
        /// </summary>
        public DateTime? ExpirationTime
        {
            get;set;
        }


    }
}
