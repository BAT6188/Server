using AlarmAndPlan.Model;
using Infrastructure.Model;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlarmAndPlan.Services
{
    [Route("Event/[Controller]")]
    public class ServiceEventLogController : Controller
    {
        private ILogger<ServiceEventLogController> _logger;

        public ServiceEventLogController(ILogger<ServiceEventLogController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Add([FromBody]ServiceEventLog eventlog)
        {
            if (eventlog == null)
            {
                return BadRequest();
            }
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    db.ServiceEventLog.Add(eventlog);
                    db.SaveChanges();
                    //广播消息
                    publish(eventlog);
                    return CreatedAtAction("", eventlog);
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError("添加服务事件异常，Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException() { ErrorCode = "DbUpdate", ErrorMessage = ex.Message });
                }
                catch (Exception ex)
                {
                    _logger.LogError("添加服务事件异常，Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException() { ErrorCode = "Unknow", ErrorMessage = ex.Message });
                }
            }
        }

    
        [HttpGet]
        public IEnumerable<ServiceEventLog> GetAll()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return db.ServiceEventLog.ToList();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    ServiceEventLog serviceEventLog = db.ServiceEventLog.FirstOrDefault(t => t.ServiceEventLogId.Equals(id));
                    if (serviceEventLog == null || !serviceEventLog.ServiceEventLogId.Equals(id))
                    {
                        return NotFound();
                    }
                    return new OkObjectResult(serviceEventLog);
                }
                catch (Exception ex)
                {
                    _logger.LogError("获取服务事件记录{0}异常，Message:{1}\r\n,StackTrace:{2}.", id, ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException() { ErrorCode = "Unknow", ErrorMessage = ex.Message });
                }
            }
        }

        /// <summary>
        /// 根据查询条件获取服务事件信息
        /// </summary>
        /// <param name="startTime">查询开始时间</param>
        /// <param name="endTime">查询结束时间</param>
        /// <param name="eventTypeIds">服务事件类型</param>
        /// <param name="eventSourceIds">服务器id</param>
        /// <param name="pageNo">页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Event/ServiceEvent")]
        public IEnumerable<ServiceEventLog> SearchAlarmLog(DateTime startTime, DateTime endTime, Guid[] eventTypeIds, Guid[] eventSourceIds, int pageNo = 1, int pageSize = 10)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                if (pageNo <= 0)
                    pageNo = 1;
                if (pageSize <= 10)
                    pageSize = 10;
                var res = db.ServiceEventLog.Include(t=>t.EventType).Include(t=>t.EventSource).Where(t => ((eventTypeIds == null || eventTypeIds.Length == 0) || eventTypeIds.Contains(t.EventTypeId)) &&
                   ((eventSourceIds == null || eventSourceIds.Length == 0) || eventSourceIds.Contains(t.EventSourceId)) &&
                    (t.TimeCreated >= startTime && t.TimeCreated <= endTime))
                    .Skip((pageNo - 1) * pageSize).Take(pageSize);

                return res.ToList();
            }
        }

        public IActionResult publish([FromBody]ServiceEventLog eventLog)
        {
            MQPulish.PublishMessage("ServiceEventLog", eventLog);
            return Ok();
        }
    }
}
