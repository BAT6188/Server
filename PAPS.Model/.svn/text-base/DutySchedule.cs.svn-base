using Infrastructure.Model;
using System;
using System.Collections.Generic;

namespace PAPS.Model
{
    /// <summary>
    /// 值班安排表
    /// </summary>
    public class DutySchedule
    {
        /// <summary>
        /// 值班安排表ID
        /// </summary>
        public Guid DutyScheduleId
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

        /// <summary>
        /// 制表人ID
        /// </summary>
        public Guid ListerId
        {
            get; set;
        }

        /// <summary>
        /// 制表人
        /// </summary>
        public virtual Staff Lister
        {
            get;set;
        }

        /// <summary>
        /// 制表时间
        /// </summary>
        public DateTime TabulationTime
        {
            get;set;
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate
        {
            get;set;
        }

       
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate
        {
            get;set;
        }

        /// <summary>
        /// 查勤周期ID
        /// </summary>
        public Guid ScheduleId
        {
            get; set;
        }

        /// <summary>
        /// 查勤周期
        /// </summary>
        public virtual Schedule Schedule
        {
            get; set;
        }

        /// <summary>
        /// 值班安排表明细集合
        /// </summary>
        public List<DutyScheduleDetail> DutyScheduleDetails
        {
            get;set;
        }

    }
}