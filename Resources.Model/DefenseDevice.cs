using Infrastructure.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resources.Model
{
    /// <summary>
    /// 防区设备，【红外，高压，栅栏】
    /// </summary>
    public class DefenseDevice
    {
        public Guid DefenseDeviceId { get; set; }

        public Guid DeviceInfoId { get; set; }

        public  virtual IPDeviceInfo DeviceInfo { get; set; }

        public Guid SentinelId { get; set; }

        [JsonIgnore]
        public virtual Sentinel Sentinel { get; set; }

        /// <summary>
        /// 防区编号
        /// </summary>
        public int DefenseNo { get; set; }

        /// <summary>
        /// 报警输入开关编号
        /// </summary>
        public int AlarmIn { get; set; }

        /// <summary>
        /// 报警输出开关编号
        /// </summary>
        public int AlarmOut { get; set; }

        /// <summary>
        /// 报警输入是否常开
        /// </summary>
        public bool AlarmInNormalOpen { get; set; }

        public Guid? DefenseDirectionId { get; set; }

        /// <summary>
        /// 防区方向
        /// </summary>
        public SystemOption DefenseDirection { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
    }
}
