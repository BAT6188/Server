using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveillance.ViewModel
{
    /// <summary>
    /// 解码器状态
    /// </summary>
    public class DecoderStatusInfo
    {
        /// <summary>
        /// 布局
        /// </summary>
        public DecoderLayout[] Layouts { get; set; }

        /// <summary>
        /// 解码子系统
        /// </summary>
        public InputKeyBoards[] InputKeyBoards { get; set; }

        /// <summary>
        /// 漫游窗口
        /// </summary>
        public RoamWindow[] Roamwnds { get; set; }

        /// <summary>
        /// 拼接窗口
        /// </summary>
        public CombineWindow[] Combinewnds { get; set; }
    }
}
