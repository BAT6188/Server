using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveillance.ViewModel.Enum
{
    /// <summary>
    /// 录像文件类型
    /// </summary>
    public enum RecordFileType
    {
        All = -1,
        /// <summary>
        /// 无效的查询
        /// </summary>
        None = 0,
        /// <summary>
        /// 循环录像策略（天、周、月）
        /// </summary>
        Cycle = 1,
        /// <summary>
        /// 计划录像策略
        /// </summary>
        Plan = 2,
        /// <summary>
        /// 手动录像策略
        /// </summary>
        Manual = 3,
        /// <summary>
        /// 报警录像策略
        /// </summary>
        Alarm = 4,
    }
}
