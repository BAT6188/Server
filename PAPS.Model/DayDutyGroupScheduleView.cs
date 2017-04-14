using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAPS.Model
{
    /// <summary>
    /// 勤务分组--日排程
    /// </summary>
    public class DayDutyGroupScheduleView
    {
        public Guid DutyCheckLogId
        {
            get; set;
        }

        public string TimeInterval
        {
            get; set;
        }

        public string Sentinel
        {
            get; set;
        }

        public string StaffName
        {
            get; set;
        }

        public int OrderNo
        {
            get; set;
        }
    }
}
