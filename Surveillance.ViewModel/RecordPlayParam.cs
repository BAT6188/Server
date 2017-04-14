using Surveillance.ViewModel.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Surveillance.ViewModel
{
    /// <summary>
    /// 录像播放参数，同样适用于下载
    /// </summary>
    public class RecordPlayParam : VideoPlayParam
    {

        public RecordPlayType PlayType { get; set; }

        /// <summary>
        /// 回放信息,本地文件回放，传录像文件路径即可
        /// 按时间回放/下载，对应的RecordInfo 是RecordPlayByTime对象
        /// 按文件回放/下载，对应的RecordInfo是RecordFile对象
        /// 按Ftp回放/下载，对应的RecordInfo是RecordPlayByFtp
        /// </summary>
        public object RecordInfo { get; set; }
    }
}
