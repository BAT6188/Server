using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resources.Model
{
    /// <summary>
    /// 设备关联的通道选项
    /// </summary>
    public class DeviceChannelTypeMapping
    {
        public Guid DeviceChannelTypeMappingId { get; set; }

        public Guid DeviceTypeId { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public virtual SystemOption DeviceType { get; set; }

        public Guid ChannelTypeId { get; set; }

        /// <summary>
        /// 通道类型
        /// </summary>
        public virtual SystemOption ChannelType { get; set; }
    }
}
