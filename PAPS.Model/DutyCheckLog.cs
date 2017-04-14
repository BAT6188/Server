using Infrastructure.Model;
using Newtonsoft.Json;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PAPS.Model
{
    /// <summary>
    /// 查勤日志
    /// </summary>
    public class DutyCheckLog
    {

        /// <summary>
        /// 考勤记录ID
        /// </summary>
        public Guid DutyCheckLogId
        {
            get; set;
        }

        /// <summary>
        /// 查勤人员ID
        /// </summary>
        public Guid? DutyCheckStaffId
        {
            get; set;
        }

        /// <summary>
        /// 查勤人员
        /// </summary>
        public virtual Staff DutyCheckStaff
        {
            get; set;
        }

        /// <summary>
        /// 实际执勤人员ID
        /// </summary>
        public Guid? DutyCheckSiteScheduleId
        {
            get; set;
        }


        /// <summary>
        /// 实际执勤人员
        /// </summary>
        public virtual DutyCheckSiteSchedule DutyCheckSiteSchedule
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
            get; set;
        }

        /// <summary>
        /// 时间区间Json
        /// </summary>
        public string TimePeriodJson
        {
            get; set;
        }

        /// <summary>
        /// 时间区间
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public virtual TimePeriod TimePeriod
        {
            get
            {
                if (string.IsNullOrEmpty(TimePeriodJson))
                    return null;
                return JsonConvert.DeserializeObject<TimePeriod>(TimePeriodJson);
            }
            set
            {
                TimePeriodJson = JsonConvert.SerializeObject(value);
            }
        }


        /// <summary>
        /// 计划查勤日期
        /// </summary>
        public DateTime? PlanDate
        {
            get;set;
        }

        /// <summary>
        /// 实际查勤时间
        /// </summary>
        public DateTime? RecordTime
        {
            get; set;
        }

        /// <summary>
        /// 状态ID
        /// </summary>
        public Guid? StatusId
        {
            get; set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public virtual SystemOption Status
        {
            get; set;
        }

        /// <summary>
        /// 总体评价ID
        /// </summary>
        public Guid? MainAppriseId
        {
            get; set;
        }

        /// <summary>
        /// 总体评价
        /// </summary>
        public virtual SystemOption MainApprise
        {
            get; set;
        }

        /// <summary>
        /// 评价集合
        /// </summary>
        public List<DutyCheckLogAppraise> Apprises
        {
            get; set;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get; set;
        }

        /// <summary>
        /// 记录类型ID(区分同级中实地查勤和网络查勤)
        /// </summary>
        public Guid RecordTypeId
        {
            get;set;
        }

        /// <summary>
        /// 记录类型(区分同级中实地查勤和网络查勤)
        /// </summary>
        public virtual SystemOption RecordType
        {
            get; set;
        }

        /// <summary>
        /// 查勤操作ID
        /// </summary>
        public Guid? DutyCheckOperationId
        {
            get; set;
        }

        /// <summary>
        /// 查勤操作
        /// </summary>
        public virtual DutyCheckOperation DutyCheckOperation
        {
            get;set;
        }


        /// <summary>
        /// 查勤处理方式
        /// </summary>
        public List<DutyCheckLogDispose> CircularTypes
        {
            get;set;
        }


        /// <summary>
        /// 日排程ID
        /// </summary>
        public Guid? DayPeriodId
        {
            get;set;
        }


        /// <summary>
        /// 日排程
        /// </summary>
        public virtual DayPeriod DayPeriod
        {
            get;set;
        }


        /// <summary>
        /// 查勤点名称（可能会发生变化）
        /// </summary>
        public string DutycheckSiteName
        {
            get; set;
        }

        /// <summary>
        /// 查勤点GUID
        /// </summary>
        public Guid? DutycheckSiteId
        {
            get;set;
        }

        /// <summary>
        /// 查勤评价截图Id
        /// </summary>
        public Guid? AppraiseICOId
        {
            get; set;
        }

        /// <summary>
        /// 查勤评价截图（差评时截图）
        /// </summary>
        public Attachment AppraiseICO
        {
            get;set;
        }
    }
}