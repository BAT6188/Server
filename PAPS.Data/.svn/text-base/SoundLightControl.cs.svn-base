using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAPS.Data
{
    /// <summary>
    /// 声光报警联动动作
    /// </summary>
    public class SoundLightControl
    {
        public Guid DeviceId { get; set; }

        public int DeviceCode { get; set; }

        public string Message { get; set; }

        /// <summary>
        /// 闪灯掩码 
        /// 0x04        红灯** 暴
        ///0x08        黄灯** 逃
        /// 0x10        蓝灯** 袭
       ///  0x20        绿灯** 灾
       ///  0x80 全亮
        /// </summary>
        public int Ledbitmask { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Audio { get; set; }

        public int SentinelCode { get; set; }
    }
}
