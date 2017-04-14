using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAPS.Data
{
    public class DeviceVersion
    {
        /// <summary>
        /// 设备guid
        /// </summary>
        public Guid DeviceId { get; set; }

        public uint SentinelCode { get; set; }

        public string Version { get; set; }

    }
}
