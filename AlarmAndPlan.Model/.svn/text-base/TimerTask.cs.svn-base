using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AlarmAndPlan.Model
{
    /// <summary>
    /// 定时任务计划
    /// </summary>
    public class TimerTask
    {
        public Guid TimerTaskId { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string TimerTaskName { get; set; }

        /// <summary>
        /// 定时任务描述
        /// </summary>
        public string Description { get; set; }

        public Guid? PlanId { get; set; }

        public Guid TaskScheduleId { get; set; }

        /// <summary>
        /// 定时执行的预案
        /// </summary>
        public Plan Plan { get; set; }

        /// <summary>
        /// 定时触发器
        /// </summary>
        public Schedule TaskSchedule { get; set; }

        [NotMapped]
        public bool Running { get; set; }
    }
}
