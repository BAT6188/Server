using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resources.Model
{
    /// <summary>
    /// 报警主机实体
    /// </summary>
    public class AlarmMainframe
    {
        public Guid AlarmMainframeId
        {
            get;set;
        }

        public Guid DeviceInfoId
        {
            get;set;
        }

        public virtual IPDeviceInfo DeviceInfo
        {
            get;set;
        }

        /// <summary>
        /// 报警外接设备
        /// </summary>
        public List<AlarmPeripheral> AlarmPeripherals
        {
            get;set;
        }
    }
}
