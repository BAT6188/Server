using Infrastructure.Model;
using System;
using System.Collections.Generic;

namespace PAPS.Model
{
    /// <summary>
    /// 分队-勤务编组安排表
    /// </summary>
    public class DutyGroupSchedule
    {

        /// <summary>
        /// 勤务编组安排表ID
        /// </summary>
        public Guid DutyGroupScheduleId
        {
            get; set;
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
            get; set;
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
            get; set;
        }

        /// <summary>
        /// 制表时间
        /// </summary>
        public DateTime TabulationTime
        {
            get; set;
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate
        {
            get; set;
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate
        {
            get; set;
        }


        /// <summary>
        /// 勤务排程周期ID
        /// </summary>
        public Guid ScheduleId
        {
            get; set;
        }

        /// <summary>
        /// 勤务排程周期
        /// </summary>
        public virtual Schedule Schedule
        {
            get; set;
        }


        /// <summary>
        /// 分队勤务编组安排表明细
        /// </summary>
        public List<DutyGroupScheduleDetail> DutyGroupScheduleDetails
        {
            get;set;
        }

        /// <summary>
        /// 应急小组
        /// </summary>
        public List<EmergencyTeam> EmergencyTeam
        {
            get; set;
        }

        /// <summary>
        /// 备勤组
        /// </summary>
        public List<Reservegroup> Reservegroup
        {
            get; set;
        }


        /// <summary>
        /// 是否已修改
        /// </summary>
        public bool IsCancel
        {
            get;set;
        }
    }
}
