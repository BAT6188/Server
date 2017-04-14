using Infrastructure.Model;
using Newtonsoft.Json;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlarmAndPlan.Model
{
    public class Plan
    {
        public Guid PlanId { get; set; }

        public string PlanName { get; set; }

        //public Guid? ScheduleId { get; set; }

        public Guid? PlanTypeId { get; set; }

        /// <summary>
        /// 预案类型:报警预案，普通预案....
        /// </summary>
        public virtual SystemOption PlanType { get; set; }

       // public virtual Schedule Schedule { get; set; }

        public List<PlanAction> Actions
        {
            get;
            set;
        }

        ///// <summary>
        ///// 是否正在运行
        ///// 任务服务启动后，需更新执行时间
        ///// </summary>
        //[NotMapped]
        //[JsonIgnore]
        //public bool Running
        //{
        //    get;set;
        //}

        ///// <summary>
        ///// 报警事件id.
        ///// 为了标记预案执行的根源
        ///// </summary>
        //[NotMapped]
        //public Guid? AlarmLogId { get; set; }

        /// <summary>
        /// 管理的报警配置信息,在报警联动执行时用到 2016-11-03
        /// </summary>
        [NotMapped]
        public PlanTriggerSource PlanTrigger { get; set; }

        public Guid? TvVideoRoundSceneId
        {
            get;set;
        }

        public Guid? RealVideoRoundSceneId
        {
            get; set;
        }

        /// <summary>
        /// 实时预览场景
        /// </summary>
        public virtual VideoRoundScene RealVideoRoundScene
        {
            get; set;
        }

        /// <summary>
        /// 视频上墙场景
        /// </summary>
        public virtual VideoRoundScene TvVideoRoundScene { get; set; }
    }
}
