using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveillance.ViewModel
{
    /// <summary>
    /// 云台3D控制参数 
    /// 已过期
    /// </summary>
    public class Ptz3DControlParam : VideoPlayParam
    {
        /// <summary>
        /// 框选大小 Canva Size
        /// </summary>
        public int FrameSelectionWidth { get; set; }

        public int FrameSelectionHeight { get; set; }

        /// <summary>
        /// 开始坐标
        /// </summary>
        public int StartPointX { get; set; }

        public int EndPointX { get; set; }

        public int StartPointY { get; set; }

        public int EndPointY { get; set; }
    }
}
