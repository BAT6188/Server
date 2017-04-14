using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PAPS.Model
{
    /// <summary>
    /// 分队勤务编组安排表--明细
    /// </summary>
    public class DutyGroupScheduleDetail
    {
        /// <summary>
        /// 分队勤务编组安排表--明细ID
        /// </summary>
        [Key]
        public Guid DutyGroupScheduleDetailId
        {
            get;set;
        }

        /// <summary>
        /// 排序编号
        /// </summary>
        public int OrderNo
        {
            get;set;
        }


        /// <summary>
        /// 网络查勤员ID
        /// </summary>
        public Guid CheckManId
        {
            get; set;
        }

        /// <summary>
        /// 网络查勤员
        /// </summary>
        public virtual Staff CheckMan
        {
            get; set;
        }


        /// <summary>
        /// 哨位明细
        /// </summary>
        public List<DutyCheckSiteSchedule> CheckDutySiteSchedule
        {
            get; set;
        }


    }
}
