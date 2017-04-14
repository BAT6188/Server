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
    /// 应用设置控制类
    /// </summary>
    public class ApplicationSettingController : Controller
    {
        private readonly ILogger<ApplicationSettingController> _logger;
        public ApplicationSettingController(ILogger<ApplicationSettingController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 获取所有系统应用设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ApplicationSetting> GetAll()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var data = db.ApplicationSetting;
                return data.ToList();
            }
        }


        /// <summary>
        /// 根据系统应用ID获取应用信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                ApplicationSetting data = db.ApplicationSetting.FirstOrDefault(p => p.ApplicationSettingId.Equals(id));
                if (data == null)
                {
                    return NoContent();
                }
                return new ObjectResult(data);
            }
        }


        /// <summary>
        /// 新增系统应用
        /// </summary>
        /// <param name="model">系统应用实体</param>
        /// <returns>返回值</returns>
        [HttpPost]
        public IActionResult Add([FromBody]ApplicationSetting model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.ApplicationSetting.Add(model);
                    db.SaveChanges();
                    return Created("", "OK");
                }
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError("Add：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Add：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 修改系统应用
        /// </summary>
        /// <param name="model">系统应用实体</param>
        /// <returns>返回值</returns>
        [HttpPut]
        public IActionResult Update([FromBody]ApplicationSetting model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.ApplicationSetting.Update(model);
                    db.SaveChanges();
                    return new NoContentResult();
                }
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError("Update：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Update：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        ///  根据系统应用ID删除系统应用
        /// </summary>
        /// <param name="id">系统应用ID</param>
        /// <returns>返回值</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    ApplicationSetting data = db.ApplicationSetting.FirstOrDefault(p => p.ApplicationSettingId == id);
                    if (data == null)
                    {
                        return NoContent();
                    }
                    db.ApplicationSetting.Remove(data);
                    db.SaveChanges();
                    return new NoContentResult();
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Delete：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }
    }
}
