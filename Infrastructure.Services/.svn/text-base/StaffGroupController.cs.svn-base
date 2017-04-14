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
    /// 人员分组控制类
    /// </summary>
    public class StaffGroupController : Controller
    {

        private readonly ILogger<StaffGroupController> _logger;
        public StaffGroupController(ILogger<StaffGroupController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<StaffGroup> GetAll()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var data = db.StaffGroup
                    .Include(t => t.Staffs).Include(t=>t.Application)
                    .Include(t=>t.Organization);
                return data.ToList();
            }
        }

        /// <summary>
        /// 根据组织机构ID获取所有人员分组
        /// </summary>
        /// <param name="organizationId">组织机构ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Infrastructure/StaffGroup/organizationId={organizationId}")]
        public IActionResult GetStaffGroupByOrganizationID(Guid organizationId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var data = db.StaffGroup
                        .Include(t => t.Staffs).Include(t => t.Application)
                        .Include(t => t.Organization)
                        .Where(p => p.Organization.OrganizationId.Equals(organizationId));
                    if (data.Count() == 0)
                    {
                        return NoContent();
                    }

                    return new ObjectResult(data.ToList());
                }
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError("GetStaffGroupByOrganizationID：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);

                return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
            }
            catch (System.Exception ex)
            {
                _logger.LogError("GetStaffGroupByOrganizationID：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 根据应用ID获取所有人员分组
        /// </summary>
        /// <param name="id">应用ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Infrastructure/StaffGroup/applicationId={applicationId}")]
        public IActionResult GetStaffGroupByApplicationID(Guid applicationId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var data = db.StaffGroup
                        .Include(t => t.Staffs).Include(t => t.Application)
                        .Include(t => t.Organization)
                        .Where(p => p.Application.ApplicationId.Equals(applicationId));
                    if (data.Count() == 0)
                    {
                        return NoContent();
                    }

                    return new ObjectResult(data.ToList());
                }
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError("GetStaffGroupByApplicationID：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);

                return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
            }
            catch (System.Exception ex)
            {
                _logger.LogError("GetStaffGroupByApplicationID：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 根据人员分组ID获取信息
        /// </summary>
        /// <param name="id">人员分组ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    StaffGroup data = db.StaffGroup
                        .Include(t => t.Staffs).Include(t => t.Application)
                        .Include(t => t.Organization)
                        .FirstOrDefault(p => p.StaffGroupId.Equals(id));
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
        /// 新增人员分组
        /// </summary>
        /// <param name="model">人员分组实体</param>
        /// <returns>返回值</returns>
        [HttpPost]
        public IActionResult Add([FromBody]StaffGroup model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.StaffGroup.Add(model);
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
        /// 修改人员分组
        /// </summary>
        /// <param name="model">人员分组实体</param>
        /// <returns>返回值</returns>
        [HttpPut]
        public IActionResult Update([FromBody]StaffGroup model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.StaffGroup.Update(model);
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
        ///  根据人员分组ID删除人员分组
        /// </summary>
        /// <param name="id">人员分组ID</param>
        /// <returns>返回值</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    StaffGroup data = db.StaffGroup.FirstOrDefault(p => p.StaffGroupId == id);
                    if (data == null)
                    {
                        return NoContent();
                    }
                    db.StaffGroup.Remove(data);
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
