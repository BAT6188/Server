using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveillance.ViewModel
{
    /// <summary>
    /// 轮巡片段视图
    /// </summary>
    public class VideoRoundSectionView
    {      
        public TemplateLayout TemplateLayout
        {
            get;set;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<RealPlayParam> PlayInfoList
        {
            get;set;
        }

        public List<MonitorView> Monitors
        {
            get;set;
        }


        public int RoundInterval
        {
            get;set;
        }
    }
}
