using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveillance.Enum
{
    /// <summary>
    /// 回放控制
    /// </summary>
    public enum RecordPlayControlFlag : int
    {
        /// <summary>
        /// 快进
        /// </summary>
        FastForward,
        /// <summary>
        /// 慢进
        /// </summary>
        SlowDown,
        /// <summary>
        /// 暂停播放
        /// </summary>
        Pause,
        /// <summary>
        /// 恢复播放
        /// </summary>
        Resume,
        /// <summary>
        /// 录像正常播放
        /// </summary>
        Noraml,							
        /// <summary>
        /// 录像单帧前进
        /// </summary>
        FrameForword,						
        /// <summary>
        /// 录像单帧后退
        /// </summary>
        FrameBack,							
        /// <summary>
        /// 设置录像播放进度
        /// </summary>
        SetPlayProgress,                  
        /// <summary>
        /// 获取录像播放进度
        /// </summary>
        GetPlayProgress,					
        /// <summary>
        /// 获取远程录像缓冲进度
        /// </summary>
        GetBufferProgress	
    }
}
