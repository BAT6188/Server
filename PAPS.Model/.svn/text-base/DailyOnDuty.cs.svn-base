using Infrastructure.Model;
using System;

namespace PAPS.Model
{
    /// <summary>
    /// 值班日报
    /// </summary>
    public class DailyOnDuty
    {
        /// <summary>
        /// 值班日报ID
        /// </summary>
        public Guid DailyOnDutyId
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
        /// 日期
        /// </summary>
        public DateTime DutyDate
        {
            get;set;
        }


        /// <summary>
        /// 今天值班员ID
        /// </summary>
        public Guid DutyOfficerTodayId
        {
            get; set;
        }

        /// <summary>
        /// 今天值班员
        /// </summary>
        public virtual Staff DutyOfficerToday
        {
            get;set;
        }

        /// <summary>
        /// 明日值班员ID
        /// </summary>
        public Guid TomorrowAttendantId
        {
            get; set;
        }

        /// <summary>
        /// 明日值班员
        /// </summary>
        public virtual Staff TomorrowAttendant
        {
            get;set;
        }

        /// <summary>
        /// 实力数
        /// </summary>
        public int StrengthNumber
        {
            get;set;
        }

        /// <summary>
        /// 在位数
        /// </summary>
        public int InNumber
        {
            get;set;
        }

        /// <summary>
        /// 状态ID
        /// </summary>
        public Guid? StatusId
        {
            get;set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public virtual SystemOption Status
        {
            get;set;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}