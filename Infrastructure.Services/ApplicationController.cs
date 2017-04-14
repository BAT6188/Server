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
    /// 系统应用程序控制类
    /// </summary>
    public class ApplicationController : Controller
    {
        private readonly ILogger<ApplicationController> _logger;
        public ApplicationController(ILogger<ApplicationController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 获取所有系统应用信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Application> GetAll()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var data = db.Application
                           .Include(t => t.ApplicationSettings)
                           .Include(t => t.ApplicationSystemOptions)
                           ;
                return data.ToList();
            }
        }


        /// <summary>
        /// 根据系统应用ID获取应用信息
        /// </summary>
        /// <param name="id">应用ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                Application data = db.Application
                                   .Include(t => t.ApplicationSettings)
                                   .Include(t => t.ApplicationSystemOptions)
                                   .FirstOrDefault(p => p.ApplicationId.Equals(id));
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
        public IActionResult Add([FromBody]Application model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.Application.Add(model);
                    db.SaveChanges();
                    return Created("", "OK");
                }
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError("新增系统应用异常：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode="DBUpdate",ErrorMessage="数据保存异常:"+dbEx.Message});
            }
            catch (System.Exception ex)
            {
                _logger.LogError("新增系统应用异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 修改系统应用
        /// </summary>
        /// <param name="model">系统应用实体</param>
        /// <returns>返回值</returns>
        [HttpPut]
        public IActionResult Update([FromBody]Application model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {

                    //建立事务
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        Application application = db.Application
                                           .Include(t => t.ApplicationSettings)
                                           .Include(t => t.ApplicationSystemOptions)
                                           .FirstOrDefault(p => p.ApplicationId.Equals(model.ApplicationId));
                        if (application == null)
                        {
                            return BadRequest();
                        }
                        //
                        RemoveManyToMany(db, application);
                        //
                        application.ApplicationSettings = model.ApplicationSettings;
                        application.ApplicationSystemOptions = model.ApplicationSystemOptions;

                        db.Application.Update(application);
                        db.SaveChanges();

                        transaction.Commit();
                        return new NoContentResult();
                    }
                }
            }
            catch (DbUpdateException dbEx)
            {
                
                _logger.LogError("修改系统应用异常：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
            }
            catch (System.Exception ex)
            {
                _logger.LogError("修改系统应用异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }

        private static void RemoveManyToMany(AllInOneContext.AllInOneContext db, Application application)
        {
            //手动处理ApplicationSetting 一对多的关系，待找到合适的方法后再调整
            if (application.ApplicationSettings != null)
            {
                List<ApplicationSetting> delList = new List<ApplicationSetting>();
                foreach (ApplicationSetting rp in application.ApplicationSettings)
                {
                    ApplicationSetting del = db.ApplicationSetting
                        .FirstOrDefault(p => p.ApplicationSettingId.Equals(rp.ApplicationSettingId));
                    if (del != null)
                    {
                        delList.Add(del);
                    }
                }
                db.ApplicationSetting.RemoveRange(delList);
                db.SaveChanges();
            }
            //手动处理ApplicationSystemOption 多对多的关系，待找到合适的方法后再调整
            if (application.ApplicationSystemOptions != null)
            {
                List<ApplicationSystemOption> delList = new List<ApplicationSystemOption>();
                foreach (ApplicationSystemOption rp in application.ApplicationSystemOptions)
                {
                    ApplicationSystemOption del = db.ApplicationSystemOption
                        .FirstOrDefault(p => p.ApplicationId.Equals(rp.ApplicationId) && p.SystemOptionId.Equals(rp.SystemOptionId));
                    if (del != null)
                    {
                        delList.Add(del);
                    }
                }
                db.ApplicationSystemOption.RemoveRange(delList);
                db.SaveChanges();
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
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        Application application = db.Application.FirstOrDefault(p => p.ApplicationId.Equals(id));
                        if (application == null)
                        {
                            return NoContent();
                        }
                        //
                        RemoveManyToMany(db, application);
                        //
                        db.Application.Remove(application);
                        db.SaveChanges();

                        transaction.Commit();
                        return new NoContentResult();
                    }
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError("删除系统应用异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }
    }
}
