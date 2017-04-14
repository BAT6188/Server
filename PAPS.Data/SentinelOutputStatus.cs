using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAPS.Data
{
    /// <summary>
    /// 哨位台输出端口状态
    /// </summary>
    public class SentinelOutputStatus
    {
        /// <summary>
        /// 设备guid
        /// </summary>
        public Guid DeviceId { get; set; }

        public int SentinelCode { get; set; }

        public int Channel { get; set; }

        /// <summary>
        /// 外设状态：0：断开，1：闭合
        /// </summary>
        public int Status { get; set; }
    }
}
