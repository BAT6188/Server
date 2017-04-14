using Infrastructure.Model;
using Resources.Model;
using System;
using System.Collections.Generic;

namespace PAPS.Model
{
    /// <summary>
    /// 通报
    /// </summary>
    public class Circular
    {
        /// <summary>
        /// 通报ID
        /// </summary>
        public Guid CircularId
        {
            get;set;
        }

        /// <summary>
        /// 查勤结果ID
        /// </summary>
        public Guid? DutyCheckLogId
        {
            get; set;
        }

        /// <summary>
        /// 查勤结果
        /// </summary>
        public virtual DutyCheckLog DutyCheckLog
        {
            get;set;
        }



        /// <summary>
        /// 通报人
        /// </summary>
        public Guid CircularStaffId
        {
            get; set;
        }


        /// <summary>
        /// 通报人
        /// </summary>
        public virtual Staff CircularStaff
        {
            get;set;
        }

        /// <summary>
        /// 通报时间
        /// </summary>
        public DateTime CircularTime
        {
            get;set;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get;set;
        }
    }
}