using Surveillance.ViewModel.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Surveillance.ViewModel
{
    /// <summary>
    /// 录像按时间回放/下载参数
    /// </summary>
    public class RecordPlayByTime
    {
        public RecordFileType RecordType { get; set; }

        public Guid? AlarmType { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
