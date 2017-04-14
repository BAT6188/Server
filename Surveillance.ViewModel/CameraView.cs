using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Surveillance.ViewModel
{
    public class CameraView
    {

        public Guid CameraId { get; set; }

        public Guid IPDeviceId { get; set; }

        public string CameraName { get; set; }

        /// <summary>
        /// 摄像机编号，对应网络键盘控制编号
        /// </summary>
        public int CameraNo { get; set; }

        /// <summary>
        /// 编码器通道号
        /// </summary>
        public int EncoderChannel { get; set; }

        /// <summary>
        /// 设备类型:枪机、球机....
        /// </summary>
        public string DeviceType { get; set; }

        /// <summary>
        /// 编码器信息
        /// </summary>
        public virtual EncoderInfo EncoderInfo { get; set; }

        /// <summary>
        /// 视频转发服务器
        /// </summary>
        public ServiceInfo VideoForwardInfo { get; set; }

        /// <summary>
        /// 视频存储服务器
        /// </summary>
        public ServiceInfo VideoStorageInfo { get; set; }

        /// <summary>
        /// 设备状态 ，方便与构建设备状态
        /// </summary>
        public string Status { get; set; }
    }

}