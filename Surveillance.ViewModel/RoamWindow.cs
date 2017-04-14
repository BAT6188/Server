using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveillance.ViewModel
{
    /// <summary>
    /// 漫游窗口
    /// </summary>
    public class RoamWindow
    {
        public int WindowID { get; set; }

        public Guid CameraID { get; set; }

        public int Left { get; set; }

        public int Top { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public XYRoamParam[] Monitors { get; set; }
    }

   

    ///// <summary>
    ///// B20 窗，普通窗参数
    ///// </summary>
    //public struct BaseWndParam
    //{
    //    public int Left;

    //    public int Top;

    //    public int Width;

    //    public int Height;
    //}
}
