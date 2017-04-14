using System;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Model
{
    /// <summary>
    /// 应用设置
    /// </summary>
    public class ApplicationSetting
    {
        /// <summary>
        /// 设置ID
        /// </summary>
        [Key]
        public Guid ApplicationSettingId
        {
            get; set;
        }

        /// <summary>
        /// 设置编码
        /// </summary>
        public string SettingKey
        {
            get; set;
        }

        /// <summary>
        /// 设置属性
        /// </summary>
        public string SettingValue
        {
            get; set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Description
        {
            get; set;
        }        
    }
}