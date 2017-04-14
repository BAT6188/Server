using System;
using System.Collections.Generic;

namespace Infrastructure.Model
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid RoleId
        {
            get;set;
        }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName
        {
            get;set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Description
        {
            get;set;
        }

        /// <summary>
        /// 角色权限
        /// </summary>
        public List<RolePermission> RolePermissions
        {
            get;set;
        }

        public Guid? ControlResourcesTypeId
        {
            get;set;
        }

        /// <summary>
        /// 需要管控的资源类型
        /// </summary>
        public virtual SystemOption ControlResourcesType
        {
            get; set;
        }

        /// <summary>
        /// 组织机构Id
        /// </summary>
        public Guid OrganizationId
        {
            get; set;
        }

        /// <summary>
        /// 组织机构
        /// </summary>
        public virtual Organization Organization
        {
            get;set;
        }

        /// <summary>
        /// 授权应用Id
        /// </summary>
        public Guid ApplicationId
        {
            get;
            set;
        }


        /// <summary>
        /// 授权应用集合
        /// </summary>
        public virtual Application Application
        {
            get;
            set;
        }


        /// <summary>
        /// 用户和角色多对多关系
        /// </summary>
        public List<UserRole> UserManyToRole
        {
            get;set;
        }
    }
}