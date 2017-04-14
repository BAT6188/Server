/*
 * 2016-12-15 zhrx 修复更新编码器失败bug,编码器ip变化时同步修改摄像机ip
 */
using AllInOneContext;
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
    [Route("Resources/[controller]")]
    public class EncoderController : Controller
    {
        ILogger<EncoderController> _logger;

        public EncoderController(ILogger<EncoderController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Add([FromBody] Encoder encoder)
        {
            try
            {
                if (encoder == null)
                    return BadRequest();
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.Encoder.Add(encoder);
                    db.SaveChanges();
                    // return CreatedAtRoute("GetById",new { id = encoder.EncoderId }, encoder);
                    return CreatedAtAction("", encoder);
                }
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError("添加编码器异常：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError("添加编码器异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] Encoder encoder)
        {
            try
            {
                if (encoder == null)
                    return BadRequest();
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.Encoder.Update(encoder);
                    db.SaveChanges();
                    //同时更新摄像机IP
                    db.Set<Camera>().Include(t => t.IPDevice).Where(t => t.EncoderId.Equals(encoder.EncoderId)).
                        ForEachAsync(f => f.IPDevice.EndPointsJson = encoder.DeviceInfo.EndPointsJson).Wait();
                    db.SaveChanges();
                    return NoContent();
                }
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError("修改编码器异常：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
            }
            catch (System.Exception ex)
            {
                _logger.LogError("修改编码器异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    Encoder deleteObj = CreateDbQuery(db).FirstOrDefault(t => t.EncoderId.Equals(id));
                    if (deleteObj == null || !deleteObj.EncoderId.Equals(id))
                    {
                        return NotFound();
                    }
                    db.Remove(deleteObj);
                    db.SaveChanges();
                    return NoContent();
                }
                catch (Exception ex)
                {
                    _logger.LogError("删除编码器异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
        }

        // [HttpGet("{id}",Name ="GetById")]
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    Encoder obj = CreateDbQuery(db).FirstOrDefault(t => t.EncoderId.Equals(id));
                    if (obj == null)
                    {
                        return NotFound();
                    }
                    return new ObjectResult(obj);
                }
                catch (System.Exception ex)
                {
                    _logger.LogError("查询编码器异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
        }

        [HttpGet]
        [Route("~/Resources/Encoder/organizationId={organizationId}")]
        public IEnumerable<Encoder> GetByOrganization(Guid organizationId, bool includeLower = false)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                if (includeLower)
                {
                    var org = db.Organization.FirstOrDefault(t => t.OrganizationId.Equals(organizationId));
                    if (org != null)
                        return CreateDbQuery(db).Where(t => t.DeviceInfo.Organization.OrganizationFullName.Contains(org.OrganizationFullName)).ToList();
                    return null;
                }
                else
                {
                    return CreateDbQuery(db).Where(t => t.DeviceInfo.Organization.OrganizationId == organizationId).ToList();
                }
            }
        }

        [HttpGet]
        public IEnumerable<Encoder> GetAll()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return CreateDbQuery(db).ToList();
            }
        }

        private IQueryable<Encoder> CreateDbQuery(AllInOneContext.AllInOneContext dbContext)
        {
            return dbContext.Encoder.Include(t => t.DeviceInfo).ThenInclude(t => t.Organization).Include(t => t.EncoderType).Include(t => t.DeviceInfo.DeviceType);
        }

        [HttpGet]
        [Route("~/Resources/Encoder/EncoderType")]
        public IEnumerable<EncoderType> GetAllEncoderType()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return db.Set<EncoderType>().ToList();
            }
        }

        ///// <summary>
        ///// 更新编码器类型，供dcp调用
        ///// </summary>
        ///// <param name="encoderCode"></param>
        ///// <param name="encoderId"></param>
        ///// <returns></returns>
        //[HttpPut]
        //[Route("~/Resources/encoder/encoderCode={encoderCode}")]
        //public IActionResult UpdateEncoderType(int encoderCode,Guid encoderId)
        //{
        //    using (var db = new AllInOneContext.AllInOneContext())
        //    {
        //       Encoder en =  db.Encoder.FirstOrDefault(t => t.EncoderId.Equals(encoderId));
        //        if (en == null)
        //        {
        //            return NotFound();
        //        }
        //        EncoderType ent = db.Set<EncoderType>().FirstOrDefault(t => t.EncoderCode == encoderCode);
        //        if (ent == null)
        //            return BadRequest(new ApplicationException() { ErrorCode = "Unknown", ErrorMessage="错误的编码器类型" });
        //        en.EncoderType = ent;
        //        db.Encoder.Update(en);
        //        db.SaveChanges();
        //        return Ok();
        //    }
        //}

        ///// <summary>
        ///// 设备ip更新，适用于智敏类型，自动检测设备ip
        ///// </summary>
        ///// <param name="endPointsJson"></param>
        ///// <param name="encoderId"></param>
        ///// <returns></returns>
        //[HttpPut]
        //[Route("~/Resources/encoder/endPointsJson={endPointsJson}")]
        //public IActionResult UpdateDeviceInfo(string endPointsJson,Guid encoderId)
        //{
        //    using (var db = new AllInOneContext.AllInOneContext())
        //    {
        //        var encoder = db.Encoder.Include(t => t.DeviceInfo).FirstOrDefault(t => t.EncoderId.Equals(encoderId));
        //        if (encoder != null)
        //        {
        //            IPDeviceInfo device = encoder.DeviceInfo;
        //            device.EndPointsJson = endPointsJson;
        //            db.IPDeviceInfo.Update(device);
        //            db.SaveChanges();
        //            return Ok();
        //        }
        //        else
        //            return NotFound();
        //    }
        //}

        /// <summary>
        /// 更新编码器类型或ip信息，供dcp调用
        /// </summary>
        /// <param name="encoder"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("~/Resources/Encoder/View")]
        public IActionResult Update([FromBody] EncoderView encoder)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    if (encoder == null)
                        return BadRequest();
                    Encoder en = db.Encoder.Include(t => t.DeviceInfo).FirstOrDefault(t => t.EncoderId.Equals(encoder.EncoderId));
                    if (en == null)
                    {
                        return NotFound();
                    }
                    if (encoder.EncoderType > 0)
                    {
                        EncoderType ent = db.Set<EncoderType>().FirstOrDefault(t => t.EncoderCode == encoder.EncoderType);
                        if (ent == null)
                            return BadRequest(new ApplicationException() { ErrorCode = "Unknown", ErrorMessage = "错误的编码器类型" });
                        en.EncoderTypeId = ent.EncoderTypeId;
                    }
                    if (encoder.EndPoints != null && encoder.EndPoints.Count > 0)
                    {
                        en.DeviceInfo.EndPoints = encoder.EndPoints;
                    }
                    db.Encoder.Update(en);
                    db.SaveChanges();
                    return Ok();
                }
                catch (DbUpdateException dbEx)
                {
                    _logger.LogError("修改编码器异常：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
                }
                catch (System.Exception ex)
                {
                    _logger.LogError("修改编码器异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
        }

    }
}
