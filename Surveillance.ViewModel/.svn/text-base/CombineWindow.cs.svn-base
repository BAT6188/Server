using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveillance.ViewModel
{
    /// <summary>
    /// 拼接窗口信息
    /// </summary>
    public class CombineWindow
    {
        public int WindowID { get; set; }

	    public Guid CameraID { get; set; }

        /// <summary>
        /// 跨行数目
        /// </summary>
        public int RowSpan
        {
            get;set;
        }

        /// <summary>
        /// 跨列数目
        /// </summary>
        public int ColumnSpan
        {
            get;set;
        }

        /// <summary>
        /// 覆盖的原始屏，理论上长度= RowSpan * ColumnSpan
        /// </summary>
        public int[] Monitors
        {
            get;set;
        }
    }

}
