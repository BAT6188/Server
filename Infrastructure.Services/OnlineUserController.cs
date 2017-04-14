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
    /// 在线用户控制类
    /// </summary>
    public class OnlineUserController : Controller
    {

        private readonly ILogger<OnlineUserController> _logger;
        public OnlineUserController(ILogger<OnlineUserController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 根获取所有在线用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<OnlineUser> GetAll()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var data = db.OnlineUser
                    .Include(t=>t.User).Include(t=>t.LoginTerminal);
                return data.ToList();
            }
        }

        /// <summary>
        /// 根据在线用户ID获取应用信息
        /// </summary>
        /// <param name="id">在线用户ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                OnlineUser data = db.OnlineUser
                    .Include(t => t.User).Include(t => t.LoginTerminal)
                    .FirstOrDefault(p => p.OnLineUserId.Equals(id));
                if (data == null)
                {
                    return NoContent();
                }
                return new ObjectResult(data);
            }
        }


        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model">在线用户实体</param>
        /// <returns>返回值</returns>
        [HttpPost]
        public IActionResult Add([FromBody]OnlineUser model)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    //密码需要加密传输，待确定加密方式
                    User data = db.User.Include(t=>t.Organization)
                        .FirstOrDefault(p => p.UserName.Equals(model.User.UserName) && p.PasswordHash.Equals(model.User.PasswordHash));
                    if (data == null)
                    {
                        return NoContent();
                    }
                    OnlineUser User = new OnlineUser
                    {
                        User = data,
                        KeepAlived = DateTime.Now,
                        LoginTerminal = model.LoginTerminal,
                        LoginTime = DateTime.Now,
                        OnLineUserId = Guid.NewGuid(),
                    };

                    db.OnlineUser.Add(User);
                    db.SaveChanges();
                    return new ObjectResult(User);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Add：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 心跳
        /// </summary>
        /// <param name="model">在线用户实体</param>
        /// <returns>返回值</returns>
        [HttpPut]
        public IActionResult Update([FromBody]OnlineUser model)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    OnlineUser data = db.OnlineUser
                        .FirstOrDefault(p => p.User.UserId.Equals(model.OnLineUserId));
                    if (data == null)
                    {
                        return NoContent();
                    }
                    OnlineUser User = new OnlineUser
                    {
                        User = model.User,
                        KeepAlived = DateTime.Now,
                        LoginTerminal = model.LoginTerminal,
                        LoginTime = data.LoginTime,
                        OnLineUserId = model.OnLineUserId
                    };

                    db.OnlineUser.Update(User);
                    db.SaveChanges();

                    return new ObjectResult(User);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Update：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        ///  根据在线用户ID删除在线用户
        /// </summary>
        /// <param name="id">在线用户ID</param>
        /// <returns>返回值</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    OnlineUser data = db.OnlineUser
                        .FirstOrDefault(p => p.OnLineUserId == id);
                    if (data == null)
                    {
                        return NoContent();
                    }
                    db.OnlineUser.Remove(data);
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
