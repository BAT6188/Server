using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resources.Data
{
    /// <summary>
    /// 哨位视频信息视图
    /// </summary>
    public class SentinelView
    {
        public Guid SentinelId { get; set; }

        /// <summary>
        /// 哨位节点名称
        /// </summary>
        public string SentinelName { get; set; }

        public IPDeviceInfo DeviceInfo
        {
            get; set;
        }

        public SentinelCameraView FrontCamera { get; set; }

      
        /// <summary>
        /// 子弹箱视频
        /// </summary>
        public SentinelCameraView BulletboxCamera
        {
            get; set;
        }

        ///// <summary>
        ///// 左防区
        ///// </summary>
        //public int LeftArea
        //{
        //    get; set;
        //}

        ///// <summary>
        ///// 右防区
        ///// </summary>
        //public int RightArea
        //{
        //    get; set;
        //}

        // 报警输出通道的名称动态加载
        ///// <summary>
        ///// 暴狱开关编号
        ///// </summary>
        //public int RebellionSwitch
        //{
        //    get; set;
        //}

        ///// <summary>
        ///// 越狱开关编号
        ///// </summary>
        //public int BreakoutSwitch
        //{
        //    get; set;
        //}

        ///// <summary>
        ///// 袭击开关编号
        ///// </summary>
        //public int RaidSwitch
        //{
        //    get; set;
        //}

        ///// <summary>
        ///// 灾害开关
        ///// </summary>
        //public int DisasterSwitch
        //{
        //    get; set;
        //}

        public List<DeviceChannelSetting> OutputChannels
        {
            get;set;
        }

        public List<SentinelCameraView> SentinelVideos
        {
            get; set;
        }

        public  SentinelSetting SentinelSetting
        {
            get; set;
        }

        /// <summary>
        /// 对讲号码
        /// </summary>
        public int Phone
        {
            get; set;
        }

        /// <summary>
        /// 是否启动
        /// </summary>
        public bool IsActive
        {
            get; set;
        }

        public List<DefenseDeviceView> DefenseDevices
        {
            get; set;
        }
    }
}
