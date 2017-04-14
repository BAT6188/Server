using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Surveillance.ViewModel
{
    /// <summary>
    /// 巡航预置点
    /// </summary>
    public class PTZPreset
    {
        /// <summary>
        /// 预置点编号
        /// </summary>
        public int Preset { get; set; }

        /// <summary>
        /// 巡航时长
        /// </summary>
        public int Time {get;set;}
    }
}
