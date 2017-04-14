using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Model
{
    /// <summary>
    /// 用户和角色多对多关系
    /// </summary>
    public class UserRole
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId
        {
            get;set;
        }

        [JsonIgnore]
        /// <summary>
        /// 用户
        /// </summary>
        public virtual User User
        {
            get;set;
        }


        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid RoleId
        {
            get;set;
        }


        /// <summary>
        /// 角色
        /// </summary>
        [JsonIgnore]
        public virtual Role Role
        {
            get;set;
        }


    }
}
