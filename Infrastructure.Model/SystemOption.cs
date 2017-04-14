using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Model
{
    /// <summary>
    /// 系统选项
    /// </summary>
    public class SystemOption
    {
        /// <summary>
        /// 系统选项id
        /// </summary>
        public Guid SystemOptionId { get; set; }

        
        /// <summary>
        /// 选项名称
        /// </summary>
        public string SystemOptionName
        {
            get;set;
        }

        /// <summary>
        /// 数据编码
        /// </summary>
        public string SystemOptionCode
        {
            get;set;
        }

        /// <summary>
        /// 应用与系统选项
        /// </summary>
        public List<ApplicationSystemOption> ApplicationSystemOptions
        {
            get; set;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get; set;
        }

        /// <summary>
        /// 所属上级ID
        /// </summary>
        public Guid? ParentSystemOptionId { get; set; }

        /// <summary>
        /// 所属上级
        /// </summary>
        //[NotMapped]
        public SystemOption ParentSystemOption { get; set; }


        /// <summary>
        /// 是否预定义
        /// </summary>
        public bool Predefine { get; set; }

        /// <summary>
        /// 硬编码，根据设备属性或者其他而附加
        /// </summary>
        public string MappingCode { get; set; }
    }
}