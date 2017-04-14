using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAPS.Model
{
    /// <summary>
    /// 分队-查勤安排表
    /// </summary>
    public class UnitDutyCheckSchedule
    {
        /// <summary>
        /// 分队-查勤安排表ID
        /// </summary>
        public Guid UnitDutyCheckScheduleId
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
        /// 分队-查勤安排表周期ID
        /// </summary>
        public Guid ScheduleId
        {
            get; set;
        }

        /// <summary>
        /// 分队-查勤安排表周期
        /// </summary>
        public virtual Schedule Schedule
        {
            get; set;
        }


        /// <summary>
        /// 分队-查勤安排表明细
        /// </summary>
        public List<UnitDutyCheckScheduleDetail> UnitDutyCheckScheduleDetails
        {
            get; set;
        }

        /// <summary>
        /// 是否已修改
        /// </summary>
        public bool IsCancel
        {
            get; set;
        }
    }
}
