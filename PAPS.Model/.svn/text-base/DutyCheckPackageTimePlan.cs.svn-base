using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PAPS.Model
{
    /// <summary>
    /// 查勤包计划
    /// </summary>
    public class DutyCheckPackageTimePlan
    {
        /// <summary>
        /// 查勤包计划ID
        /// </summary>
        public Guid DutyCheckPackageTimePlanId
        {
            get; set;
        }


        /// <summary>
        /// 随机抽检率
        /// </summary>
        public double RandomRate
        {
            get;set;
        }

        /// <summary>
        /// 查勤包生成周期ID
        /// </summary>
        public Guid ScheduleId
        {
            get; set;
        }

        /// <summary>
        /// 生成查勤包排程周期
        /// </summary>
        public virtual Schedule Schedule
        {
            get;set;
        }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public Guid OrganizationId
        {
            get; set;
        }

        /// <summary>
        /// 组织机构
        /// </summary>
        public virtual Organization Organization
        {
            get;set;
        }

        [NotMapped]
        public bool Running { get; set; }

    }
}