/**
 * 2016-12-24 zhrx 增加分屏数
 */ 

using System;
using System.ComponentModel;

namespace Resources.Model
{
    public class TemplateCell
    {
        public Guid TemplateCellId
        {
            get;set;
        }

        /// <summary>
        /// 行索引
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// 行索引
        /// </summary>
        public int Column{ get; set; }

        [DefaultValue(1)]
        /// <summary>
        /// 跨行
        /// </summary>
        public int RowSpan { get; set; }

        [DefaultValue(1)]
        /// <summary>
        /// 跨列
        /// </summary>
        public int ColumnSpan { get; set; } 
        
        /// <summary>
        /// 分屏数，用于视频上墙模板分隔(1,4,9,16...) 2016-12-24 zhrx
        /// </summary>
        public int ViewCount
        {
            get;set;
        } 
    }
}
