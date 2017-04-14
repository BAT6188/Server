using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAPS.Data
{
    /// <summary>
    /// 哨位事件截图
    /// </summary>
    public enum SentinelSnapshotEventType
    {
        /// <summary>
        /// 供弹事件
        /// </summary>
        BulletboxLog,
        /// <summary>
        /// 查哨换岗
        /// </summary>
        PunchLog
    }
}
