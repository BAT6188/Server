using Infrastructure.Model;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resources.Model
{
    /// <summary>
    /// 设备状态历史记录
    /// </summary>
    public class DeviceStatusHistory
    {
        public Guid DeviceStatusHistoryId
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
        /// 设备状态
        /// </summary>
        public Guid StatusId
        {
            get;set;
        }

        public SystemOption Status
        {
            get;set;
        }

        /// <summary>
        /// 状态记录时间
        /// </summary>
        public DateTime CreateTime
        {
            get;set;
        }
    }
}
