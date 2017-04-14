using Infrastructure.Model;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resources.Model
{
    /// <summary>
    /// 报警外设
    /// </summary>
    public class AlarmPeripheral
    {
        /// <summary>
        /// PK,设备id
        /// </summary>
        public Guid AlarmPeripheralId 
        {
            get; set;
        }

        /// <summary>
        /// 接入报警主机的外设
        /// 基于报警配置和预案，报警源和预案动作的主体都是IPDevice object而增加的字段，从而间接建立与报警配置的关系
        /// AlarmPeripheral->IPDevice
        /// </summary>
        public Guid AlarmDeviceId
        {
            get;set;
        }
        ///// <summary>
        ///// 外接设备名称
        ///// </summary>
        //[MaxLength(64)]
        //public string AlarmPeripheralName
        //{
        //    get;set;
        //}

        ///// <summary>
        ///// 报警接入主机Id
        ///// </summary>
        //public Guid AlarmMainframeInfoId
        //{
        //    get;set;
        //}

        ///// <summary>
        ///// 报警接入主机[编码器/报警主机...]
        ///// </summary>
        //public virtual IPDeviceInfo AlarmMainframeInfo
        //{
        //    get; set;
        //}

        /// <summary>
        /// 报警接入主机Id
        /// </summary>
        public Guid? AlarmMainframeId
        {
            get; set;
        }

        /// <summary>
        /// 编码器id
        /// </summary>
        public Guid? EncoderId
        {
            get;set;
        }

        ///// <summary>
        ///// 报警接入主机[编码器/报警主机...]
        ///// </summary>
        //[JsonIgnore]
        //public virtual AlarmMainframe AlarmMainframe
        //{
        //    get; set;
        //}

        public virtual IPDeviceInfo AlarmDevice
        {
            get;set;
        }

        /// <summary>
        /// 报警类型Id
        /// </summary>
        public Guid AlarmTypeId
        {
            get;set;
        }

        /// <summary>
        /// 报警类型
        /// </summary>
        public virtual SystemOption AlarmType
        {
            get; set;
        }

        /// <summary>
        /// 报警接入通道
        /// </summary>
        public int AlarmChannel
        {
            get; set;
        }

        /// <summary>
        /// 防区编号
        /// </summary>
        public int DefendArea
        {
            get; set;
        }              
    }
}
