using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resources.Model
{
    /// <summary>
    /// 视频模板
    /// </summary>
    public class TemplateLayout
    {
        /// <summary>
        /// PK
        /// </summary>
        public Guid TemplateLayoutId { get; set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        [MaxLength(32)]
        public string TemplateLayoutName { get; set; }

        /// <summary>
        /// 行数
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// 列数
        /// </summary>
        public int Columns { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<TemplateCell> Cells { get; set; }

        public Guid LayoutTypeId { get; set; }

        public Guid TemplateTypeId { get; set; }

        /// <summary>
        /// 布局类型
        /// </summary>
        public virtual SystemOption LayoutType { get; set; }

        /// <summary>
        /// 模板类型
        /// </summary>
        public virtual SystemOption TemplateType { get; set; }

    }
}
