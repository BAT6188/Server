using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resources.Model
{
    /// <summary>
    /// 设备通道配置
    /// 考虑到哨位台输入/输出通道多个，通道名称可变
    /// </summary>
    public class DeviceChannelSetting
    {
        public Guid DeviceChannelSettingId { get; set; }

        /// <summary>
        /// 通道号
        /// </summary>
        public int ChannelNo { get; set; }

        /// <summary>
        /// 通道类型
        /// </summary>
        public Guid ChannelTypeId { get; set; }

        public virtual SystemOption ChannelType { get; set; }
    }
}
