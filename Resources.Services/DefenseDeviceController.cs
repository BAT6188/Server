using Infrastructure.Model;
using Infrastructure.Utility;
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
    /// <summary>
    /// 
    /// </summary>
    [Route("Resources/[controller]")]
    public class DefenseDeviceController : Controller
    {
        ILogger<DefenseDeviceController> _logger;

        public DefenseDeviceController(ILogger<DefenseDeviceController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Add([FromBody] DefenseDevice device)
        {
            if (device == null)
                return BadRequest("DefenseDevice object can not be null!");
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    db.DefenseDevice.Add(device);
                    db.SaveChanges();
                    return CreatedAtAction("", device);
                }
                catch (DbUpdateException dbEx)
                {
                    _logger.LogError("添加防区设备异常：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
                }
                catch (Exception ex)
                {
                    _logger.LogError("添加防区设备异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] DefenseDevice device)
        {
            if (device == null)
                return BadRequest("DefenseDevice object can not be null!");

            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    db.DefenseDevice.Update(device);
                    db.SaveChanges();
                    return NoContent();
                }
                catch (DbUpdateException dbEx)
                {
                    _logger.LogError("更新防区设备异常：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
                }
                catch (Exception ex)
                {
                    _logger.LogError("更新防区设备异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                DefenseDevice deleteObj = db.DefenseDevice.FirstOrDefault(t => t.DefenseDeviceId.Equals(id));
                if (deleteObj == null)
                    return NotFound();
                db.DefenseDevice.Remove(deleteObj);
                db.SaveChanges();
            }
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                DefenseDevice obj = GetQuery(db).FirstOrDefault(t => t.DefenseDeviceId.Equals(id));
                if (obj == null)
                    return NotFound();
                return new ObjectResult(obj);
            }
        }

        [HttpGet]
        [Route("~/Resources/DefenseDevice/organizationId={organizationId}")]
        public IEnumerable<DefenseDevice> GetByOrganization(Guid organizationId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return GetQuery(db).Where(t => t.DeviceInfo.OrganizationId.Equals(organizationId)).ToList();
            }
        }
          
        //[HttpGet]
        //[Route("~/Resources/defensedevice/View/organizationId={organizationId}")]
        //public IEnumerable<DefenseDeviceView> GetDefenseDeviceViewByOrganization(Guid organizationId)
        //{
        //    List<DefenseDeviceView> devices = new List<DefenseDeviceView>();
        //    using (var db = new AllInOneContext.AllInOneContext())
        //    {
        //        var list = GetQuery(db).Where(t => t.DeviceInfo.OrganizationId.Equals(organizationId)).ToList();
        //        list.ForEach(t=>{
        //            DefenseDeviceView ddv = new DefenseDeviceView() {
        //                DeviceInfo = t.DeviceInfo,
        //                DefenseNo = t.DefenseNo,
        //                AlarmIn = t.AlarmIn,
        //                AlarmInNormalOpen = t.AlarmInNormalOpen,
        //                AlarmOut = t.AlarmOut
        //            };
        //            devices.Add(ddv);
        //        });

        //        return devices;
        //    }
        //}

        [HttpGet]
        public IEnumerable<DefenseDevice> GetAll()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return GetQuery(db).ToList();
            }
        }

        private IQueryable<DefenseDevice> GetQuery(AllInOneContext.AllInOneContext dbContext)
        {
            //return dbContext.DefenseDevice.Include(t => t.DeviceInfo).ThenInclude(t => t.DeviceType).Include(t => t.Sentinel).ThenInclude(t => t.DeviceInfo).Include(t=>t.DefenseDirection);

            var deleteStatusId = Guid.Parse("a0002016-e009-b019-e001-abcd13800005");
            return dbContext.DefenseDevice.Include(t => t.DeviceInfo).ThenInclude(t => t.DeviceType).Include(t => t.DefenseDirection).
                Where(t => t.DeviceInfo.StatusId == null || t.DeviceInfo.StatusId.Equals(deleteStatusId));
        }
    }
}
