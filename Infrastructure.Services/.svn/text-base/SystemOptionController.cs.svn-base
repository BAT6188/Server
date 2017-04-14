/**
 * 2016-12-22 zhrx 系统选项按编号排序
 */ 
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
    /// 系统选项控制类
    /// </summary>
    public class SystemOptionController : Controller
    {
        private readonly ILogger<SystemOptionController> _logger;
        public SystemOptionController(ILogger<SystemOptionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<SystemOption> GetAll()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var data = db.SystemOption
                    .Include(t => t.ApplicationSystemOptions).Include(t=>t.ParentSystemOption);
                return data.ToList();
            }
        }

        /// <summary>
        /// 根据父节点Code获取所有系统选项
        /// </summary>
        /// <param name="systemoptionname">父节点编码/param>
        /// <param name="include">是否包含自身节点/param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Infrastructure/SystemOption/systemOptionCode={systemOptionCode}&include={include}")]
        public IActionResult GetSystemOptionsBySystemOptionCode(string systemOptionCode,bool include)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var data = (db.SystemOption
                        .Include(t => t.ParentSystemOption)
                        .Where(p => p.ParentSystemOption!=null && p.ParentSystemOption.SystemOptionCode.Equals(systemOptionCode)));
                    if (include)
                    {
                        data = data.Union(db.SystemOption.Where(p => p.SystemOptionCode.Equals(systemOptionCode)));
                    }

                    if (data.Count() == 0)
                    {
                        return NoContent();
                    }

                    return new ObjectResult(data.ToList());
                }
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError("GetSystemOptionsBySystemOptionCode：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);

                return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
            }
            catch (System.Exception ex)
            {
                _logger.LogError("GetSystemOptionsBySystemOptionCode：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }


        /// <summary>
        /// 根据父节点ID获取所有系统选项
        /// </summary>
        /// <param name="organizationId">父节点ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Infrastructure/SystemOption/parentSystemOptionId={parentSystemOptionId}")]
        public IActionResult GetSystemOptionsByOrganizationID(Guid parentSystemOptionId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var data = db.SystemOption
                        .Include(t => t.ApplicationSystemOptions).Include(t => t.ParentSystemOption)
                        .Where(p => p.ParentSystemOption != null && p.ParentSystemOptionId.Equals(parentSystemOptionId))
                        .OrderBy(t=>t.SystemOptionCode);

                    if (data.Count() == 0)
                    {
                        return NoContent();
                    }

                    return new ObjectResult(data.ToList());
                }
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError("GetSystemOptionsByOrganizationID：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);

                return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
            }
            catch (System.Exception ex)
            {
                _logger.LogError("GetSystemOptionsByOrganizationID：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }


        /// <summary>
        /// 根据系统选项ID获取信息
        /// </summary>
        /// <param name="id">系统选项ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    SystemOption data = db.SystemOption
                        .Include(t => t.ApplicationSystemOptions)
                        .FirstOrDefault(p => p.SystemOptionId.Equals(id));
                    if (data == null)
                    {
                        return NoContent();
                    }

                    return new ObjectResult(data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Get：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }



        /// <summary>
        /// 新增系统选项
        /// </summary>
        /// <param name="model">系统选项实体</param>
        /// <returns>返回值</returns>
        [HttpPost]
        public IActionResult Add([FromBody]SystemOption model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.SystemOption.Add(model);
                    db.SaveChanges();
                    return Created("", "OK");
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Add：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 修改系统选项
        /// </summary>
        /// <param name="model">系统选项实体</param>
        /// <returns>返回值</returns>
        [HttpPut]
        public IActionResult Update([FromBody]SystemOption model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.SystemOption.Update(model);
                    db.SaveChanges();
                    return new NoContentResult();
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Update：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        ///  根据系统选项ID删除系统选项
        /// </summary>
        /// <param name="id">系统选项ID</param>
        /// <returns>返回值</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    SystemOption data = db.SystemOption.FirstOrDefault(p => p.SystemOptionId == id);
                    if (data == null)
                    {
                        return NoContent();
                    }
                    db.SystemOption.Remove(data);
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
