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
    /// 权限范围控制类
    /// </summary>
    public class PermissionController : Controller
    {
        private readonly ILogger<PermissionController> _logger;
        public PermissionController(ILogger<PermissionController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 获取所有权限范围集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Permission> GetAll()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var data = db.Permission
                    .Include(t => t.RolePermissions).Include(t => t.ResourcesAction)
                    .Include(t => t.Resource).ThenInclude(t => t.Actions);
                return data.ToList();
            }
        }

        /// <summary>
        /// 根据资源ID获取权限范围
        /// </summary>
        /// <param name="resourceId">资源ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Infrastructure/Permission/resourceId={resourceId}")]
        public IActionResult GetByResourceID(Guid resourceId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var data = db.Permission
                    .Include(t => t.RolePermissions).Include(t => t.ResourcesAction)
                    .Include(t => t.Resource).ThenInclude(t => t.Actions)
                    .Where(p => p.ResourceId.Equals(resourceId));
                if (data.Count() == 0)
                {
                    return NoContent();
                }

                return new ObjectResult(data.ToList());
            }
        }

        /// <summary>
        /// 根据权限范围ID获取权限范围
        /// </summary>
        /// <param name="id">权限范围ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                Permission data = db.Permission
                    .Include(t => t.RolePermissions).Include(t => t.ResourcesAction)
                    .Include(t => t.Resource).ThenInclude(t => t.Actions)
                    .FirstOrDefault(p => p.PermissionId.Equals(id));
                if (data == null)
                {
                    return NoContent();
                }

                return new ObjectResult(data);
            }
        }


        /// <summary>
        /// 新增权限范围
        /// </summary>
        /// <param name="model">权限范围实体</param>
        /// <returns>返回值</returns>
        [HttpPost]
        public IActionResult Add([FromBody]Permission model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.Permission.Add(model);
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
        /// 修改权限范围
        /// </summary>
        /// <param name="model">权限范围实体</param>
        /// <returns>返回值</returns>
        [HttpPut]
        public IActionResult Update([FromBody]Permission model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.Permission.Update(model);
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
        ///  根据权限范围ID删除权限范围
        /// </summary>
        /// <param name="id">权限范围ID</param>
        /// <returns>返回值</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    Permission data = db.Permission.FirstOrDefault(p => p.PermissionId == id);
                    if (data == null)
                    {
                        return NoContent();
                    }
                    db.Permission.Remove(data);
                    db.SaveChanges();
                    return new NoContentResult();
                }
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError("Delete：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);

                return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Delete：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }
    }
}
