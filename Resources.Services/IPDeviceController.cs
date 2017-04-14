/**
 * 2016-12-22 zhrx 过滤已经删除的设备 
 * 2016-12-26 zhrx 设备编号自动分配
 */ 
using HttpClientEx;
using Infrastructure.Model;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Resources.Services
{
    [Route("Resources/[controller]")]
    public class IPDeviceController:Controller
    {
        ILogger<IPDeviceController> _logger;

        public IPDeviceController(ILogger<IPDeviceController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Add([FromBody] IPDeviceInfo deviceInfo)
        {
            if (deviceInfo == null)
                return BadRequest("IPDeviceInfo object can not be null!");
            if (ExistsName(deviceInfo.IPDeviceName, deviceInfo.OrganizationId, deviceInfo.IPDeviceInfoId))
            {
                return BadRequest("IPDeviceInfo name has been used!");
            }
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    deviceInfo.Modified = DateTime.Now;
                    deviceInfo.IPDeviceCode = NewIpdeviceCode(db, deviceInfo.OrganizationId);
                    db.IPDeviceInfo.Add(deviceInfo);
                    db.SaveChanges();
                    //  return CreatedAtRoute("GetById",new { id = deviceInfo.DeviceInfoId }, deviceInfo);
                    SendDatachangeNotify(deviceInfo, 2);
                    return CreatedAtAction("", deviceInfo);
                }
                catch (DbUpdateException dbEx)
                {
                    _logger.LogError("添加设备异常：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
                }
                catch (Exception ex)
                {
                    _logger.LogError("添加设备异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
        }

        //设备ip更新，适用于智敏类型，自动检测设备ip
        [HttpPut]
        public IActionResult Update([FromBody] IPDeviceInfo deviceInfo)
        {
            if (deviceInfo == null)
                return BadRequest("IPDeviceInfo object can not be null!");
            if (ExistsName(deviceInfo.IPDeviceName, deviceInfo.OrganizationId, deviceInfo.IPDeviceInfoId))
                return BadRequest("IPDeviceInfo name has been used!");
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    deviceInfo.Modified = DateTime.Now;
                    db.IPDeviceInfo.Update(deviceInfo);
                    db.SaveChanges();
                    SendDatachangeNotify(deviceInfo, 1);
                    return NoContent();
                }
                catch (DbUpdateException dbEx)
                {
                    _logger.LogError("更新设备异常：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
                }
                catch (Exception ex)
                {
                    _logger.LogError("更新设备异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                IPDeviceInfo deleteObj = db.IPDeviceInfo.FirstOrDefault(t => t.IPDeviceInfoId.Equals(id));
                if (deleteObj == null)
                    return NotFound();
                db.IPDeviceInfo.Remove(deleteObj);
                db.SaveChanges();
                SendDatachangeNotify(deleteObj, 0);
            }
            return NoContent();
        }

        //   [HttpGet("{id}",Name ="GetById")]
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var device = db.IPDeviceInfo.Include(t=>t.DeviceType).Include(t => t.Organization).FirstOrDefault(t => t.IPDeviceInfoId.Equals(id));
                if (device == null)
                    return NotFound();
                return new OkObjectResult(device);
            }
        }

        /// <summary>
        /// 获取组织机构的所属设备
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Resources/IPDevice/organizationId={organizationId}")]
        public IEnumerable<IPDeviceInfo> GetByOrganization(Guid organizationId, Guid deviceTypeId, bool includeLower = true)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var devices = FilterDeleteDevice(db);
                if (includeLower)
                {
                    var org = db.Organization.FirstOrDefault(t => t.OrganizationId.Equals(organizationId));
                    return devices.Where(t => t.Organization.OrganizationFullName.Contains(org.OrganizationFullName)
                        && (Guid.Empty.Equals(deviceTypeId) || t.DeviceTypeId.Equals(deviceTypeId))).ToList();
                }
                else
                {
                    return devices.Where(t => t.Organization.OrganizationId == organizationId
                        && (Guid.Empty.Equals(deviceTypeId) || t.DeviceTypeId.Equals(deviceTypeId))).ToList();
                }
            }
        }

        /// <summary>
        /// 根据设备类型过滤查询
        /// </summary>
        /// <param name="systemOptionId">设备类型id（可能是设备子类型id）</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Resources/IPDevice/systemOptionId={systemOptionId}")]
        public IEnumerable<IPDeviceInfo> GetByDeviceType(Guid systemOptionId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var devices = FilterDeleteDevice(db);
                return devices.Where(t => systemOptionId.Equals(t.DeviceType.SystemOptionId) ||
                     systemOptionId.Equals(t.DeviceType.ParentSystemOptionId)).ToList();
                //return db.IPDeviceInfo.Include(t => t.DeviceType).Include(t=>t.Organization).Where(t => systemOptionId.Equals(t.DeviceType.SystemOptionId) ||
                // systemOptionId.Equals(t.DeviceType.ParentSystemOptionId)).ToList();
            }
        }

        /// <summary>
        /// 根据设备类型过滤查询
        /// </summary>
        /// <param name="systemOptionId">设备类型id（可能是设备子类型id）</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Resources/IPDevice/systemOptionCode={systemOptionCode}")]
        public IEnumerable<IPDeviceInfo> GetBySystemOptionCode(string systemOptionCode)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var deviceType = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals(systemOptionCode));
                if (deviceType != null)
                {
                    var devices = FilterDeleteDevice(db);
                    return devices.Where(t => deviceType.SystemOptionId.Equals(t.DeviceType.SystemOptionId) ||
                        deviceType.SystemOptionId.Equals(t.DeviceType.ParentSystemOptionId)).ToList();
                    //return db.IPDeviceInfo.Include(t => t.DeviceType).Include(t=>t.Organization).Where(t => deviceType.SystemOptionId.Equals(t.DeviceType.SystemOptionId) ||
                    // deviceType.SystemOptionId.Equals(t.DeviceType.ParentSystemOptionId)).ToList();
                }
                return null;
            }
        }

        /// <summary>
        /// 获取用户可操作的设备
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        //ipdeviceIno/userid=...
        [Route("~/Resources/IPDevice/userid={userId}")]
        public IEnumerable<IPDeviceInfo> GetByUser(Guid userId)
        {
            return null;
        }

        [HttpGet] 
        public IEnumerable<IPDeviceInfo> GetAll()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return FilterDeleteDevice(db);
            }
        }

        //private bool Exists(Guid deviceid)
        //{
        //    using (var db = new AllInOneContext.AllInOneContext())
        //    {
        //        return db.IPDeviceInfo.Where(t => t.IPDeviceInfoId.Equals(deviceid)).Count() > 0;
        //    } 
        //    //return _cacheIPDeviceList.Exists(d => d.IPDeviceInfoId.Equals(deviceid));
        //}

        /// <summary>
        /// 判断组织机构下的设备名称是否已存在
        /// </summary>
        /// <param name="name"></param>
        /// <param name="organizationId"></param>
        /// <param name="deviceId">设备id,更新需考虑</param>
        /// <returns></returns>
        public bool ExistsName(string name, Guid organizationId, Guid deviceId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return db.IPDeviceInfo.Where(t => t.IPDeviceName.Equals(name)
                    && t.OrganizationId.Equals(organizationId)
                    && !deviceId.Equals(t.IPDeviceInfoId)).Count() > 0;
            }
        }

        /// <summary>
        /// 设备状态更新
        /// </summary>
        /// <param name="deviceInfoId"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("~/Resources/IPDevice/DeviceStatus")]
        //http://localhost:5001/IPDevice/deviceStatus?deviceInfoId=7d57976c-a472-8a47-81d4-0005d4dff4ec&StatusId=A0002016-E009-B019-E001-ABCD13800002
        public IActionResult UpdateDeviceStatus(Guid deviceInfoId, Guid statusId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    //SystemOption statusOptions = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals(statusId));
                    //if (statusOptions == null)
                    //    return BadRequest(new ApplicationException() { ErrorCode = "Unknow", ErrorMessage = "未定义设备状态" });
                    var device = db.IPDeviceInfo.FirstOrDefault(t => t.IPDeviceInfoId.Equals(deviceInfoId));
                    if (device != null && !device.StatusId.Equals(statusId))
                    {
                        device.StatusId = statusId;
                        db.IPDeviceInfo.Update(device);
                        //status history
                        DeviceStatusHistory statusHis = new DeviceStatusHistory()
                        {
                            DeviceStatusHistoryId = Guid.NewGuid(),
                            DeviceInfoId = deviceInfoId,
                            StatusId = statusId,
                            CreateTime = DateTime.Now
                        };
                        db.DeviceStatusHistory.Add(statusHis);
                        db.SaveChanges();
                        MQPulish.PublishMessage("DeviceStatus",statusHis);
                    }
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError("更新设备状态异常：Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException() { ErrorCode = "Unknow", ErrorMessage = ex.Message });
                }
            }
        }

        /// <summary>
        /// 设备同步通知
        /// </summary>
        /// <param name="device"></param>
        /// <param name="state">0:删除，1：更新： 2：新增</param>
        private void SendDatachangeNotify(IPDeviceInfo device, int state)
        {
            if (device.IPDeviceCode > 0)
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    //哨位台挂在哨位节点上面
                    var deviceOrg = db.Organization.FirstOrDefault(t => t.OrganizationId.Equals(device.OrganizationId));
                    var service = db.ServiceInfo.Include(t => t.ServerInfo).FirstOrDefault(t => t.ServiceTypeId.Equals(Guid.Parse("A0002016-E009-B019-E001-ABCD11300206"))
                            && (t.ServerInfo.OrganizationId.Equals(deviceOrg.ParentOrganizationId) || t.ServerInfo.OrganizationId.Equals(device.OrganizationId)));
                    if (service != null)
                    {
                        PAPS.Data.DeviceInfoChange changeInfo = new PAPS.Data.DeviceInfoChange()
                        {
                            DeviceCode = device.IPDeviceCode,
                            DeviceId = device.IPDeviceInfoId,
                            DevicetypeId = device.DeviceTypeId,
                            Operater = state
                        };
                        try
                        {
                            new ASCSApi(service).NotifyDeviceInfoChange(changeInfo);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError("设备变更通知异常{0}\r\rStackTrace:{1}", ex.Message, ex.StackTrace);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取设备的通道选项定义
        /// </summary>
        /// <param name="deviceTypeId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Resources/IPDevice/ChannelOption/deviceTypeId={deviceTypeId}")]
        public IEnumerable<DeviceChannelTypeMapping> GetDeviceChannelOptions(Guid deviceTypeId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return db.DeviceChannelTypeMapping.Include(t => t.ChannelType).Where(t => t.DeviceTypeId.Equals(deviceTypeId)).ToList();
            }
        }

        /// <summary>
        /// 排除过滤的数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        private List<IPDeviceInfo> FilterDeleteDevice(AllInOneContext.AllInOneContext dbContext)
        {
            var deleteStatusId = Guid.Parse("a0002016-e009-b019-e001-abcd13800005");
            return dbContext.IPDeviceInfo.Include(t => t.Organization).Include(t => t.DeviceType).
                 Where(t => t.StatusId == null || !t.StatusId.Equals(deleteStatusId)).ToList();
        }

        /// <summary>
        /// 获取可用的设备编号 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="organizationId">设备所属机构</param>
        /// <returns></returns>
        private int NewIpdeviceCode(AllInOneContext.AllInOneContext db, Guid organizationId)
        {
            var deleteStatusId = Guid.Parse("a0002016-e009-b019-e001-abcd13800005");
            var ipdevicecodes = db.IPDeviceInfo.Include(t => t.Organization).Where(t => !deleteStatusId.Equals(t.StatusId)
                  && (t.OrganizationId.Equals(organizationId) || t.Organization.ParentOrganizationId.Equals(organizationId))).
                OrderBy(t => t.IPDeviceCode).Select(t => t.IPDeviceCode).ToList();
            for (int i = 100; i <= 1000; i++)
            {
                if (ipdevicecodes.Contains(i))
                    continue;
                else
                    return i;
            }
            return -1;
        }

    }
}
