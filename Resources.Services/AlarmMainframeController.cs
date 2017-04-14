/**
 * 2016-12-25 调整报警主机和外接设备关系，报警主机
 */ 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resources.Services
{
    [Route("Resources/[controller]")]
    public class AlarmMainframeController : Controller
    {
        ILogger<AlarmMainframeController> _logger = null;

        public AlarmMainframeController(ILogger<AlarmMainframeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Add([FromBody] AlarmMainframe deviceInfo)
        {
            if (deviceInfo == null)
                return BadRequest("AlarmMainframe object can not be null!");

            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    db.AlarmMainframe.Add(deviceInfo);
                    db.SaveChanges();
                    return CreatedAtAction("", deviceInfo);
                }
                catch (Exception ex)
                {
                    _logger.LogError("添加报警主机异常，Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(ex);
                }
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] AlarmMainframe alarmMainframe)
        {
            if (alarmMainframe == null)
                return BadRequest("AlarmMainframe object can not be null!");

            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    var obj = GetQuery(db).FirstOrDefault(t => t.AlarmMainframeId.Equals(alarmMainframe.AlarmMainframeId));
                    //已删除的防区设备
                    var deleteStatus = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("13800005"));
                    if (obj.AlarmPeripherals != null)
                    {
                        if (alarmMainframe.AlarmPeripherals == null)
                            obj.AlarmPeripherals.ForEach(t => t.AlarmDevice.StatusId = deleteStatus.SystemOptionId);
                        else
                        {
                            var uiAlarmPeripheralIds = alarmMainframe.AlarmPeripherals.Select(t => t.AlarmPeripheralId).ToList();
                            obj.AlarmPeripherals.ForEach(t =>
                            {
                                if (!uiAlarmPeripheralIds.Contains(t.AlarmPeripheralId))
                                {
                                    t.AlarmDevice.StatusId = deleteStatus.SystemOptionId; //已删除
                                }
                            });
                        }
                    }

                    //防区设备更新
                    if (alarmMainframe.AlarmPeripherals != null)
                    {
                        if (obj.AlarmPeripherals == null)
                            obj.AlarmPeripherals = alarmMainframe.AlarmPeripherals;
                        else
                        {
                            var dbAlarmPeripherals = obj.AlarmPeripherals.Select(t => t.AlarmPeripheralId).ToList();
                            alarmMainframe.AlarmPeripherals.ForEach(t =>
                            {
                                if (!dbAlarmPeripherals.Contains(t.AlarmPeripheralId))
                                {
                                    obj.AlarmPeripherals.Add(t);
                                }
                                else //已添加，更新
                                {
                                    var dbAlarmPeripheral = obj.AlarmPeripherals.FirstOrDefault(f => f.AlarmPeripheralId.Equals(t.AlarmPeripheralId));
                                    dbAlarmPeripheral.AlarmDevice.DeviceTypeId = t.AlarmDevice.DeviceTypeId;
                                    dbAlarmPeripheral.AlarmDevice.IPDeviceName = t.AlarmDevice.IPDeviceName;
                                    dbAlarmPeripheral.AlarmTypeId = t.AlarmTypeId;
                                    dbAlarmPeripheral.AlarmChannel = t.AlarmChannel;
                                    dbAlarmPeripheral.DefendArea = t.DefendArea;
                                }
                            });
                        }
                    }
                    obj.DeviceInfo.IPDeviceCode = alarmMainframe.DeviceInfo.IPDeviceCode;
                    obj.DeviceInfo.EndPointsJson = alarmMainframe.DeviceInfo.EndPointsJson;
                    obj.DeviceInfo.IPDeviceName = alarmMainframe.DeviceInfo.IPDeviceName;
                    obj.DeviceInfo.ModifiedByUserId = alarmMainframe.DeviceInfo.ModifiedByUserId;
                    obj.DeviceInfo.OrganizationId = alarmMainframe.DeviceInfo.OrganizationId;
                    obj.DeviceInfo.Modified = DateTime.Now;
                    obj.DeviceInfo.DeviceTypeId = alarmMainframe.DeviceInfo.DeviceTypeId;
                    db.AlarmMainframe.Update(obj);
                    db.SaveChanges();
                    return NoContent();
                }
                catch (Exception ex)
                {
                    _logger.LogError("更新报警主机异常，Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(ex);
                }
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    var deleteStatusId = Guid.Parse("a0002016-e009-b019-e001-abcd13800005");
                    var deleteObj = GetQuery(db).FirstOrDefault(t => t.AlarmMainframeId.Equals(id));
                    if (deleteObj == null || !deleteObj.AlarmMainframeId.Equals(id))
                        return NotFound();
                    //改为删除状态
                    deleteObj.DeviceInfo.StatusId = deleteStatusId;
                    //报警外设设备删除
                    if (deleteObj.AlarmPeripherals != null)
                        deleteObj.AlarmPeripherals.ForEach(t => t.AlarmDevice.StatusId = deleteStatusId);
                    db.SaveChanges();
                    return NoContent();
                    //IPDeviceInfo deleteObj = db.IPDeviceInfo.FirstOrDefault(t => t.IPDeviceInfoId.Equals(id));
                    //if (deleteObj == null || !deleteObj.IPDeviceInfoId.Equals(id))
                    //    return NotFound();
                    //db.IPDeviceInfo.Remove(deleteObj);
                    //db.SaveChanges();
                    //return NoContent();
                }
                catch (Exception ex)
                {
                    _logger.LogError("删除报警主机异常，Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(ex);
                }
            }
        }

        //   [HttpGet("{id}",Name ="GetById")]
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                //IPDeviceInfo device = db.IPDeviceInfo.FirstOrDefault(t => t.IPDeviceInfoId.Equals(id));
                //if (device == null || !device.IPDeviceInfoId.Equals(id))
                //    return NotFound();
                //return new ObjectResult(device);

                var obj = GetQuery(db).FirstOrDefault(t => t.AlarmMainframeId.Equals(id));
                if (obj != null)
                {
                    List<AlarmMainframe> alarmFrames = new List<AlarmMainframe>();
                    alarmFrames.Add(obj);
                    FilterDeletedAlarmPeripherals(alarmFrames);
                }
                return new ObjectResult(obj);
            }
        }

        /// <summary>
        /// 获取组织机构的所属报警主机
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Resources/AlarmMainframe/organizationId={organizationId}")]
        public IEnumerable<AlarmMainframe> GetByOrganization(Guid organizationId, bool includeLower = false)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                //return db.IPDeviceInfo.Include(t => t.Organization).Include(t => t.DeviceType).
                //    Where(t => t.Organization.OrganizationId == organizationId &&
                //    t.DeviceType.ParentSystemOptionId.Equals(Guid.Parse("A0002016-E009-B019-E001-ABCD11000003"))).ToList();
                List<AlarmMainframe> mainframes = new List<AlarmMainframe>();
                if (includeLower)
                {
                    var org = db.Organization.FirstOrDefault(t => t.OrganizationId.Equals(organizationId));
                    if (org != null)
                    {
                        mainframes = GetQuery(db).Where(t => t.DeviceInfo.Organization.OrganizationFullName.
                          Contains(org.OrganizationFullName)).
                          OrderBy(t => t.DeviceInfo.IPDeviceName).ToList();
                    }
                }
                else
                {
                    mainframes = GetQuery(db).Where(t => t.DeviceInfo.OrganizationId.Equals(organizationId)).OrderBy(t => t.DeviceInfo.IPDeviceName).ToList();
                }
                FilterDeletedAlarmPeripherals(mainframes);
                return mainframes;
            }
        }

        /// <summary>
        ///获取所有报警主机
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<AlarmMainframe> GetAll()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var alarmFrames = GetQuery(db).ToList();
                FilterDeletedAlarmPeripherals(alarmFrames);
                return alarmFrames;
            }
        }

        /// <summary>
        /// 根据报警主机类型过滤查询
        /// </summary>
        /// <param name="systemOptionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Resources/AlarmMainframe/systemOptionId={systemOptionId}")]
        public IEnumerable<AlarmMainframe> GetByDeviceType(Guid systemOptionId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var alarmFrames = GetQuery(db).Where(t => systemOptionId == t.DeviceInfo.DeviceTypeId).ToList();
                FilterDeletedAlarmPeripherals(alarmFrames);
                return alarmFrames;
            }
        }

        /// <summary>
        /// 获取用户可操作的设备
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        //ipdeviceIno/userid=...
        [Route("~/Resources/AlarmMainframe/userid={userId}")]
        public IEnumerable<AlarmMainframe> GetByUser(Guid userId)
        {
            return null;
        }

        private IQueryable<AlarmMainframe> GetQuery(AllInOneContext.AllInOneContext dbContext)
        {
            var deleteStatusId = Guid.Parse("a0002016-e009-b019-e001-abcd13800005");
            return dbContext.AlarmMainframe.Include(t => t.DeviceInfo).ThenInclude(t => t.Organization)
                 .Include(t => t.DeviceInfo).ThenInclude(t => t.DeviceType)
                 .Include(t => t.AlarmPeripherals).ThenInclude(t=>t.AlarmDevice)
                 .Include(t => t.AlarmPeripherals).ThenInclude(t => t.AlarmType)
                 .Where(t => !deleteStatusId.Equals(t.DeviceInfo.StatusId));
        }

        /// <summary>
        /// 移除报警主机中已删除的防区设备
        /// </summary>
        private void FilterDeletedAlarmPeripherals(List<AlarmMainframe> mainFrames)
        {
            if (mainFrames != null)
                mainFrames.ForEach(t =>
                {
                    if (t.AlarmPeripherals != null)
                    {
                        var deleteStatusId = Guid.Parse("a0002016-e009-b019-e001-abcd13800005");
                        //移除已经删除的防区设备....
                        int i = t.AlarmPeripherals.Count - 1;
                        for (int j = i; j >= 0; j--)
                        {
                            if (deleteStatusId.Equals(t.AlarmPeripherals[j].AlarmDevice.StatusId))
                            {
                                t.AlarmPeripherals.RemoveAt(j);
                            }
                        }
                    }
                });
        }
    }
}
