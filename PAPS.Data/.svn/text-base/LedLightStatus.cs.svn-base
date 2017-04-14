using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAPS.Data
{
    /// <summary>
    /// 声光报警器LED灯闪烁状态
    /// </summary>
    public class LedLightStatus
    {
        /// <summary>
        /// 设备guid
        /// </summary>
        public Guid DeviceId { get; set; }

        public int DeviceCode { get; set; }

        /// <summary>
        /// 多种灯同时闪烁，按位 |或 操作
        /// 0x00 -关闭所有灯 ,0x01 -红     0x02- 黄        0x04-蓝          0x08-绿
        /// </summary>
        public int Status { get; set; }
    }
}
