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
    /// 系统资源控制类
    /// </summary>
    public class ApplicationResourceController : Controller
    {
        private readonly ILogger<ApplicationResourceController> _logger;
        public ApplicationResourceController(ILogger<ApplicationResourceController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<ApplicationResource> GetAll()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var data = db.ApplicationResource
                    .Include(t => t.Actions)
                    .Include(t => t.Application)
                    .Include(t => t.ParentResource);
                return data.ToList();
            }
        }

        /// <summary>
        /// 根据应用ID获取所有系统资源
        /// </summary>
        /// <param name="applicationId">应用ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Infrastructure/ApplicationResource/applicationId={applicationId}")]
        public IActionResult GetApplicationResourceByApplicationID(Guid applicationId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var data = db.ApplicationResource
                                .Include(t => t.Actions)
                                .Include(t => t.Application)
                                .Include(t => t.ParentResource)
                        .Where(p => p.ApplicationId.Equals(applicationId));

                    if (data.Count() == 0)
                    {
                        return NoContent();
                    }

                    return new ObjectResult(data.ToList());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("GetApplicationResourceByApplicationID：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }


        /// <summary>
        /// 根据父节点ID获取所有系统资源
        /// </summary>
        /// <param name="organizationId">父节点ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Infrastructure/ApplicationResource/parentResourceId={parentResourceId}")]
        public IActionResult GetApplicationResourceByApplicationResourceID(Guid parentResourceId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var data = db.ApplicationResource
                                .Include(t => t.Actions)
                                .Include(t => t.Application)
                                .Include(t => t.ParentResource)
                        .Where(p => p.ParentResource != null && p.ParentResourceId.Equals(parentResourceId));

                    if (data.Count() == 0)
                    {
                        return NoContent();
                    }

                    return new ObjectResult(data.ToList());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("GetApplicationResourceByApplicationResourceID：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }


        /// <summary>
        /// 根据系统资源ID获取信息
        /// </summary>
        /// <param name="id">系统资源ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    ApplicationResource data = db.ApplicationResource
                                .Include(t => t.Actions)
                                .Include(t => t.Application)
                                .Include(t => t.ParentResource)
                        .FirstOrDefault(p => p.ApplicationResourceId.Equals(id));
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
    }
}
