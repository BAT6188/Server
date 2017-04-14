using AlarmAndPlan.Model;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskServer
{
    /// <summary>
    /// 预案动作数据封装
    /// </summary>
    internal class PlanActionArgument
    {
        public IPDeviceInfo DeviceInfo
        {
            get; set;
        }

        /// <summary>
        /// 运行参数
        /// </summary>
        public string  Argument
        {
            get; set;
        }

        /// <summary>
        /// 预案动作编号
        /// </summary>
        public string ActionCode
        {
            get;set;
        }

        ///// <summary>
        ///// 报警id[可能是Empty]
        ///// </summary>
        //public Guid AlarmLogId
        //{
        //    get;set;
        //}
    }
}
