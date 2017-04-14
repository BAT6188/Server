using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Resources.Model;
using Infrastructure.Model;
using AllInOneContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Infrastructure.Utility;

namespace Resources.Services
{
    /// <summary>
    /// 监控点控制器
    /// </summary>
    [Route("Resources/[controller]")]
    public class MonitorySiteController : Controller
    {
        ILogger<MonitorySiteController> _logger;

        public MonitorySiteController(ILogger<MonitorySiteController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add([FromBody]MonitorySite ms)
        {
            if (ms == null)
                return BadRequest("MonitorySite object can not be null!");
            if (ExistsName(ms.MonitorySiteName, ms.OrganizationId, ms.MonitorySiteId))
                return BadRequest("MonitorySite name has been used!");
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    db.MonitorySite.Add(ms);
                    //将encoder ip保存到camera
                    var encoder = db.Encoder.Include(t => t.DeviceInfo).FirstOrDefault(t => t.EncoderId.Equals(ms.Camera.EncoderId));
                    if (encoder != null)
                        ms.Camera.IPDevice.EndPointsJson = encoder.DeviceInfo.EndPointsJson;
                    db.SaveChanges();
                    //return CreatedAtRoute("GetById", new { id = ms.MonitorySiteId }, ms);
                    //推送到dcp,数据同步
                    return CreatedAtAction("", ms);
                }
                catch (DbUpdateException dbEx)
                {
                    _logger.LogError("添加监控点异常：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
                }
                catch (Exception ex)
                {
                    _logger.LogError("添加监控点异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Remove(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        MonitorySite ms = db.MonitorySite.FirstOrDefault(t => t.MonitorySiteId.Equals(id));
                        if (ms == null)
                        {
                            return NotFound();
                        }
                        Camera camera = db.Set<Camera>().First(t => t.CameraId.Equals(ms.CameraId));
                        IPDeviceInfo cameraDevice = db.IPDeviceInfo.First(t => t.IPDeviceInfoId.Equals(camera.IPDeviceId));
                        db.MonitorySite.Remove(ms);
                        db.Set<Camera>().Remove(camera);
                        //db.IPDeviceInfo.Remove(cameraDevice);
                        var deleteStatusId = Guid.Parse("a0002016-e009-b019-e001-abcd13800005");
                        cameraDevice.StatusId = deleteStatusId;
                        //Remove AlarmSetting
                        List<Guid> alarmSourceIds = new List<Guid>();
                        alarmSourceIds.Add(cameraDevice.IPDeviceInfoId);
                        AlarmSettingUtility.RemoveAlarmSetting(db, alarmSourceIds);
                        db.SaveChanges();
                        transaction.Commit();
                        //推送到dcp,数据同步。。。。。。
                        return new NoContentResult();
                    }
                    catch (DbUpdateException dbEx)
                    {
                        transaction.Rollback();
                        _logger.LogError("删除监控点异常：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);
                        return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("删除监控点异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                        return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                    }
                }
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody]MonitorySite ms)
        {
            if (ms == null)
                return BadRequest("MonitorySite object can not be null!");
            if (ExistsName(ms.MonitorySiteName, ms.OrganizationId, ms.MonitorySiteId))
                return BadRequest("MonitorySite name has been used!");
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    db.MonitorySite.Update(ms);
                    IPDeviceInfo cameraDevice = db.IPDeviceInfo.FirstOrDefault(t => t.IPDeviceInfoId.Equals(ms.Camera.IPDeviceId));
                    if (cameraDevice != null)
                    {
                        cameraDevice.IPDeviceName = ms.MonitorySiteName;
                        db.IPDeviceInfo.Update(cameraDevice);
                    }
                    db.SaveChanges();
                    // //推送到dcp,数据同步。。。。。
                    return NoContent();
                }
                catch (DbUpdateException dbEx)
                {
                    _logger.LogError("更新监控点异常：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
                }
                catch (Exception ex)
                {
                    _logger.LogError("更新监控点异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
        }

        /// <summary>
        /// 根据监控点id获取监控点信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeEncoder"></param>
        /// <param name="includeVideoForward"></param>
        /// <param name="includeCameraType"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                if (db.MonitorySite.Where(t => t.MonitorySiteId.Equals(id)).Count() > 0)
                {
                    var monitorysite = GetAllMonitorysite(db).FirstOrDefault(t => t.MonitorySiteId.Equals(id));
                    return new ObjectResult(monitorysite);
                }
                else
                {
                    return NotFound();
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
        //http://localhost:5001/Resources/MonitorySite/organizationId=b31f22c1-bcd8-4b5a-ad5b-70760a1a9d74?cameraTypeId=889594b5-e35f-4e74-b56b-05d9cbe3e240&includeLower=true
        [HttpGet]
        [Route("~/Resources/MonitorySite/organizationId={organizationId}")]
        //public IEnumerable<MonitorySite> GetByOrganization(Guid organizationId, bool includeEncoder=true, bool includeCameraType=true, bool includeVideoForward=true)
        public IEnumerable<MonitorySite> GetByOrganization(Guid organizationId, Guid cameraTypeId, string ipDeviceName,bool includeLower = false)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var monitorysites = GetAllMonitorysite(db);
                //var query = db.MonitorySite.Where(t => t.Organization.OrganizationId.Equals(organizationId));
                //if (includeEncoder)
                //    query = query.Include(t => t.Camera).ThenInclude(t => t.Encoder);
                //if (includeCameraType)
                //    query = query.Include(t => t.CameraType);
                //if (includeVideoForward)
                //    query = query.Include(t => t.VideoForward);
                //return query.OrderBy(t=>t.OrderNo).ThenBy(t=>t.MonitorySiteName).ToList();
                if (includeLower)
                {
                    var org = db.Organization.FirstOrDefault(t => t.OrganizationId.Equals(organizationId));
                    if (org != null)
                    {
                        return monitorysites.Where(t => t.Organization.OrganizationFullName.Contains(org.OrganizationFullName) &&
                             ((cameraTypeId.Equals(Guid.Empty) || t.Camera.IPDevice.DeviceTypeId.Equals(cameraTypeId))) &&
                             ((String.IsNullOrEmpty(ipDeviceName) || t.Camera.IPDevice.IPDeviceName.Contains(ipDeviceName)))).ToList();
                    }
                }
                else
                {
                    return monitorysites.Where(t => t.OrganizationId.Equals(organizationId) &&
                              ((cameraTypeId.Equals(Guid.Empty) || t.Camera.IPDevice.DeviceTypeId.Equals(cameraTypeId))) &&
                              ((String.IsNullOrEmpty(ipDeviceName) || t.Camera.IPDevice.IPDeviceName.Contains(ipDeviceName)))).ToList();
                }
                return null;
            }
        }

        /// <summary>
        /// 获取用户可操作的监控点
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("~/Resources/MonitorySite/userId={userId}")]
        public IEnumerable<MonitorySite> GetByUser(Guid userId)
        {
            return null;
        }

        [HttpGet]
        public IEnumerable<MonitorySite> GetAll()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return GetAllMonitorysite(db);
            }
        }

        /// <summary>
        /// 判断组织机构下的监控点名称是否已存在
        /// </summary>
        /// <param name="monitorySiteName"></param>
        /// <param name="organizationId"></param>
        /// <param name="monitorySiteId">设备id,更新需考虑</param>
        /// <returns></returns>
        public bool ExistsName(string monitorySiteName, Guid organizationId, Guid monitorySiteId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return db.MonitorySite.Where(t => t.MonitorySiteName.Equals(monitorySiteName)
                    && t.OrganizationId.Equals(organizationId)
                    && !monitorySiteId.Equals(t.MonitorySiteId)).Count() > 0;
            }
        }


