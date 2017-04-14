using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveillance.ViewModel
{
    /// <summary>
    /// 普通屏
    /// </summary>
    public class OriginalWindow
    {
        /// <summary>
        /// 屏号
        /// </summary>
        public int Monitor
        { get; set; }

        /// <summary>
        /// 窗口id
        /// </summary>
        public int WindowID
        { get; set; }

        /// <summary>
        /// 分屏数
        /// </summary>
        public int ViewCount
        { get; set; }

        public int Left { get; set; }

        public int Top { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        /// <summary>
        /// 上墙的视频
        /// </summary>
        public TvPlayingCamera[] Views
        {
            get;set;
        }
    }
}
