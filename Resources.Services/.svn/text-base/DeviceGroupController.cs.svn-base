using AllInOneContext;
using Infrastructure.Model;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resources.Services
{
    [Route("Resources/[controller]")]
    public class DeviceGroupController:Controller
    {
        ILogger<DeviceGroupController> _logger;

        public DeviceGroupController(ILogger<DeviceGroupController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Add([FromBody] DeviceGroup deviceGroup)
        {
            if (deviceGroup == null)
                return BadRequest("DeviceGroup object can not be null!");
            if (ExistsName(deviceGroup.DeviceGroupName, deviceGroup.OrganizationId, deviceGroup.DeviceGroupId))
                return BadRequest("DeviceGroup name has been used!");
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    db.DeviceGroup.Add(deviceGroup);
                    db.SaveChanges();
                    // return CreatedAtRoute("GetById", new { id = deviceGroup.DeviceGroupId }, deviceGroup);
                    return CreatedAtAction("", deviceGroup);
                }
                catch (DbUpdateException dbEx)
                {
                    _logger.LogError("添加设备分组异常：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
                }
                catch (Exception ex)
                {
                    _logger.LogError("添加设备分组异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] DeviceGroup deviceGroup)
        {
            if (deviceGroup == null)
                return BadRequest("DeviceGroup object can not be null!");
            if (ExistsName(deviceGroup.DeviceGroupName, deviceGroup.OrganizationId, deviceGroup.DeviceGroupId))
                return BadRequest("DeviceGroup name has been used!");

            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    db.Update(deviceGroup);
                    db.SaveChanges();
                    return NoContent();
                }
                catch (DbUpdateException dbEx)
                {
                    _logger.LogError("更新设备分组异常：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
                }
                catch (Exception ex)
                {
                    _logger.LogError("更新设备分组异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                DeviceGroup deleteObj = db.DeviceGroup.FirstOrDefault(t => t.DeviceGroupId.Equals(id));
                if (deleteObj == null)
                    return NotFound();
                db.DeviceGroup.Remove(deleteObj);
                db.SaveChanges();
                return NoContent();
            }
        }

        /// <summary>
        /// 获取组织机构下的设备编组
        /// </summary>
        /// <param name="organizationid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Resources/DeviceGroup/organizationId={organizationId}")]
        public IEnumerable<DeviceGroup> GetDeviceGroupByOrganization(Guid organizationId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var deviceGroupList = db.DeviceGroup.Include(t=>t.DeviceGroupType).Where(d => d.OrganizationId.Equals(organizationId)).ToList();
                deviceGroupList.ForEach(t => {
                    if (!string.IsNullOrEmpty(t.DeviceListJson))
                    {
                        var deviceIdList = JsonConvert.DeserializeObject<Guid[]>(t.DeviceListJson);
                        t.DeviceList = db.IPDeviceInfo.Where(p => deviceIdList.Contains(p.IPDeviceInfoId)).ToList();
                    }
                });
                return deviceGroupList;
            }
        }

        [HttpGet]
        public IEnumerable<DeviceGroup> GetAll()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var deviceGroupList = db.DeviceGroup.ToList();
                deviceGroupList.ForEach(t => {
                    if (!string.IsNullOrEmpty(t.DeviceListJson ))
                    {
                        var deviceIdList = JsonConvert.DeserializeObject<Guid[]>(t.DeviceListJson);
                        t.DeviceList = db.IPDeviceInfo.Where(p => deviceIdList.Contains(p.IPDeviceInfoId)).ToList();
                    }
                });
                return deviceGroupList;
            }
        }

        //[HttpGet("{id}",Name = "GetById")]
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var dg = db.DeviceGroup.FirstOrDefault(t => t.DeviceGroupId.Equals(id));
                if (dg == null)
                    return NotFound();
                if (!string.IsNullOrEmpty(dg.DeviceListJson))
                {
                    var deviceIdList = JsonConvert.DeserializeObject<Guid[]>(dg.DeviceListJson);
                    dg.DeviceList = db.IPDeviceInfo.Where(p => deviceIdList.Contains(p.IPDeviceInfoId)).ToList();
                }
                return new OkObjectResult(dg);
            }
        }

        //private bool Exists(Guid deviceGroupid)
        //{
        //    using (var db = new AllInOneContext.AllInOneContext())
        //    {
        //        return db.DeviceGroup.Where(d => d.DeviceGroupId.Equals(deviceGroupid)).Count() > 0;
        //    }
        //}

        /// <summary>
        /// 判断组织机构下的设备组名称是否已存在
        /// </summary>
        /// <param name="name"></param>
        /// <param name="organizationId"></param>
        /// <param name="groupId">设备组id,更新需考虑</param>
        /// <returns></returns>
        public bool ExistsName(string name, Guid organizationId, Guid groupId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return db.DeviceGroup.Where(t => t.DeviceGroupName.Equals(name)
                    && t.OrganizationId.Equals(organizationId)
                    && !groupId.Equals(t.DeviceGroupId)).Count() > 0;
            }
        }
    }
}
