using Infrastructure.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlarmAndPlan.Model
{
    public class AlarmProcessed
    {
        public Guid AlarmProcessedId
        {
            get;set;
        }

        /// <summary>
        /// 处理意见
        /// </summary>
        public string Conclusion { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime TimeProcessed
        {
            get;set;
        }

        public User ProcessedBy
        {
            get;
            set;
        }

        public Guid AlarmLogId
        {
            get;
            set;
        }

        /// <summary>
        /// 处理的报警日志
        /// </summary>
        public virtual AlarmLog AlarmLog
        {
            get;
            set;
        }
    }
}
