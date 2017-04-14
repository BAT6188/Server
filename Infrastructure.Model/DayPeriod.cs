using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Model
{
    /// <summary>
    /// 月/周周期里面的天，可允许时间不同
    /// </summary>
    public class DayPeriod
    {
        public Guid DayPeriodId { get; set; }

        /// <summary>
        /// 时间段
        /// </summary>
        public List<TimePeriod> TimePeriods { get; set; }

        public int Day { get; set; }

        public int DayOfWeek { get; set; }
    }
}
