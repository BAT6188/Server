using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAPS.Data
{
    public class DeviceAlarmStatus
    {
        public Guid DeviceId { get; set; }

        //public Guid SentinelType { get; set; }

        public Guid AlarmType { get; set; }

        public int SentinelCode { get; set; }

        /// <summary>
        /// 报警状态 true：报警，false：取消
        /// </summary>
        public bool AlarmStatus { get; set; }

        /// <summary>
        /// 可能是哨位设备编号/防区设备编号  若为暴逃袭灾，DeviceCode和SentinelCode相同
        /// </summary>
        public int DeviceCode { get; set; }
    }

}
