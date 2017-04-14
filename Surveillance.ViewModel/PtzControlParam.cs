using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Surveillance.ViewModel.Enum;

namespace Surveillance.ViewModel
{
    /// <summary>
    /// 云台控制参数
    /// </summary>
    public class PtzControlParam : VideoPlayParam
    {
        /// <summary>
        /// 云台动作控制
        /// </summary>
        public int Command
        { get; set; }

        public int Value { get; set; }

        /// <summary>
        /// 控制速度  
        /// </summary>
        public int Speed { get; set; }

        ///// <summary>
        ///// 巡航组号，在自动巡航时传参
        ///// </summary>
        //public int CruiseScanGroupIndex
        //{
        //    get;set;
        //}
        public int Param { get; set; }

        public int StreamType { get; set; }
    }
}
