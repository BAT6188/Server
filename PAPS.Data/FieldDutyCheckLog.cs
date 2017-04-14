using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAPS.Data
{
    /// <summary>
    /// 实地查勤记录
    /// </summary>
    public class FieldDutyCheckLog
    {
        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizationName { get; set; }

        /// <summary>
        /// 计划查勤时间
        /// </summary>
        public string PlanDutyCheckTime { get; set; }

        /// <summary>
        /// 计划查勤人
        /// </summary>
        public string PlanDutyCheckStaffName { get; set; }


        /// <summary>
        /// 哨位名称
        /// </summary>
        public string SentinelName { get; set; }

        /// <summary>
        /// 实际查勤时间
        /// </summary>
        public string DutyCheckTime { get; set; }


        /// <summary>
        /// 实际查勤人
        /// </summary>
        public string DutyCheckStaffName { get; set; }


        /// <summary>
        /// 查勤评定
        /// </summary>
        public string Appriase { get; set; }


        /// <summary>
        /// 查勤状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 附件路径
        /// </summary>
        public string AttachmentPath { get; set; }
    }
}
