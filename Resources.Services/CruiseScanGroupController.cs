using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Resources.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Infrastructure.Model;
using Infrastructure.Utility;

namespace Resources.Services
{
    [Route("Resources/[controller]")]
    public class CruiseScanGroupController : Controller
    {
        ILogger<CruiseScanGroupController> _logger;

        public CruiseScanGroupController(ILogger<CruiseScanGroupController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Add([FromBody] CruiseScanGroup cruiseScanGroup)
        {
            if (cruiseScanGroup == null)
                return BadRequest("CruiseScanGroup object can not be null!");
            if (ExistsName(cruiseScanGroup.GroupName, cruiseScanGroup.CameraId, cruiseScanGroup.CruiseScanGroupId))
                return BadRequest("CruiseScanGroup name has been used!");
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    db.CruiseScanGroup.Add(cruiseScanGroup);
                    db.SaveChanges();
                    return CreatedAtAction("", cruiseScanGroup);
                }
                catch (DbUpdateException dbEx)
                {
                    _logger.LogError("添加巡航异常：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
                }
                catch (Exception ex)
                {
                    _logger.LogError("添加巡航异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] CruiseScanGroup cruiseScanGroup)
        {
            if (cruiseScanGroup == null)
                return BadRequest("CruiseScanGroup object can not be null!");
            if (ExistsName(cruiseScanGroup.GroupName, cruiseScanGroup.CameraId, cruiseScanGroup.CruiseScanGroupId))
                return BadRequest("CruiseScanGroup name has been used!");
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        //先删除，再更新
                        var dbCruiseScanGroup = db.CruiseScanGroup.Include(t => t.PresetSites).FirstOrDefault(t => t.CruiseScanGroupId.Equals(cruiseScanGroup.CruiseScanGroupId));
                        if (dbCruiseScanGroup.PresetSites != null)
                        {
                            db.Set<CruiseScanGroupPresetSite>().RemoveRange(dbCruiseScanGroup.PresetSites);
                        }
                        db.SaveChanges();

                        foreach (CruiseScanGroupPresetSite ps in cruiseScanGroup.PresetSites)
                        {
                            dbCruiseScanGroup.PresetSites.Add(ps);
                        }
                        //dbCruiseScanGroup.PresetSites = cruiseScanGroup.PresetSites;
                        db.CruiseScanGroup.Update(cruiseScanGroup);
                        db.SaveChanges();
                        tran.Commit();
                        return NoContent();
                    }
                }
                catch (DbUpdateException dbEx)
                {
                    _logger.LogError("更新巡航异常：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
                }
                catch (Exception ex)
                {
                    _logger.LogError("更新巡航异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
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
                    CruiseScanGroup deleteObj = db.CruiseScanGroup.FirstOrDefault(t => t.CruiseScanGroupId.Equals(id));
                    if (deleteObj == null)
                        return NotFound();
                    db.CruiseScanGroup.Remove(deleteObj);
                    db.SaveChanges();
                    return NoContent();
                }
                catch (DbUpdateException dbEx)
                {
                    _logger.LogError("删除巡航异常：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
                }
                catch (Exception ex)
                {
                    _logger.LogError("删除巡航异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
        }

        //[HttpGet("{id}",Name ="GetById")]
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var scanGroup = db.CruiseScanGroup.Include(t => t.PresetSites).ThenInclude(t => t.PresetSite).
                    Where(t => t.CruiseScanGroupId.Equals(id)).FirstOrDefault();
                if (scanGroup == null)
                    return NotFound();
                return new OkObjectResult(scanGroup);
                //return new OkObjectResult(db.CruiseScanGroup.First(t => t.CruiseScanGroupId.Equals(id)));
            }
        }

        /// <summary>
        /// 获取摄像机的巡航组
        /// </summary>
        /// <param name="cameraId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Resources/CruiseScanGroup/cameraId={cameraId}")]
        public IEnumerable<CruiseScanGroup> GetByMonitorSiteId(Guid cameraId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return db.CruiseScanGroup.Include(t => t.PresetSites).ThenInclude(p => p.PresetSite).Where(t => t.CameraId.Equals(cameraId)).ToList();
            }
        }

        [HttpGet]
        public IEnumerable<CruiseScanGroup> GetAll()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return db.CruiseScanGroup.ToList();
            }
        }

        //private bool Exists(Guid deviceid)
        //{
        //    using (var db = new AllInOneContext.AllInOneContext())
        //    {
        //        return db.CruiseScanGroup.Where(t => t.CruiseScanGroupId.Equals(deviceid)).Count() > 0;
        //    }
        //}

        /// <summary>
        /// 判断摄像机巡航组名称是否已存在
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cameraId"></param>
        /// <param name="groupId">预置点id,更新需考虑</param>
        /// <returns></returns>
        public bool ExistsName(string name, Guid cameraId, Guid groupId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return db.CruiseScanGroup.Where(t => t.GroupName.Equals(name)
                    && t.CameraId.Equals(cameraId)
                    && !t.CruiseScanGroupId.Equals(groupId)).Count() > 0;
            }
        }

        /// <summary>
        /// 一次添加多个巡航组
        /// </summary>
        /// <param name="cruiseScanGroups"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/Resources/CruiseScanGroup/Batch")]
        public IActionResult BatchAdd([FromBody] List<CruiseScanGroup> cruiseScanGroups)
        {
            if (cruiseScanGroups == null)
                return BadRequest("CruiseScanGroup object can not be null!");
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    db.CruiseScanGroup.AddRange(cruiseScanGroups);
                    db.SaveChanges();
                    return NoContent();
                }
                catch (DbUpdateException dbEx)
                {
                    _logger.LogError("批量添加巡航异常：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
                }
                catch (Exception ex)
                {
                    _logger.LogError("批量添加巡航异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
        }
    }
}
