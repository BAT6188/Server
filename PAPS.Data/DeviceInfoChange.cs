using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAPS.Data
{
    /// <summary>
    /// 设备信息变更通知
    /// </summary>
    public class DeviceInfoChange
    {
        /// <summary>
        /// 设备guid
        /// </summary>
        public Guid DeviceId { get; set; }

        public Guid DevicetypeId { get; set; }
    

        /// <summary>
        /// 设备编号
        /// </summary>
        public int DeviceCode { get; set; }

        /// <summary>
        /// 操作类型, 0:删除，1：更新： 2：新增
        /// </summary>
        public int Operater { get; set; }

    }
}
