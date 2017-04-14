using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Model
{
    /// <summary>
    /// 角色和权限多对多关系
    /// </summary>
    public class RolePermission
    {
        //public Guid RolePermissionId
        //{
        //    get; set;
        //}

        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid RoleId
        {
            get; set;
        }


        /// <summary>
        /// 角色
        /// </summary>
        [JsonIgnore]
        public virtual Role Role
        {
            get;set;
        }



        /// <summary>
        /// 权限ID
        /// </summary>
        public Guid PermissionId
        {
            get;set;
        }

        /// <summary>
        /// 权限
        /// </summary>
        [JsonIgnore]
        public virtual Permission Permission
        {
            get;set;
        }
    }
}
