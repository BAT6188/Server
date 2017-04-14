using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resources.Model
{
    /// <summary>
    /// 主要基报警设置，配置关联的是IPDevice,预案设置关联的是IPdevice。增加Camera的出发点是将监控点和设备关联：
    /// MonitorySite-->Camera-->Encoder
    ///                                        -->IPDeviceInfo
    /// </summary>
    public class Camera
    {
        public Guid CameraId
        {
            get;set;
        }

        public Guid EncoderId
        {
            get;set;
        }

        public Guid IPDeviceId
        {
            get;set;
        }

        public virtual IPDeviceInfo IPDevice
        {
            get;set;
        }

        /// <summary>
        /// 编码器
        /// </summary>
        public virtual Encoder Encoder
        {
            get;set;
        }

        /// <summary>
        /// 编码器通道
        /// </summary>
        public int EncoderChannel
        {
            get; set;
        }

        /// <summary>
        /// 键盘控制器编号
        /// </summary>
        public int CameraNo
        {
            get; set;
        }


        //public Guid CameraTypeId
        //{
        //    get; set;
        //}

        /// <summary>
        /// 转发服务器
        /// </summary>
        public virtual ServiceInfo VideoForward
        {
            get; set;
        }

        public Guid? VideoForwardId
        {
            get; set;
        }

        //归并到设备类型
        ///// <summary>
        ///// 摄像机类型
        ///// </summary>
        //public virtual SystemOption CameraType
        //{
        //    get; set;
        //}

        /// <summary>
        /// 自动巡航
        /// </summary>
        public virtual ICollection<CruiseScanGroup> CruiseScanGroups
        {
            get; set;
        }

        /// <summary>
        /// 预置点
        /// </summary>
        public virtual ICollection<PresetSite> PresetSites
        {
            get; set;
        }

        public Guid? SnapshotId
        {
            get;set;
        }

        /// <summary>
        /// 视频截图【可用于视频上墙，或者预览等等。。。】
        /// </summary>
        public virtual Attachment Snapshot
        {
            get;set;
        }

    }
}
