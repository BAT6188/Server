using Infrastructure.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace PAPS.Model
{
    /// <summary>
    /// 查勤检查安排
    /// </summary>
    public class InstitutionsDutyCheckSchedule
    {
        /// <summary>
        /// 查勤安排ID
        /// </summary>
        public Guid InstitutionsDutyCheckScheduleId
        {
            get;set;
        }

        /// <summary>
        /// 受检组织ID
        /// </summary>
        public Guid InspectedOrganizationId
        {
            get; set;
        }

        /// <summary>
        /// 受检组织
        /// </summary>
        public virtual Organization InspectedOrganization
        {
            get;set;
        }

        /// <summary>
        /// 主要负责人ID
        /// </summary>
        public Guid LeadId
        {
            get; set;
        }

        /// <summary>
        /// 主要负责人
        /// </summary>
        public virtual Staff Lead
        {
            get;set;
        }

        /// <summary>
        ///  随行人员集合实际保存数据
        /// </summary>
        public string EntouragesJson
        {
            get; set;
        }


        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime
        {
            get;set;
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime
        {
            get;set;
        }


        /// <summary>
        ///  检查目标集合实际保存数据
        /// </summary>
        public string InspectionTargetJson
        {
            get; set;
        }


        /// <summary>
        /// 检查重点
        /// </summary>
        public string InspectionKey
        {
            get;set;
        }
    }
}