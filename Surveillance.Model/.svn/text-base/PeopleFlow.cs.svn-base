using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Surveillance.Model
{
    /// <summary>
    /// 客流量
    /// </summary>
    public class PeopleFlow
    {
        public Guid PeopleFlowId { get; set; }

        /**
         * 统计设备id
         */
        public Guid DeviceInfoId { get; set; }

        public virtual IPDeviceInfo DeviceInfo { get; set; }

        public  DateTime CreateTime { get; set; }

        /// <summary>
        /// 进入人数
        /// </summary>

        public  int EntryCount { get; set; }

        /// <summary>
        /// 离开人数
        /// </summary>
        public int LeaveCount { get; set; }

    }
}
