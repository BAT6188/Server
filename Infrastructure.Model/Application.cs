using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Model
{
    /// <summary>
    /// 系统应用
    /// </summary>
    public class Application
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        [Key]
        public Guid ApplicationId
        {
            get; set;
        }

        /// <summary>
        /// 应用名称
        /// </summary>
        public string ApplicationName
        {
            get; set;
        }

        /// <summary>
        /// 应用编码
        /// </summary>
        public string ApplicationCode
        {
            get; set;
        }

        /// <summary>
        /// 应用设置集合
        /// </summary>
        public List<ApplicationSetting> ApplicationSettings
        {
            get;set;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 应用和系统选项的ManyToMany
        /// </summary>
        public List<ApplicationSystemOption> ApplicationSystemOptions { get; set; }
    }
}