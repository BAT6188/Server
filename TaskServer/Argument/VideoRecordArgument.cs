using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskServer.Argument
{
    public class VideoRecordArgument
    {
        /// <summary>
        /// 视频设备id
        /// </summary>
        public Guid VideoDeviceId
        {
            get;set;
        }

        /// <summary>
        /// 存储服务器Id
        /// </summary>
        public Guid? VideoStorageServerId
        {
            get;set;
        }

        /// <summary>
        /// 录像时长，单位：min
        /// </summary>
        public int RecordTimeout
        {
            get;set;
        }

        /// <summary>
        /// 码流类型
        /// </summary>
        public int StreamType
        {
            get;set;
        }
    }
}
