using System;
using System.Collections.Generic;

namespace PAPS.Model
{
    public class DutyCheckGroup
    { 
        public Guid DutyCheckGroupId { get; set; }
        /// <summary>
        /// 检查组名
        /// </summary>
        public string DutyGroupName
        {
            get;set;
        }

        /// <summary>
        /// 检查点人员安排明细
        /// </summary>
        public List<DutyCheckSiteSchedule> CheckDutySiteSchedules
        {
            get;set;
        }
    }
}
