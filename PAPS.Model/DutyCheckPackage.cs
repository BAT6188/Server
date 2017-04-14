using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAPS.Model
{
    /// <summary>
    /// 查勤包
    /// </summary>
    public class DutyCheckPackage
    {
        /// <summary>
        /// 查勤包ID
        /// </summary>
        public Guid DutyCheckPackageId
        {
            get;set;
        }

        /// <summary>
        /// 查勤包明细
        /// </summary>
        public List<DutyCheckPackageLog> DutyCheckPackLogs
        {
            get;set;
        }

        /// <summary>
        /// 查勤开始时间
        /// </summary>
        public DateTime StartTime
        {
            get;set;
        }

        /// <summary>
        /// 查勤结束时间
        /// </summary>
        public System.DateTime EndTime
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
        /// 查勤包状态ID
        /// </summary>
        public Guid PackageStatusId
        {
            get;set;
        }


        /// <summary>
        /// 查勤包状态
        /// </summary>
        public virtual SystemOption PackageStatus
        {
            get;set;
        }
    }
}