        private List<MonitorySite> GetAllMonitorysite(AllInOneContext.AllInOneContext dbContext)
        {
            var cameras = dbContext.Set<Camera>().Include(t => t.Encoder).ThenInclude(t => t.EncoderType).
                Include(t => t.Encoder).ThenInclude(t => t.DeviceInfo).ThenInclude(t=>t.Organization).
                Include(t => t.VideoForward).ThenInclude(t=>t.ServerInfo).Include(t => t.IPDevice).ThenInclude(t => t.DeviceType).
                ToList();

            var monitorysites = dbContext.MonitorySite.Include(t => t.Organization).OrderBy(t => t.OrderNo).ThenBy(t => t.MonitorySiteName).ToList();
            monitorysites.ForEach(t =>
            {
                t.Camera = cameras.FirstOrDefault(c => c.CameraId.Equals(t.CameraId));
            });
           return monitorysites;
        }


        [HttpGet]
        [Route("~/Resources/MonitorySite/cameraId={cameraId}")]
        public IActionResult GetMonitorySiteByCameraID(Guid cameraId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    List<MonitorySite> monitorysites = GetAllMonitorysite(db);

                    MonitorySite ms = monitorysites.FirstOrDefault(p => p.CameraId.Equals(cameraId));

                    return new ObjectResult(ms);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("GetMonitorySiteByCameraID：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }
    }
}
