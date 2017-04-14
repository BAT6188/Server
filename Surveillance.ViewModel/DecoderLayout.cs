using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveillance.ViewModel
{
    /// <summary>
    /// 解码器布局
    /// </summary>
    public class DecoderLayout
    {

        /// <summary>
        /// 此底图窗口能否移动标志
        /// </summary>
        public int MoveEnable { get; set; }

        /// <summary>
        /// 物理屏宽
        /// </summary>
        public int BaseWidth { get; set; }

        /// <summary>
        /// 物理屏高
        /// </summary>
        public int BaseHeight { get; set; }

        /// <summary>
        /// 行数
        /// </summary>
        public int Row { get; set; }


        /// <summary>
        /// 列
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// 监视器列表
        /// </summary>
        public OriginalWindow[] Monitors { get; set; }

    }
}
