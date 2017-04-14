using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources.Model
{
    /// <summary>
    /// 轮巡片段
    /// </summary>
    public class VideoRoundSection
    {
        public Guid VideoRoundSectionId
        {
            get;set;
        }
        public Guid TemplateLayoutId
        {
            get; set;
        }

        public virtual TemplateLayout TemplateLayout
        {
            get;set;
        }

        public List<VideoRoundMonitorySiteSetting> RoundMonitorySiteSettings
        {
            get;set;
        }

        public int RoundInterval
        {
            get;set;
        }
    }
}
