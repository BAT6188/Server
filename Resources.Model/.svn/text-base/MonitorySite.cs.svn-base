using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resources.Model
{
    /// <summary>
    /// 监控点实体
    /// </summary>
    public class MonitorySite
    {
        /// <summary>
        /// 监控点Id
        /// </summary>
        public Guid MonitorySiteId
        {
            get; set;
        }

        /// <summary>
        /// 监控点名称
        /// </summary>
        [MaxLength(64)]
        public string MonitorySiteName
        {
            get; set;
        }

        /// <summary>
        /// 安装地址
        /// </summary>
        public string InstallAddress
        {
            get;set;
        }

        /// <summary>
        /// 是否激活启用
        /// </summary>
        public bool IsActive
        {
            get;set;
        }

        /// <summary>
        /// 对讲编号(与IP广播设备关联)
        /// </summary>
        public int Phone
        {
            get;set;
        }

        /// <summary>
        /// 自定义序号
        /// </summary>
        public int OrderNo
        {
            get; set;
        }

        //public Guid VideoForwardId
        //{
        //    get; set;
        //}

        //public Guid EncoderId
        //{
        //    get;set;
        //}

        public Guid OrganizationId
        {
            get;set;
        }

        //public Guid CameraTypeId
        //{
        //    get; set;
        //}

        public Guid CameraId
        {
            get; set;
        }

        /// <summary>
        /// 摄像机
        /// </summary>
        public virtual Camera Camera
        {
            get; set;
        }


        ///// <summary>
        ///// 编码器
        ///// </summary>
        //public virtual Encoder Encoder
        //{
        //    get;set;
        //}

        ///// <summary>
        ///// 编码器通道
        ///// </summary>
        //public int EncoderChannel
        //{
        //    get;set;
        //}

        ///// <summary>
        ///// 转发服务器
        ///// </summary>
        //public  virtual ServerInfo VideoForward
        //{
        //    get;set;
        //}

        ///// <summary>
        ///// 摄像机类型
        ///// </summary>
        //public virtual SystemOption CameraType
        //{
        //    get; set;
        //}

        //是否查哨点，从组织机构分类设置.由于哨位、值班室等节点下可挂n个摄像机，故加标示指定配置
        /// <summary>
        /// 是否为查勤监控点,
        /// </summary>
        public bool IsDutycheckSite
        {
            get; set;
        }

        /// <summary>
        /// 所属机构
        /// </summary>
        public virtual Organization Organization
        {
            get;set;
        }

        /// <summary>
        /// 备注，描述补充
        /// </summary>
        public string Description
        {
            get; set;
        }

        ///// <summary>
        ///// 自动巡航
        ///// </summary>
        //public virtual ICollection<CruiseScanGroup> CruiseScanGroups
        //{
        //    get; set;
        //}

        ///// <summary>
        ///// 预置点
        ///// </summary>
        //public virtual ICollection<PresetSite> PresetSites
        //{
        //    get;set;
        //}
    }
}
