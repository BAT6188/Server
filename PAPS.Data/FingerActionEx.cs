using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAPS.Data
{
    /// <summary>
    /// 精简的指纹下发动作实体
    /// </summary>
    public class FingerActionEx
    {
        /// <summary>
        /// 人员id
        /// </summary>
        public List<Guid> StaffIds { get; set; }

        /// <summary>
        /// 哨位id
        /// </summary>
        public List<Guid> SentinelIds { get; set; }
    }
}
