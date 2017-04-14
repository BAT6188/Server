using Infrastructure.Model;
using System;
using System.Collections.Generic;

namespace Resources.Model
{
    /// <summary>
    /// 哨位集成箱
    /// </summary>
    public class Sentinel
    {
        public Guid SentinelId
        {
            get;set;
        }

        public Guid DeviceInfoId
        {
            get;set;
        }

        public IPDeviceInfo DeviceInfo
        {
            get;set;
        }

        public Guid? FrontCameraId
        {
            get;set;
        }

        /// <summary>
        /// 前置视频
        /// </summary>
        public SentinelVideo FrontCamera
        {
            get;set;
        }

        public Guid? BulletboxCameraId
        {
            get;set;
        }

        /// <summary>
        /// 子弹箱视频
        /// </summary>
        public SentinelVideo BulletboxCamera
        {
            get;set;
        }

        //2016-12-03 zhrx
        ///// <summary>
        ///// 左防区，当前不用到该字段
        ///// </summary>
        //public int LeftArea
        //{
        //    get;set;
        //}

        ///// <summary>
        ///// 右防区，当前不用到该字段
        ///// </summary>
        //public int RightArea
        //{
        //    get;set;
        //}

        //2016-12-03 不通哨位类型的报警输出通道名称不一样，以键值的方式加载 zhrx
        /// <summary>
        /// 报警输出通道
        /// </summary>
        public List<DeviceChannelSetting> AlarmOutputChannels
        {
            get;set;
        }
        ///// <summary>
        ///// 暴狱开关编号
        ///// </summary>
        //public int RebellionSwitch
        //{
        //    get;set;
        //}

        ///// <summary>
        ///// 越狱开关编号
        ///// </summary>
        //public int BreakoutSwitch
        //{
        //    get;set;
        //}

        ///// <summary>
        ///// 袭击开关编号
        ///// </summary>
        //public int RaidSwitch
        //{
        //    get;set;
        //}

        ///// <summary>
        ///// 灾害开关
        ///// </summary>
        //public int DisasterSwitch
        //{
        //    get;set;
        //}

        public Guid? SentinelSettingId
        {
            get;set;
        }

        /// <summary>
        /// 哨位信息设置
        /// </summary>
        public virtual SentinelSetting SentinelSetting
        {
            get;set;
        }

        /// <summary>
        /// 哨位终端视频
        /// </summary>
        public List<SentinelVideo> SentinelVideos
        {
            get;set;
        }

        /// <summary>
        /// 对讲号码
        /// </summary>
        public int Phone
        {
            get;set;
        }

        /// <summary>
        /// 是否启动
        /// </summary>
        public bool IsActive
        {
            get;set;
        }

        ///// <summary>
        ///// 人员指纹记录
        ///// </summary>
        //public List<SentinelFingerPrintMapping> FingerDispatch
        //{
        //    get;set;
        //}

        /// <summary>
        /// 哨位台的防区设备
        /// </summary>
        public List<DefenseDevice> DefenseDevices { get; set; }

        /// <summary>
        /// 哨位音频文件
        /// </summary>
        public Guid? AudioFileId { get; set; }

        /// <summary>
        /// 音频文件
        /// </summary>
        public  virtual Attachment AudioFile { get; set; }
    }
}
