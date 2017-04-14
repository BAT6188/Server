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
    /// 系统日志控制类
    /// </summary>
    public class EventLogController : Controller
    {
        private readonly ILogger<EventLogController> _logger;
        public EventLogController(ILogger<EventLogController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 根据系统日志ID获取应用信息
        /// </summary>
        /// <param name="id">系统日志ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    EventLog data = db.EventLog
                        .Include(t=>t.Application).Include(t=>t.EventLevel)
                        .Include(t=>t.EventLogType).Include(t=>t.EventSource)
                        .Include(t=>t.Organization)
                        .FirstOrDefault(p => p.EventLogId.Equals(id));
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
        /// 新增系统日志
        /// </summary>
        /// <param name="model">系统日志实体</param>
        /// <returns>返回值</returns>
        [HttpPost]
        public IActionResult Add([FromBody]EventLog model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.EventLog.Add(model);
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

        ///// <summary>
        ///// 修改系统日志
        ///// </summary>
        ///// <param name="model">系统日志实体</param>
        ///// <returns>返回值</returns>
        //[HttpPut]
        //public IActionResult UpdateEventLog([FromBody]EventLog model)
        //{

        //    return Ok(1);
        //}

        /// <summary>
        ///  根据系统日志ID删除系统日志
        /// </summary>
        /// <param name="id">系统日志ID</param>
        /// <returns>返回值</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    EventLog data = db.EventLog.FirstOrDefault(p => p.EventLogId == id);
                    if (data == null)
                    {
                        return NoContent();
                    }
                    db.EventLog.Remove(data);
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


        /// <summary>
        /// 根据参数获取日志集合
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="currentPage">当前页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="eventLevelId">事件级别id</param>
        /// <param name="eventSourceId">事件来源id</param>
        /// <param name="eventLogTypeId">日志类型id</param>
        /// <param name="organizationId">组织机构id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Infrastructure/EventLog")]
        public IActionResult GetEventLogByParameter(DateTime startTime, DateTime endTime,int currentPage,
            int pageSize, Guid?[] eventLevelId, Guid?[] eventSourceId, Guid?[] eventLogTypeId, Guid organizationId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var query = from p in db.EventLog
                                .Include(t => t.Application).Include(t => t.EventLevel)
                                .Include(t => t.EventLogType).Include(t => t.EventSource)
                                .Include(t => t.Organization)
                                orderby p.TimeCreated descending
                                where p.TimeCreated >= startTime && p.TimeCreated <= endTime
                                && ((eventLevelId == null || eventLevelId.Length == 0) || eventLevelId.Contains(p.EventLevel.SystemOptionId))
                                && ((eventSourceId == null || eventSourceId.Length == 0) || eventSourceId.Contains(p.EventSource.SystemOptionId))
                                && ((eventLogTypeId == null || eventLogTypeId.Length == 0) || eventLogTypeId.Contains(p.EventLogType.SystemOptionId))
                                && p.Organization.OrganizationId == organizationId
                                select p;
                    if (currentPage == 0)
                        currentPage = 1;
                    if (pageSize <= 0)
                        pageSize = 10;

                    var data = query.Skip(pageSize * (currentPage - 1)).Take(pageSize * currentPage).ToList();

                    if (data.Count() == 0)
                    {
                        return NoContent();
                    }

                    QueryPagingRecord queryPagingRecord = new QueryPagingRecord
                    {
                        SumRecordCount=query.Count(),
                        Record= data.ToList()
                    };


                    return new ObjectResult(queryPagingRecord);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("GetEventLogByParameter：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }
    }
}
