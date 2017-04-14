using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAPS.Data
{
    public class PushMessage
    {
        /// <summary>
        /// 设备guid
        /// </summary>
        public Guid DeviceId { get; set; }

        public int DeviceCode { get; set; }

        public string Message { get; set; }

    }
}
