using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PAPS.Model
{
    /// <summary>
    /// 分队-查勤安排表明细
    /// </summary>
    public class UnitDutyCheckScheduleDetail
    {

        /// <summary>
        /// 分队-查勤安排表明细--明细ID
        /// </summary>
        [Key]
        public Guid UnitDutyCheckScheduleDetailId
        {
            get; set;
        }

        /// <summary>
        /// 查勤员ID
        /// </summary>
        public Guid CheckManId
        {
            get; set;
        }

        /// <summary>
        /// 查勤员
        /// </summary>
        public virtual Staff CheckMan
        {
            get; set;
        }


        /// <summary>
        /// 日期
        /// </summary>
        public string Data
        {
            get;set;
        }

        /// <summary>
        ///  时间段ID
        /// </summary>
        public Guid TimePeriodId
        {
            get;set;
        }
    }
}
