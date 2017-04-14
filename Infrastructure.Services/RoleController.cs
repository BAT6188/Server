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
    /// 角色控制类
    /// </summary>
    public class RoleController : Controller
    {
        private readonly ILogger<RoleController> _logger;
        public RoleController(ILogger<RoleController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Role> GetAllRole()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var data = db.Role
                    .Include(t => t.RolePermissions)
                    .Include(t => t.UserManyToRole)
                    .Include(t=>t.Organization)
                    .Include(t=>t.Application)
                    .Include(t=>t.ControlResourcesType);
                return data.ToList();
            }
        }

        /// <summary>
        /// 根据组织机构ID获取所有角色
        /// </summary>
        /// <param name="organizationId">组织机构ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Infrastructure/Role/organizationId={organizationId}")]
        public IActionResult GetRoleByOrganizationID(Guid organizationId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var data = db.Role
                        .Include(t => t.RolePermissions).Include(t => t.UserManyToRole)
                        .Include(t => t.Organization).Include(t => t.Application)
                        .Include(t => t.ControlResourcesType)
                    .FirstOrDefault(p => p.Organization.OrganizationId.Equals(organizationId));
                    if (data == null)
                    {
                        return NoContent();
                    }

                    return new ObjectResult(data);
                }
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError("GetRoleByOrganizationID：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);

                return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
            }
            catch (System.Exception ex)
            {
                _logger.LogError("GetRoleByOrganizationID：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 根据应用ID获取所有角色
        /// </summary>
        /// <param name="applicationId">应用ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Infrastructure/Role/applicationId={applicationId}")]
        public IActionResult GetRoleByApplicationID(Guid applicationId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var data = db.Role
                        .Include(t => t.RolePermissions).Include(t => t.UserManyToRole)
                        .Include(t => t.Organization).Include(t => t.Application)
                        .Include(t => t.ControlResourcesType)
                        .Where(p => p.Application.ApplicationId.Equals(applicationId));
                    if (data == null)
                    {
                        return NoContent();
                    }

                    return new ObjectResult(data.ToList());
                }
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError("GetRoleByApplicationID：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);

                return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
            }
            catch (System.Exception ex)
            {
                _logger.LogError("GetRoleByApplicationID：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 根据用户ID获取所有角色
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Infrastructure/Role/userId={userId}")]
        public IActionResult GetRoleByUserID(Guid userId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var data = db.Role
                        .Include(t => t.RolePermissions).Include(t => t.UserManyToRole)
                        .Include(t => t.Organization).Include(t => t.Application)
                        .Include(t => t.ControlResourcesType)
                        .Where(p => p.UserManyToRole.Where(t => t.UserId.Equals(userId)) != null);
                    if (data == null)
                    {
                        return NoContent();
                    }

                    return new ObjectResult(data.ToList());
                }
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError("GetRoleByUserID：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);

                return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
            }
            catch (System.Exception ex)
            {
                _logger.LogError("GetRoleByUserID：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 根据角色ID获取应用信息
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    Role data = db.Role
                        .Include(t => t.RolePermissions).Include(t => t.UserManyToRole)
                        .Include(t => t.Organization).Include(t => t.Application)
                        .Include(t => t.ControlResourcesType)
                        .FirstOrDefault(p => p.RoleId.Equals(id));
                    if (data == null)
                    {
                        return NoContent();
                    }

                    return new ObjectResult(data);
                }
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError("Get：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);

                return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Get：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }


        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="model">角色实体</param>
        /// <returns>返回值</returns>
        [HttpPost]
        public IActionResult Add([FromBody]Role model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.Role.Add(model);
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
        /// 修改角色
        /// </summary>
        /// <param name="model">角色实体</param>
        /// <returns>返回值</returns>
        [HttpPut]
        public IActionResult Update([FromBody]Role model)
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
                        //转换一般数据
                        Role Role = db.Role
                                    .Include(t => t.RolePermissions)
                                    .Include(t => t.UserManyToRole)
                                    .Include(t => t.Organization)
                                    .Include(t => t.Application)
                                    .Include(t => t.ControlResourcesType)
                                    .FirstOrDefault(p => p.RoleId.Equals(model.RoleId));
                        if (Role == null)
                        {
                            return BadRequest();
                        }
                        //
                        Role.ApplicationId = model.ApplicationId;
                        Role.ControlResourcesTypeId = model.ControlResourcesTypeId;
                        Role.Description = model.Description;
                        Role.OrganizationId = model.OrganizationId;
                        Role.RoleName = model.RoleName;
                        //
                        RemoveManyToMany(db, Role);
                        //
                        Role.UserManyToRole = model.UserManyToRole;
                        Role.RolePermissions = model.RolePermissions;
                        db.Role.Update(Role);
                        db.SaveChanges();

                        transaction.Commit();

                        return new NoContentResult();
                    }
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

        private static void RemoveManyToMany(AllInOneContext.AllInOneContext db, Role Role)
        {
            //手动处理RolePermissions 多对多的关系，待找到合适的方法后再调整
            if (Role.RolePermissions != null)
            {
                List<RolePermission> delList = new List<RolePermission>();
                foreach (RolePermission rp in Role.RolePermissions)
                {
                    RolePermission del = db.RolePermission
                        .FirstOrDefault(p => p.PermissionId.Equals(rp.PermissionId) && p.RoleId.Equals(rp.RoleId));
                    if (del != null)
                    {
                        delList.Add(del);
                    }
                }
                db.RolePermission.RemoveRange(delList);
                db.SaveChanges();
            }
            //手动处理UserRole 多对多的关系，待找到合适的方法后再调整
            if (Role.UserManyToRole != null)
            {
                List<UserRole> delList = new List<UserRole>();
                foreach (UserRole rp in Role.UserManyToRole)
                {
                    UserRole del = db.UserRole
                        .FirstOrDefault(p => p.UserId.Equals(rp.UserId) && p.RoleId.Equals(rp.RoleId));
                    if (del != null)
                    {
                        delList.Add(del);
                    }
                }
                db.UserRole.RemoveRange(delList);
                db.SaveChanges();
            }
        }

        /// <summary>
        ///  根据角色ID删除角色
        /// </summary>
        /// <param name="id">角色ID</param>
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
                        Role Role = db.Role
                                    .Include(t => t.RolePermissions)
                                    .Include(t => t.UserManyToRole)
                                    .Include(t => t.Organization)
                                    .Include(t => t.Application)
                                    .Include(t => t.ControlResourcesType)
                                    .FirstOrDefault(p => p.RoleId == id);
                        if (Role == null)
                        {
                            return NoContent();
                        }
                        //
                        RemoveManyToMany(db, Role);
                        //
                        db.Role.Remove(Role);
                        db.SaveChanges();

                        transaction.Commit();
                        return new NoContentResult();
                    }
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
