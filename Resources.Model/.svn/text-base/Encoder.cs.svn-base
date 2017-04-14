/**
 * 2016-12-26 zhrx  调整编码器和报警外设关系
 */ 
using System;
using System.Collections.Generic;

namespace Resources.Model
{
    /// <summary>
    /// 编码器设备实体
    /// </summary>
    public class Encoder
    {
        /// <summary>
        /// 编码器Id
        /// </summary>
        public Guid EncoderId
        {
            get;set;
        }

        public Guid DeviceInfoId
        {
            get;set;
        }

        /// <summary>
        /// 编码器信息
        /// </summary>
        public virtual IPDeviceInfo DeviceInfo
        {
            get;set;
        }

        public Guid EncoderTypeId
        {
            get;set;
        }

        /// <summary>
        /// 编码器设备型号
        /// </summary>
        public virtual EncoderType EncoderType
        {
            get;set;
        }

        /// <summary>
        /// 报警外接设备
        /// </summary>
        public List<AlarmPeripheral> AlarmPeripherals
        {
            get; set;
        }

    }
}
