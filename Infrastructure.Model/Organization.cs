using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Infrastructure.Model
{
    /// <summary>
    /// 组织机构
    /// </summary>
    public class Organization
    {

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public Guid OrganizationId
        {
            get;set;
        }

        /// <summary>
        /// 组织机构Code,对于哨位节点，同时作为哨位编号
        /// </summary>
        public string OrganizationCode
        {
            get; set;
        }

        /// <summary>
        /// 组织机构名称
        /// </summary>
        [Required]
        public string OrganizationShortName
        {
            get;set;
        }

        /// <summary>
        /// 完整名称
        /// </summary>
        public string OrganizationFullName
        {
            get;set;
        }
        
        /// <summary>
        /// 自定义序号
        /// </summary>
        public int OrderNo
        {
            get;set;
        }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone
        {
            get;set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Description
        {
            get; set;
        }
        
        /// <summary>
        /// 组织机构级别
        /// </summary>
        public int OrganizationLevel
        {
            get; set;
        }

        /// <summary>
        /// 应用中心Id
        /// </summary>
        public Guid? CenterId
        {
            get; set;
        }

        /// <summary>
        /// 应用中心设置
        /// </summary>
        public virtual ApplicationCenter Center
        {
            get; set;
        }

        /// <summary>
        /// 组织机构类型Id
        /// </summary>
        public Guid? OrganizationTypeId { get; set; }

        /// <summary>
        /// 组织机构类型
        /// </summary>
        //[NotMapped]
        public virtual  SystemOption OrganizationType
        {
            get; set;
        }


        /// <summary>
        /// 上级机构Id
        /// </summary>
        public Guid? ParentOrganizationId { get; set; }

        /// <summary>
        /// 父节点组织机构
        /// </summary>
        //[NotMapped]
        [JsonIgnore]
        [XmlIgnore]
        public  virtual Organization ParentOrganization
        {
            get; set;
        }

        //[NotMapped]
        //public virtual ICollection<Organization> ChildOrganizations { get; set; }

            /// <summary>
            /// 勤务类型
            /// </summary>
        public Guid? InServiceTypeId
        {
            get;set;
        }

        public virtual SystemOption InServiceType
        {
            get;set;
        }

        /// <summary>
        /// 执勤目标
        /// </summary>
        public string OnDutyTarget
        {
            get;set;
        }

        /// <summary>
        /// 查勤点数量
        /// </summary>
        //[NotMapped]
        public int DutycheckPoints
        {
            get;set;
        }
    }
}