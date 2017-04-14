using AlarmAndPlan.Model;
using HttpClientEx;
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
    /// <summary>
    /// 定时任务接口api
    /// </summary>
    [Route("Plan/[controller]")]
    public class TimerTaskController : Controller
    {
        private ILogger<TimerTaskController> _logger;

        public TimerTaskController(ILogger<TimerTaskController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Add([FromBody]TimerTask task)
        {
            if (task == null)
            {
                return BadRequest();
            }
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    //计算预案的下一执行时间
                    db.TimerTask.Add(task);
                    db.SaveChanges();
                    //将预案发送到任务服务器
                    string url = string.Format("{0}/Task/PlanAction", GlobalSetting.TaskServerBaseUrl);
                    HttpClientHelper.Post<TimerTask>(task, url);
                    return CreatedAtAction("", task);
                }
                catch (Exception ex)
                {
                    _logger.LogError("新增定时任务异常，Message:{0}\r\nStackTrace{1}", ex.Message, ex.StackTrace);
                    return BadRequest(ex);
                }
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody]TimerTask task)
        {
            if (task == null)
            {
                return BadRequest("TimerTask object is null!");
            }

            using (var db = new AllInOneContext.AllInOneContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.TimerTask.Update(task);
                        transaction.Commit();
                        return NoContent();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError("更新定时任务异常，Message:{0}\r\nStackTrace{1}", ex.Message, ex.StackTrace);
                        return BadRequest(ex);
                    }
                }
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        TimerTask delObj = db.TimerTask.FirstOrDefault(t => t.TimerTaskId.Equals(id));
                        if (delObj == null || !delObj.PlanId.Equals(id))
                        {
                            return NotFound();
                        }
                        db.TimerTask.Remove(delObj);
                        db.SaveChanges();
                        transaction.Commit();

                        //移除任务服务器的预案
                        string url = string.Format("{0}/Task/PlanAction/{1}", GlobalSetting.TaskServerBaseUrl, id);
                        string responseContent = "";
                        HttpClientHelper.Delete(url, ref responseContent);
                        return NoContent();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError("删除定时任务异常，Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                        return BadRequest(ex);
                    }
                }
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    TimerTask task = GetDbQuery(db).FirstOrDefault(t => t.TimerTaskId.Equals(id));
                    if (task == null || task.TimerTaskId.Equals(Guid.Empty))
                        return NotFound();
                    return new OkObjectResult(task);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("获取定时任务{0}异常，Mesage:{1}\r\nStackTrace:{2}", id, ex.Message, ex.StackTrace);
                return BadRequest(ex);
            }
        }

        private IQueryable<TimerTask> GetDbQuery(AllInOneContext.AllInOneContext dbContext)
        {
            return dbContext.TimerTask.Include(t => t.Plan).ThenInclude(t=>t.Actions).ThenInclude(t => t.PlanActions).ThenInclude(t => t.Action).
                        Include(t => t.Plan).ThenInclude(t => t.Actions).ThenInclude(t => t.PlanDevice).
                        Include(t => t.TaskSchedule).ThenInclude(t => t.ScheduleCycle).ThenInclude(t => t.DayPeriods).
                        Include(t => t.TaskSchedule).ThenInclude(t => t.ScheduleCycle).ThenInclude(t => t.CycleType);
        }


        [HttpGet]
        public IEnumerable<TimerTask> GeAll()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return GetDbQuery(db).ToList();
            }
        }
    }
}
