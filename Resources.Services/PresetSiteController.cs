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
    /// <summary>
    /// 预置点管理
    /// </summary>
    [Route("Resources/[controller]")]
    public class PresetSiteController : Controller
    {
        ILogger<PresetSiteController> _logger;

        public PresetSiteController(ILogger<PresetSiteController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Add([FromBody] PresetSite presetSite)
        {
            if (presetSite == null)
                return BadRequest("PresetSite object can not be null!");
            if (ExistsName(presetSite.PresetSizeName, presetSite.CameraId, presetSite.PresetSiteId))
                return BadRequest("PresetSite name has been used!");
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    db.PresetSite.Add(presetSite);
                    db.SaveChanges();
                    //  return CreatedAtRoute("GetById", new { id = presetSite.PresetSiteId }, presetSite);
                    return CreatedAtAction("", presetSite);
                }
                catch (DbUpdateException dbEx)
                {
                    _logger.LogError("添加预置点异常：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
                }
                catch (Exception ex)
                {
                    _logger.LogError("添加预置点异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] PresetSite presetSite)
        {
            if (presetSite == null)
                return BadRequest("PresetSite object can not be null!");
            if (ExistsName(presetSite.PresetSizeName, presetSite.CameraId, presetSite.PresetSiteId))
                return BadRequest("PresetSite name has been used!");

            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    db.PresetSite.Update(presetSite);
                    db.SaveChanges();
                    return NoContent();
                }
                catch (DbUpdateException dbEx)
                {
                    _logger.LogError("更新预置点异常：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
                }
                catch (Exception ex)
                {
                    _logger.LogError("更新预置点异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                PresetSite deleteObj = db.PresetSite.FirstOrDefault(t => t.PresetSiteId.Equals(id));
                if (deleteObj == null)
                {
                    return NotFound();
                }
                db.PresetSite.Remove(deleteObj);
                db.SaveChanges();
                return NoContent();
            }
        }

        //    [HttpGet("{id}",Name ="GetById")]
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var preset = db.PresetSite.FirstOrDefault(t => t.PresetSiteId.Equals(id));
                if (preset == null)
                    return NotFound();
                return new OkObjectResult(preset);
            }
        }

        /// <summary>
        /// 获取摄像机的巡航组
        /// </summary>
        /// <param name="cameraId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Resources/PresetSite/cameraId={cameraId}")]
        public IEnumerable<PresetSite> GetByCameraId(Guid cameraId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return db.PresetSite.Where(t => t.CameraId.Equals(cameraId)).ToList();
            }
        }

        /// <summary>
        /// 获取摄像机的巡航组
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Resources/PresetSite/deviceId={deviceId}")]
        public IEnumerable<PresetSite> GetByDeviceId(Guid deviceId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var camera = db.Set<Camera>().FirstOrDefault(t => t.IPDeviceId.Equals(deviceId));
                if (camera != null)
                {
                    var cameraId = camera.CameraId;
                    using (var db2 = new AllInOneContext.AllInOneContext())  //db具有跟踪功能，不从新从数据库中查询，返回记录只有1条，原因待跟进
                    {
                        return db2.PresetSite.Where(t => t.CameraId.Equals(cameraId)).ToList();
                    }
                }
                else
                    return null;
            }
        }

        [HttpGet]
        public IEnumerable<PresetSite> GetAll()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return db.PresetSite.ToList();
            }
        }

        //private bool Exists(Guid deviceid)
        //{
        //    using (var db = new AllInOneContext.AllInOneContext())
        //    {
        //        return db.PresetSite.Where(t => t.PresetSiteId.Equals(deviceid)).Count() > 0;
        //    }
        //}

        /// <summary>
        /// 判断摄像机预置点名称是否已存在
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cameraId"></param>
        /// <param name="presetSiteId">预置点id,更新需考虑</param>
        /// <returns></returns>
        public bool ExistsName(string name, Guid cameraId, Guid presetSiteId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return db.PresetSite.Where(t => t.PresetSizeName.Equals(name)
                    && t.CameraId.Equals(cameraId)
                    && !t.PresetSiteId.Equals(presetSiteId)).Count() > 0;
            }
        }
    }
}
