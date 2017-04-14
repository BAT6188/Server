using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAPS.Data
{
    public class DutyDetail
    {
        /// <summary>
        /// 哨位ID
        /// </summary>
        public Guid Sentinelid
        {
            get;set;
        }

        /// <summary>
        /// 接班人
        /// </summary>
        public Staff OnDutyStaff { get; set; }

        /// <summary>
        /// 交班人
        /// </summary>
        public Staff OffDutyStaff { get; set; }
    }
}
