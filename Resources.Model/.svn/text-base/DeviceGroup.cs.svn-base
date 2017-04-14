using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resources.Model
{
    /// <summary>
    /// 设备编组
    /// 2016-09-23 :取消设备与组关系表，改为将id序列化后保存到设备
    /// </summary>
    public class DeviceGroup
    {
        public Guid DeviceGroupId { get; set; }

        public string DeviceGroupName { get; set; }

        public Guid OrganizationId { get; set; }

        public Guid DeviceGroupTypeId { get; set; }

        /// <summary>
        /// 所属组织机构
        /// </summary>
        public virtual Organization Organization { get; set; }

        /// <summary>
        /// 分组类型【根据不同设备类型分类划分】
        /// </summary>
        public virtual SystemOption DeviceGroupType { get; set; }

        //public List<DeviceGroupIPDevice> DeviceGroupIPDevices { get; set; }

        public Guid? ModifiedByUserId { get; set; }

        public User ModifiedBy
        {
            get; set;
        }

        public DateTime? Mondified
        {
            get; set;
        }

        /// <summary>
        /// 设备列表【将设备id序列化】
        /// </summary>
        public string DeviceListJson
        {
            get; set;
        }

        [NotMapped]
        public List<IPDeviceInfo> DeviceList
        {
            get; set;
        }

        [MaxLength(128)]
        public string Description
        {
            get; set;
        }
    }
}
