using AllInOneContext;
using Infrastructure.Model;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Infrastructure.Services
{
    [Route("Infrastructure/[controller]")]
    /// <summary>
    /// 系统中心程序控制类
    /// </summary>
    public class ApplicationCenterController : Controller
    {
        private readonly ILogger<ApplicationCenterController> _logger;
        public ApplicationCenterController(ILogger<ApplicationCenterController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 获取所有系统中心信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ApplicationCenter> GetAll()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var data = db.ApplicationCenter;
                return data.ToList();
            }
        }


        /// <summary>
        /// 根据系统中心ID获取应用信息
        /// </summary>
        /// <param name="id">应用ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    ApplicationCenter data = db.ApplicationCenter
                        .FirstOrDefault(p => p.ApplicationCenterId.Equals(id));
                    if (data == null)
                    {
                        return NoContent();
                    }

                    return new ObjectResult(data);
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }


        /// <summary>
        /// 新增系统中心
        /// </summary>
        /// <param name="model">系统中心实体</param>
        /// <returns>返回值</returns>
        [HttpPost]
        public IActionResult Add([FromBody]ApplicationCenter model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.ApplicationCenter.Add(model);
                    db.SaveChanges();
                    return Created("", "OK");
                }
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError("新增系统中心异常：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);

                return BadRequest(new ApplicationException { ErrorCode="DBUpdate",ErrorMessage="数据保存异常:"+dbEx.Message});
            }
            catch (System.Exception ex)
            {
                _logger.LogError("新增系统中心异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 修改系统中心
        /// </summary>
        /// <param name="model">系统中心实体</param>
        /// <returns>返回值</returns>
        [HttpPut]
        public IActionResult Update([FromBody]ApplicationCenter model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.ApplicationCenter.Update(model);
                    db.SaveChanges();
                    return new NoContentResult();
                }
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError("修改系统中心异常：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
            }
            catch (System.Exception ex)
            {
                _logger.LogError("修改系统中心异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        ///  根据系统中心ID删除系统中心
        /// </summary>
        /// <param name="id">系统中心ID</param>
        /// <returns>返回值</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    ApplicationCenter data = db.ApplicationCenter.FirstOrDefault(p => p.ApplicationCenterId == id);
                    if (data == null)
                    {
                        return NoContent();
                    }
                    db.ApplicationCenter.Remove(data);
                    db.SaveChanges();
                    return new NoContentResult();
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError("删除系统中心异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }
    }
}
