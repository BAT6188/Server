using Infrastructure.Model;
using System;

namespace PAPS.Model
{
    /// <summary>
    /// 值班安排
    /// </summary>
    public class DutyCheckSchedule
    {
        public Guid DutyCheckScheduleId { get; set; }

        /// <summary>
        /// 主班ID
        /// </summary>
        public Guid LeaderId
        {
            get; set;
        }

        /// <summary>
        /// 主班
        /// </summary>
        public virtual Staff Leader
        {
            get;set;
        }

        /// <summary>
        /// 副班ID
        /// </summary>
        public Guid? DeputyId
        {
            get;
            set;
        }

        /// <summary>
        /// 副班
        /// </summary>
        public virtual Staff Deputy
        {
            get;
            set;
        }

        /// <summary>
        /// 检查时段ID
        /// </summary>
        public Guid? CheckTimePeriodId { get; set; }

        /// <summary>
        /// 检查时段
        /// </summary>
        public virtual TimePeriod CheckTimePeriod { get; set; }
    }
}
