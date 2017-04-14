using Infrastructure.Model;
using System;

namespace PAPS.Model
{
    /// <summary>
    /// 值班交接记录
    /// </summary>
    public class ShiftHandoverLog
    {
        /// <summary>
        /// 交接班记录ID
        /// </summary>
        public Guid ShiftHandoverLogId
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
        /// 交班时间
        /// </summary>
        public System.DateTime HandoverTime
        {
            get;set;
        }

        /// <summary>
        /// 交班人ID
        /// </summary>
        public Guid OffGoingId
        {
            get; set;
        }

        /// <summary>
        /// 交班人
        /// </summary>
        public virtual Staff OffGoing
        {
            get;set;
        }

        /// <summary>
        /// 接班人ID
        /// </summary>
        public Guid OnComingId
        {
            get; set;
        }

        /// <summary>
        /// 接班人
        /// </summary>
        public virtual Staff OnComing
        {
            get;set;
        }

        /// <summary>
        /// 交班日期
        /// </summary>
        public DateTime HandoverDate
        {
            get;set;
        }


        /// <summary>
        /// 交班状态ID
        /// </summary>
        public Guid? StatusId
        {
            get;set;
        }

        /// <summary>
        /// 交班状态
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