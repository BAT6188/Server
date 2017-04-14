using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAPS.Data
{
    /// <summary>
    /// 哨位截图消息事件
    /// </summary>
    public class SentinelSnapshotEvent
    {
        public Guid EventSourceId { get; set; }

        public SentinelSnapshotEventType EventType { get; set; }

        /// <summary>
        /// 截图
        /// </summary>
        public Attachment Snapshot{ get; set; }

        /// <summary>
        /// 截图视频
        /// </summary>
        public SentinelSnapshotVideo SanpshotVideo { get; set; }
    }
}
