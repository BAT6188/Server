using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Resources.Model;

namespace TaskServer.Argument
{
    /// <summary>
    /// 视频播放参数
    /// </summary>
    public class RealVideoPlayArgument
    {
        /// <summary>
        /// 摄像机id -需关联MonitorySite去查找播放参数
        /// </summary>
        public Guid VideoDeviceId
        {
            get; set;
        }

        /// <summary>
        /// 码流类型
        /// </summary>
        public int StreamType
        {
            get; set;
        }

        /// <summary>
        /// 轮巡时长
        /// </summary>
        public int RoundInterval
        {
            get;set;
        }

        /// <summary>
        /// 预置点
        /// </summary>
        public int PresetSiteNo
        {
            get;set;
        }

        /// <summary>
        /// 是否截图
        /// </summary>
        public bool ScreenShot
        {
            get;set;
        }
    }
}
