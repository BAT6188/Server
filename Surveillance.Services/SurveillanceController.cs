using Infrastructure.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Resources.Model;
using Surveillance.Model;
using Surveillance.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Surveillance.Services
{
    [Route("Surveillance/[Controller]")]
    public class SurveillanceController : Controller
    {
        private ILogger<SurveillanceController> _logger;

        public SurveillanceController(ILogger<SurveillanceController> logger)
        {
            _logger = logger;
        }

        ///// <summary>
        ///// 获取摄像机列表  2016-09-27 将摄像机列表和报警列表合并到同一个接口
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("~/Surveillance/Camera")]
        //public IEnumerable<CameraView> GetAllCamera()
        //{
        //    using (var db = new AllInOneContext.AllInOneContext())
        //    {
        //        try
        //        {
        //            var list = (from ms in db.MonitorySite.Include(t => t.VideoForward).ToList()
        //                        join cam in db.Set<Camera>().ToList() on ms.CameraId equals cam.CameraId
        //                        join encoder in db.Encoder.Include(t => t.DeviceInfo).Include(t => t.EncoderType).ToList() on cam.EncoderId equals encoder.EncoderId
        //                        select new CameraView()
        //                        {
        //                            DeviceInfoId = cam.IPDeviceId,
        //                            DeviceName = ms.MonitorySiteName,
        //                            CameraNo = ms.CameraNo,
        //                            EncoderCode = encoder.EncoderType.EncoderCode,
        //                            EncoderChannel = cam.EncoderChannel,
        //                            EncoderId = cam.EncoderId,
        //                            EncoderEndPoints = encoder.DeviceInfo.EndPoints,
        //                            Password = encoder.DeviceInfo.Password,
        //                            UserName = encoder.DeviceInfo.UserName,
        //                            VideoForwardEndPoints = ms.VideoForward.EndPoints
        //                        }).ToList();
        //            return list;
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError("获取摄像机信息异常：Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
        //            return null;
        //        }
        //    }
        //}

            //补充：转发信息加载....
        /// <summary>
        /// 获取编码器报警设置，提供给DCP调用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Surveillance/Encoder/Device")]
        public IEnumerable<EncoderAlarmView> GetEncoderAlarmSetting()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    //摄像机通道 + 外接设备接入通道 = 编码器通道；
                    var alarmchannels = ((from cam in db.Set<Camera>().Include(t=>t.IPDevice).Include(t=>t.VideoForward).ToList()
                                          join encoder in db.Encoder.Include(t=>t.DeviceInfo).Include(t=>t.EncoderType).ToList() on cam.EncoderId equals encoder.EncoderId
                                          select new
                                          {
                                              IPDeviceId = cam.IPDeviceId,
                                              CameraId = cam.CameraId,
                                              DeviceInfo = cam.IPDevice,
                                              Encoder = cam.Encoder,
                                              EncoderId = cam.EncoderId,
                                              EncoderChannel = cam.EncoderChannel,
                                              //VideoForwardPort = cam.VideoForward.Port,
                                              VideoForward = cam.VideoForward,
                                              CameraNo = cam.CameraNo,
                                              IsCamera=true,
                                          }).Union(
                        from ap in db.AlarmPeripheral.Include(t=>t.AlarmDevice).ToList()
                        join encoder in db.Encoder.Include(t => t.DeviceInfo).Include(t=>t.EncoderType).ToList() on ap.EncoderId equals encoder.EncoderId
                        select new
                        {
                            IPDeviceId = ap.AlarmDeviceId,
                            CameraId = ap.AlarmDeviceId,
                            DeviceInfo = ap.AlarmDevice,
                            Encoder = encoder,
                            EncoderId = encoder.EncoderId,
                            EncoderChannel = ap.AlarmChannel,
                            //VideoForwardPort = 0,
                            VideoForward = new Resources.Model.ServiceInfo(),
                            CameraNo = 0,
                            IsCamera=false
                        })).ToList();

                    var alarmsettings = db.AlarmSetting.Include(t => t.AlarmType).Select(t => new
                    {
                        IPDeviceId = t.AlarmSourceId,
                        AlarmTypeId = t.AlarmTypeId
                    }).ToList().GroupBy(t => t.IPDeviceId, t => t.AlarmTypeId);

                    var list = (from device in alarmchannels
                                join st in alarmsettings on device.IPDeviceId equals st.Key into leftAlarmSettings
                                from leftAlarmSetting in leftAlarmSettings.DefaultIfEmpty()
                                select new EncoderAlarmView()
                                {
                                    DeviceInfo = new CameraView()
                                    {
                                        CameraId = device.CameraId,
                                        IPDeviceId = device.IPDeviceId,
                                        CameraName = device.DeviceInfo.IPDeviceName,
                                        EncoderChannel = device.EncoderChannel,
                                        CameraNo = device.CameraNo,
                                        EncoderInfo = new EncoderInfo()
                                        {
                                            EncoderType = device.Encoder.EncoderType.EncoderCode,
                                            EndPoints = device.Encoder.DeviceInfo.EndPoints,
                                            Password = device.Encoder.DeviceInfo.Password,
                                            User = device.Encoder.DeviceInfo.UserName
                                        },
                                        VideoForwardInfo = new ViewModel.ServiceInfo()
                                        {
                                            EndPoints = device.VideoForward.EndPoints,
                                            User = device.VideoForward.Username,
                                            Password = device.VideoForward.Password
                                        }
                                    },
                                    DeployAlarmType = leftAlarmSetting==null?null:leftAlarmSetting.ToList(),//st.ToList(), 
                                    EncoderId = device.EncoderId,
                                    IsCamera = device.IsCamera
                                }).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    _logger.LogError("获取编码器配置异常：Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                    return null;
                }
            }
        }

        /// <summary>
        /// 获取编码器视频接入配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Surveillance/Encoder/Camera")]
        public IEnumerable<CameraView> GetEncoderCamera()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    var camInfo = (from cam in db.Set<Camera>().Include(t => t.VideoForward).Include(t => t.IPDevice).ThenInclude(t=>t.DeviceType).ToList()
                                   join encoder in db.Encoder.Include(t => t.DeviceInfo).Include(t => t.EncoderType).ToList() on cam.EncoderId equals encoder.EncoderId
                                   select new CameraView()
                                   {
                                       CameraId = cam.CameraId,
                                       IPDeviceId = cam.IPDeviceId,
                                       CameraName = cam.IPDevice.IPDeviceName,
                                       EncoderChannel = cam.EncoderChannel,
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

                                   }).ToList();// t => t.DeviceInfoId.Equals(deviceId));
                    return camInfo;
                }
                catch (Exception ex)
                {
                    _logger.LogError("根据获取获取摄像机信息异常：Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                    return null;
                }
            }
        }


        /// <summary>
        /// 设备时钟同步
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("~/Surveillance/Timelock")]
        public IActionResult Timelock()
        {
            return Ok();
        }

        /// <summary>
        /// 增加人脸识别记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("~/Surveillance/Face")]
        public IActionResult AddFaceRecognition(FaceRecognition face)
        {
            return Ok();
        }

        /// <summary>
        /// 增加车牌识别 license plate recognition(LPR)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("~/Surveillance/LicencePlate")]
        public IActionResult AddLPR(LicensePlateRecognition lpr)
        {
            return Ok();
        }

        /// <summary>
        /// 增加视频质量分析
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("~/Surveillance/VideoQuality")]
        public IActionResult VideoQuality(LicensePlateRecognition lpr)
        {
            return Ok();
        }

        /// <summary>
        /// 增加车牌识别 license plate recognition(LPR)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("~/Surveillance/ThermalImaging")]
        public IActionResult AddThermalImaging(ThermalImaging ti)
        {
            return Ok();
        }

        /// <summary>
        /// 根据摄像机设备id获取摄像机信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Surveillance/Camera/deviceId={deviceId}")]
        public CameraView GetByCameraDeviceId(Guid deviceId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    var camInfo = (from cam in db.Set<Camera>().Include(t => t.VideoForward).Include(t => t.IPDevice).ThenInclude(t=>t.DeviceType).ToList()
                                   join encoder in db.Encoder.Include(t => t.DeviceInfo).Include(t => t.EncoderType).ToList() on cam.EncoderId equals encoder.EncoderId
                                   where cam.IPDeviceId.Equals(deviceId)
                                   select new CameraView()
                                   {
                                       CameraId = cam.CameraId,
                                       IPDeviceId = cam.IPDeviceId,
                                       CameraName = cam.IPDevice.IPDeviceName,
                                       EncoderChannel = cam.EncoderChannel,
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
                                   }).FirstOrDefault();// t => t.DeviceInfoId.Equals(deviceId));
                    return camInfo;
                }
                catch (Exception ex)
                {
                    _logger.LogError("根据设备id获取获取摄像机信息异常：Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                    return null;
                }
            }
        }

        /// <summary>
        /// 根据摄像机设备id获取摄像机信息组信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Surveillance/Camera/ids={ids}")]
        public IEnumerable<CameraView> GetByCameraDeviceIds(string ids)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    var cameraIds = JsonConvert.DeserializeObject<Guid[]>(ids);
                    var camInfo = (from cam in db.Set<Camera>().Include(t => t.VideoForward).Include(t => t.IPDevice).ThenInclude(t=>t.DeviceType).ToList()
                                   join encoder in db.Encoder.Include(t => t.DeviceInfo).Include(t => t.EncoderType).ToList() on cam.EncoderId equals encoder.EncoderId
                                   where cameraIds.Contains(cam.IPDeviceId)
                                   select new CameraView()
                                   {
                                       CameraId = cam.CameraId,
                                       IPDeviceId = cam.IPDeviceId,
                                       EncoderChannel = cam.EncoderChannel,
                                       CameraName = cam.IPDevice.IPDeviceName,
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
                                   }).ToList();
                    return camInfo;
                }
                catch (Exception ex)
                {
                    _logger.LogError("根据设备id列表获取获取摄像机信息异常：Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                    return null;
                }
            }
        }

        /// <summary>
        /// 获取组织机构的所属监控点
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="includeEncoder"></param>
        /// <param name="includeVideoForward"></param>
        /// <param name="includeCameraType"></param>
        /// <returns></returns>
        //http://localhost:5001/Surveillance/Camera/organizationId=b31f22c1-bcd8-4b5a-ad5b-70760a1a9d74?includeLower=true
        [HttpGet]
        [Route("~/Surveillance/Camera/organizationId={organizationId}")]
        public IEnumerable<CameraView> GetByOrganization(Guid organizationId, bool includeLower = false)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                List<MonitorySite> monitorysites = db.MonitorySite.Include(t => t.Organization).OrderBy(t => t.OrderNo).ThenBy(t => t.MonitorySiteName).ToList();
                if (includeLower)
                {
                    var org = db.Organization.FirstOrDefault(t => t.OrganizationId.Equals(organizationId));
                    if (org != null)
                        monitorysites = monitorysites.Where(t => t.Organization.OrganizationFullName.Contains(org.OrganizationFullName)).ToList();
                }
                else
                {
                    monitorysites = monitorysites.Where(t => t.OrganizationId.Equals(organizationId)).ToList();
                }
                var camInfo = (from ms in monitorysites
                               join cam in db.Set<Camera>().Include(t => t.VideoForward).
                               Include(t => t.IPDevice).ThenInclude(t => t.Status).
                               Include(t => t.IPDevice).ThenInclude(t => t.DeviceType).ToList() on ms.CameraId equals cam.CameraId
                               join encoder in db.Encoder.Include(t => t.DeviceInfo).Include(t => t.EncoderType).ToList() on cam.EncoderId equals encoder.EncoderId
                               select new CameraView()
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
                               }).ToList();
                return camInfo;
            }
        }

    }
}
