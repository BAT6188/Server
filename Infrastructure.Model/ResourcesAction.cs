using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Infrastructure.Model
{
    /// <summary>
    /// 操作权限
    /// </summary>
    public class ResourcesAction
    {
        /// <summary>
        /// 操作权限ID
        /// </summary>
        [Key]
        public Guid ResourcesActionId
        {
            get;set;
        }


        /// <summary>
        /// 操作权限名称
        /// </summary>
        public string ResourcesActionName
        {
            get;set;
        }
    }
}