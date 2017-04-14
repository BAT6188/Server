using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resources.Data;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resources.Services
{
    [Route("Resources/[controller]")]
    public class AlarmPeripheralController : Controller
    {
        private readonly ILogger<AlarmPeripheralController> _logger;
        public AlarmPeripheralController(ILogger<AlarmPeripheralController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Add([FromBody]AlarmPeripheral peripheral)
        {
            if (peripheral == null)
                return BadRequest("AlarmPeripheral object can not be null!");
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    db.AlarmPeripheral.Add(peripheral);
                    db.SaveChanges();
                    return CreatedAtAction("", peripheral);
                }
                catch (Exception ex)
                {
                    _logger.LogError("添加报警外设设备异常，Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(ex);
                }
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody]AlarmPeripheral peripheral)
        {
            if (peripheral == null)
                return BadRequest("AlarmPeripheral object can not be null!");
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    db.AlarmPeripheral.Update(peripheral);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    _logger.LogError("更新报警外设设备异常，Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(ex.Message);
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    AlarmPeripheral editObj = db.AlarmPeripheral.FirstOrDefault(s => s.AlarmPeripheralId.Equals(id));
                    if (editObj == null || !editObj.AlarmPeripheralId.Equals(id))
                        return NotFound();
                    IPDeviceInfo alarmDevice = db.IPDeviceInfo.First(s => s.IPDeviceInfoId.Equals(editObj.AlarmDeviceId));
                    db.AlarmPeripheral.Remove(editObj);
                    db.IPDeviceInfo.Remove(alarmDevice);
                    db.SaveChanges();
                    return NoContent();
                }
                catch (Exception ex)
                {
                    _logger.LogError("删除报警外设异常：Message:{0}\r\nStackTrace:{0}", ex.Message, ex.StackTrace);
                    return BadRequest(ex);
                }
            }
        }

        //[HttpGet("{id}",Name ="GetById")]
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            _logger.LogInformation("Get alarmperihperal info by id {0}", id);

            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    AlarmPeripheral si = db.AlarmPeripheral.//Include(t => t.AlarmMainframe).
                        Include(t => t.AlarmDevice).FirstOrDefault(t => t.AlarmPeripheralId.Equals(id));
                    if (si == null || !si.AlarmPeripheralId.Equals(Guid.NewGuid()))
                    {
                        return NotFound();
                    }
                    return new ObjectResult(si);
                }
                catch (Exception ex)
                {
                    _logger.LogError("获取报警外设异常：Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(ex);
                }
            }
        }

        [HttpGet]
        [Route("~/Resources/AlarmPeripheral/organizationId={organizationId}")]
        public IEnumerable<AlarmPeripheral> GetByOrganization(Guid organizationId)
        {
            //return _serverList.Where(s => s.Organization.OrganizationId.Equals(organizationid));
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return db.AlarmPeripheral./*Include(t => t.AlarmMainframe).*/Include(t => t.AlarmDevice).
                    Where(t => t.AlarmDevice.OrganizationId.Equals(organizationId)).ToList();
            }
        }

        [HttpGet]
        public IEnumerable<AlarmPeripheral> GetAll()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return db.AlarmPeripheral./*Include(t => t.AlarmMainframe).*/Include(t => t.AlarmDevice).ToList();
            }
        }

        /// <summary>
        /// 外接设备视图数据，常规报警主机
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Resources/alarmperipheral/View")]
        public IEnumerable<AlarmPeripheralView> GetAlarmPeripheralView()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                //var list = from t in db.AlarmPeripheral.Include(t => t.AlarmMainframe).Include(t => t.AlarmDevice).ToList()
                //           select new AlarmPeripheralView {
                //               DeviceId = t.AlarmDeviceId,
                //               DeviceName = t.AlarmDevice.IPDeviceName,
                //               AlarmTypeId = t.AlarmTypeId,
                //               AlarmChannel = t.AlarmChannel,
                //               MainFrameNo = t.AlarmMainframe.DeviceInfo.IPDeviceCode
                //           };
                //return list.ToArray();

                List<AlarmPeripheralView> alarmPeripheralViews = new List<AlarmPeripheralView>();
                var mainframes = db.AlarmMainframe.Include(t => t.DeviceInfo).Include(t => t.AlarmPeripherals).ThenInclude(t => t.AlarmDevice)
                    .Where(t => t.DeviceInfo.DeviceTypeId.Equals(Guid.Parse("a0002016-e009-b019-e001-ab1100000302"))).ToList();
                mainframes.ForEach(f => {
                               f.AlarmPeripherals.ForEach(j =>
                               {
                                   alarmPeripheralViews.Add(new AlarmPeripheralView
                                   {
                                       DeviceId = j.AlarmDeviceId,
                                       DeviceName = j.AlarmDevice.IPDeviceName,
                                       AlarmTypeId = j.AlarmTypeId,
                                       AlarmChannel = j.AlarmChannel,
                                       MainFrameNo = f.DeviceInfo.IPDeviceCode
                                   });
                               });
                    });
                return alarmPeripheralViews;
            }
        }

    }
}
