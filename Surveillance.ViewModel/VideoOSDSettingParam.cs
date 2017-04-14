using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveillance.ViewModel
{
    /// <summary>
    /// 视频播放osd设置参数
    /// </summary>
    class VideoOSDSettingParam : OSDSettingParam
    {
        /// <summary>
        /// 视频请求返回句柄
        /// </summary>
        public int Handle
        {
            get; set;
        }
    }
}
