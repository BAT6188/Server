using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Surveillance.ViewModel
{
    /// <summary>
    /// 资源静态扩展方法
    /// </summary>
    public static class ResourcesModelExtension
    {
       /// <summary>
       /// 将monitorysite转发成CameraView对象
       /// </summary>
       /// <param name="ms"></param>
       /// <returns></returns>
        public static CameraView ToCameraView(this MonitorySite ms)
        {
            var cam = ms.Camera;
            CameraView view = new CameraView()
            {
                Status = cam.IPDevice.Status != null ? cam.IPDevice.Status.MappingCode : "0",
                CameraId = cam.CameraId,
                IPDeviceId = cam.IPDeviceId,
                EncoderChannel = cam.EncoderChannel,
                CameraName = ms.MonitorySiteName,
                CameraNo = cam.CameraNo,
                DeviceType = cam.IPDevice.DeviceType.SystemOptionName,
                EncoderInfo = new EncoderInfo()
                {
                    EncoderType = cam.Encoder.EncoderType.EncoderCode,
                    EndPoints = cam.Encoder.DeviceInfo.EndPoints,
                    Password = cam.Encoder.DeviceInfo.Password,
                    User = cam.Encoder.DeviceInfo.UserName
                },
                VideoForwardInfo = new ViewModel.ServiceInfo()
                {
                    EndPoints = cam.VideoForward.EndPoints,
                    User = cam.VideoForward.Username,
                    Password = cam.VideoForward.Password
                }
            };
            return view;
        }
    }
}
