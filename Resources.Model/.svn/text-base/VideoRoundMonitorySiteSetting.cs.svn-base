using System;

namespace Resources.Model
{
    /// <summary>
    /// 轮巡监控点信息
    /// </summary>
    public class VideoRoundMonitorySiteSetting
    {
        public Guid VideoRoundMonitorySiteSettingId
        {
            get;set;
        }

        public Guid MonitorySiteId
        {
            get;set;
        }

        /// <summary>
        /// 监控点
        /// </summary>
        public  virtual MonitorySite MonitorySite
        {
            get;set;
        }

        /// <summary>
        /// 监视器编号
        /// </summary>
        public int Monitor
        {
            get;set;
        }

        /// <summary>
        /// 分屏编号
        /// </summary>
        public int SubView
        {
            get;set;
        }

        public Guid? PresetSiteId
        {
            get;set;
        }

        /// <summary>
        /// 调用预置点
        /// </summary>
        public virtual PresetSite PresetSite
        {
            get;set;
        }

        /// <summary>
        /// 视频流
        /// </summary>
        public int VideoStream
        {
            get;set;
        }
    }
}
