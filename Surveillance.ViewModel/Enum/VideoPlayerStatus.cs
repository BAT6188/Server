using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveillance.ViewModel.Enum
{
    /// <summary>
    /// 播放状态
    /// </summary>
    public enum VideoPlayerStatus : int
    {
        /// <summary>
        /// 不操作
        /// </summary>
        None = 0,
        /// <summary>
        /// 实时播放
        /// </summary>
        RealPlay = 1,
        /// <summary>
        /// 录像播放
        /// </summary>
        RecordPlay = 2,
        /// <summary>
        /// 本地录像
        /// </summary>
        LocalRecord = 4,
        /// <summary>
        /// 录像下载
        /// </summary>
        RecordDownload = 8,
        /// <summary>
        /// 视频上墙。
        /// </summary>
        VideoToTV = 16,
        /// <summary>
        /// 录像暂停
        /// </summary>
        RecordPlayPause = 32,
        /// <summary>
        /// 录像变速播放状态。
        /// </summary>
        RecordRatePlay = 64,
        /// <summary>
        /// 转码状态。
        /// </summary>
        TransCode = 128,
    }
}
