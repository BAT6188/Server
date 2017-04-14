using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Model
{

    /// <summary>
    /// 时间周期额外属性
    /// </summary>
    public class TimePeriodExtra
    {
        /// <summary>
        /// 值类型(绝对值：0，百分比：1)
        /// </summary>
        public int ValueType { get; set; }

        /// <summary>
        /// 绝对值
        /// </summary>
        public int AbsoluteValue { get; set; }

        /// <summary>
        /// 百分比
        /// </summary>
        public double PercentValue { get; set; }

    }
}
