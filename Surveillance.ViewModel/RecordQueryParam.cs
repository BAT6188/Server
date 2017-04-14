using Surveillance.ViewModel.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveillance.ViewModel
{
    /// <summary>
    /// 录像文件检索参数
    /// </summary>
    public class RecordQueryParam
    {
        /// <summary>
        /// 录像文件类型
        /// </summary>
        public int RecordType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间，适用于
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 报警类型
        /// </summary>
        public Guid? AlarmType { get; set; }

        /// <summary>
        /// 报警事件id
        /// </summary>
        public Guid? AlarmID { get; set; }
    }
}
