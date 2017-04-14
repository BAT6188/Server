using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskServer.Argument
{
    public class SoundLightArgument
    {
        /// <summary>
        /// 声光报警设备id
        /// </summary>
        public Guid SoundLightDeviceId { get; set; }

        /// <summary>
        /// 是否推送文字
        /// </summary>
        public bool PushMessage { get; set; }

        /// <summary>
        /// 推送内容
        /// </summary>
        public string Message
        {
            get; set;
        }

        /// <summary>
        /// 是否语言播报
        /// </summary>
        public bool AudioAction { get; set; }

        /// <summary>
        /// 语音播报文件
        /// </summary>
        public string AudioFile { get; set; }

        /// <summary>
        /// 是否开启警灯
        /// </summary>
        public bool TurnonLightAction { get; set; }
    }
}
