using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Model
{
    /// <summary>
    /// 权限
    /// </summary>
    public class Permission
    {
        /// <summary>
        /// 权限Id
        /// </summary>
       // [Key]
        public Guid PermissionId
        {
            get;set;
        }

        public Guid ResourceId { get; set; }

        public Guid ResourcesActionId { get; set; }

        /// <summary>
        /// 资源
        /// </summary>
        public virtual ApplicationResource Resource { get; set; }

        /// <summary>
        /// 操作权限
        /// </summary>
        public virtual ResourcesAction ResourcesAction
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
    }
}
