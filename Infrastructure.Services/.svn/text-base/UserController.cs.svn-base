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
    /// 用户控制类
    /// </summary>
    public class UserController : Controller
    {

        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var data = db.User
                    .Include(t => t.ControlResources)
                    .Include(t=>t.UserManyToRole)
                    .Include(t=>t.Application)
                    .Include(t => t.UserSettings)
                    .Include(t=>t.Organization)
                    ;
                return data.ToList();
            }
        }

        /// <summary>
        /// 根据组织机构ID获取所属用户
        /// </summary>
        /// <param name="organizationId">组织机构ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Infrastructure/User/organizationId={organizationId}")]
        public IActionResult GetUserByOrganizationId(Guid organizationId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var data = db.User
                        .Include(t => t.ControlResources)
                        .Include(t => t.UserManyToRole)
                        .Include(t => t.Application)
                        .Include(t => t.UserSettings)
                        .Include(t => t.Organization)
                        .Where(p => p.Organization.OrganizationId.Equals(organizationId));
                    if (data == null)
                    {
                        return NoContent();
                    }

                    return new ObjectResult(data.ToList());
                }
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError("GetUserByOrganizationId：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);

                return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
            }
            catch (System.Exception ex)
            {
                _logger.LogError("GetUserByOrganizationId：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 根据应用ID获取所属用户
        /// </summary>
        /// <param name="applicationId">应用ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Infrastructure/User/applicationId={applicationId}")]
        public IActionResult GetUserByApplicationId(Guid applicationId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var data = db.User
                        .Include(t => t.ControlResources)
                        .Include(t => t.UserManyToRole)
                        .Include(t => t.Application)
                        .Include(t => t.UserSettings)
                        .Include(t => t.Organization)
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
                _logger.LogError("GetUserByApplicationId：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);

                return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
            }
            catch (System.Exception ex)
            {
                _logger.LogError("GetUserByApplicationId：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 根据用户ID获取所属用户
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    User data = db.User
                        .Include(t => t.ControlResources)
                        .Include(t => t.UserManyToRole)
                        .Include(t => t.Application)
                        .Include(t => t.UserSettings)
                        .Include(t => t.Organization)
                        .FirstOrDefault(p => p.UserId.Equals(id));
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
        /// 新增用户
        /// </summary>
        /// <param name="model">用户实体</param>
        /// <returns>返回值</returns>
        [HttpPost]
        public IActionResult Add([FromBody]User model)
        {
            try
            {
                ///待考虑是否进行密码格式验证
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.User.Add(model);
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
        /// 修改用户
        /// </summary>
        /// <param name="model">用户实体</param>
        /// <returns>返回值</returns>
        [HttpPut]
        public IActionResult Update([FromBody]User model)
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
                        User User = db.User
                                    .Include(t => t.ControlResources)
                                    .Include(t => t.UserManyToRole)
                                    .Include(t => t.Application)
                                    .Include(t => t.UserSettings)
                                    .Include(t => t.Organization)
                                    .FirstOrDefault(p => p.UserId.Equals(model.UserId));
                        if (User == null)
                        {
                            return BadRequest();
                        }
                        //
                        User.ApplicationId = model.ApplicationId;
                        User.OrganizationId = model.OrganizationId;
                        User.PasswordHash = model.PasswordHash;
                        User.UserName = model.UserName;
                        User.Enable = model.Enable; //zhrx
                        User.Description = model.Description; //liupei
                        //
                        RmoveManyToMany(db, User);
                        //
                        User.ControlResources = model.ControlResources;
                        User.UserManyToRole = model.UserManyToRole;
                        User.UserSettings = model.UserSettings;

                        db.User.Update(User);
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

        private static void RmoveManyToMany(AllInOneContext.AllInOneContext db, User User)
        {
            //手动处理UserRole 多对多的关系，待找到合适的方法后再调整
            if (User.UserManyToRole != null)
            {
                List<UserRole> delList = new List<UserRole>();
                foreach (UserRole rp in User.UserManyToRole)
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
            //手动处理ControlResources 多对多的关系，待找到合适的方法后再调整
            if (User.ControlResources != null)
            {
                List<ControlResources> delList = new List<ControlResources>();
                foreach (ControlResources rp in User.ControlResources)
                {
                    ControlResources del = db.ControlResources
                        .FirstOrDefault(p => p.ControlResourcesId.Equals(rp.ControlResourcesId));
                    if (del != null)
                    {
                        delList.Add(del);
                    }
                }
                db.ControlResources.RemoveRange(delList);
                db.SaveChanges();
            }
            //手动处理UserSettingMapping 一对多的关系，待找到合适的方法后再调整
            if (User.UserSettings != null)
            {
                List<UserSettingMapping> delList = new List<UserSettingMapping>();
                foreach (UserSettingMapping rp in User.UserSettings)
                {
                    UserSettingMapping del = db.Set<UserSettingMapping>()
                        .FirstOrDefault(p => p.UserSettingId.Equals(rp.UserSettingId));
                    if (del != null)
                    {
                        delList.Add(del);
                    }
                }
                db.Set<UserSettingMapping>().RemoveRange(delList);
                db.SaveChanges();
            }
        }

        /// <summary>
        ///  根据用户ID删除用户
        /// </summary>
        /// <param name="id">用户ID</param>
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
                        User User = db.User
                                    .Include(t => t.ControlResources)
                                    .Include(t => t.UserManyToRole)
                                    .Include(t => t.Application)
                                    .Include(t => t.UserSettings)
                                    .Include(t => t.Organization)
                                    .FirstOrDefault(p => p.UserId == id);
                        if (User == null)
                        {
                            return NoContent();
                        }
                        //
                        RmoveManyToMany(db, User);
                        //
                        db.User.Remove(User);
                        db.SaveChanges();

                        transaction.Commit();
                        return new NoContentResult();
                    }
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 根据用户名模糊搜索用户
        /// </summary>
        /// <param name="userName">应用ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Infrastructure/User/userName={userName}")]
        public IActionResult FuzzySearchByUserName(string userName)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var data = db.User
                        .Include(t => t.ControlResources)
                        .Include(t => t.UserManyToRole)
                        .Include(t => t.Application)
                        .Include(t => t.UserSettings)
                        .Include(t => t.Organization)
                        .Where(p => p.UserName.Contains(userName));
                    if (data == null || data.Count() == 0)
                    {
                        return NoContent();
                    }

                    return new ObjectResult(data.ToList());
                }
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError("FuzzySearchByUserName：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);

                return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
            }
            catch (System.Exception ex)
            {
                _logger.LogError("FuzzySearchByUserName：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }
    }
}
