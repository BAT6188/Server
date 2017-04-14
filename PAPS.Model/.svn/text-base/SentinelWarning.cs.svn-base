using Infrastructure.Model;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAPS.Model
{
    /// <summary>
    /// 哨位警告
    /// </summary>
    public class SentinelWarning
    {
        public Guid SentinelWarningId { get; set; }

        public Guid DeviceInfoId { get; set; }

        /// <summary>
        /// 告警设备
        /// </summary>
        public virtual IPDeviceInfo DeviceInfo { get; set; }

        public DateTime CreatedTime { get; set; }

        public Guid WarningTypeId { get; set; }

        public virtual SystemOption WarningType { get; set; }

        /// <summary>
        /// 警告开/关
        /// </summary>
        public bool Open { get; set; }

    }
}
