using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Model
{
    /// <summary>
    /// 系统资源
    /// </summary>
    public class ApplicationResource
    {
        /// <summary>
        /// 资源Id
        /// </summary>
        [Key]
        public Guid ApplicationResourceId
        {
            get;set;
        }
        
        /// <summary>
        /// 资源名称
        /// </summary>
        public string ApplicationResourceName
        {
            get;set;
        }

        /// <summary>
        /// 具备操作
        /// </summary>
        public List<ResourcesAction> Actions
        {
            get;set;
        }

        /// <summary>
        /// 所属系统Id
        /// </summary>
        public Guid ApplicationId
        {
            set;
            get;
        }


        /// <summary>
        /// 所属系统
        /// </summary>
        public virtual Application Application
        {
            set;
            get;
        }

        /// <summary>
        /// 所属上级Id
        /// </summary>
        public Guid? ParentResourceId
        {
            get; set;
        }

        /// <summary>
        /// 所属上级
        /// </summary>
        public virtual ApplicationResource ParentResource
        {
            get;set;
        }
    }
}