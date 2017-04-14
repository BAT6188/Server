using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Surveillance.ViewModel
{
    /// <summary>
    /// 语音对讲参数
    /// </summary>
    class VoiceTalkParam
    {
        /// <summary>
        /// 对讲设备
        /// </summary>
        public Encoder EncoderDevice { get; set; }

        /// <summary>
        /// 值引用，对讲成功后返回的句柄值
        /// </summary>
        public int Handle { get; set; }
    }
}
