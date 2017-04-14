using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAPS.Model
{
    /// <summary>
    /// 值班安排表明细
    /// </summary>
    public class DutyScheduleDetail
    {

        public Guid DutyScheduleDetailId
        {
            get;set;
        }


        /// <summary>
        /// 检查日期
        /// </summary>
        public DateTime CheckDay
        {
            get; set;
        }



        /// <summary>
        /// 首长排程ID
        /// </summary>
        public Guid CadreScheduleId { get; set; }

        /// <summary>
        /// 首长排程
        /// </summary>
        public virtual DutyCheckSchedule CadreSchedule { get; set; }

        /// <summary>
        /// 值班员排程ID
        /// </summary>
        public Guid OfficerScheduleId { get; set; }

        /// <summary>
        /// 值班员排程
        /// </summary>
        public virtual DutyCheckSchedule OfficerSchedule { get; set; }


        /// <summary>
        /// 网络查勤员排程
        /// </summary>
        public List<DutyCheckSchedule> NetWatcherSchedule { get; set; }
    }
}
