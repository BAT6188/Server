/*************************
 * 2016-12-16 zhrx  修改预案，只移除预案动作，再更新（解决关联报警设置的外键关系，导致删除失败）
 */ 

using AlarmAndPlan.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlarmAndPlan.Services
{
    [Route("Plan/[controller]")]
    public class PlanController : Controller
    {
        private ILogger<PlanController> _logger;

        public PlanController(ILogger<PlanController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Add([FromBody]Plan plan)
        {
            if (plan == null)
            {
                return BadRequest();
            }
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    //计算预案的下一执行时间
                    db.Plan.Add(plan);
                    db.SaveChanges();
                    ////将预案发送到任务服务器
                    //string error = "";
                    //HttpClientHelper.Post<Plan>(plan, "http://localhost:5005/Task/PlanAction", ref error);
                    return CreatedAtAction("", plan);
                }
                catch (Exception ex)
                {
                    _logger.LogError("新增预案异常，Message:{0}\r\nStackTrace{1}", ex.Message, ex.StackTrace);
                    return BadRequest(ex);
                }
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody]Plan plan)
        {
            if (plan == null)
            {
                return BadRequest("Plan object is null!");
            }

            using (var db = new AllInOneContext.AllInOneContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //先移除上一配置的Action
                        var dbPlan = GetDbQuery(db).FirstOrDefault(t => t.PlanId.Equals(plan.PlanId));
                        dbPlan.Actions.ForEach(t => db.Set<PredefinedAction>().RemoveRange(t.PlanActions));
                        db.Set<PlanAction>().RemoveRange(dbPlan.Actions);
                        //db.Plan.Remove(dbPlan);
                        db.SaveChanges();

                        //重新赋值
                        dbPlan.Actions = plan.Actions;
                        dbPlan.PlanName = plan.PlanName;
                        dbPlan.PlanTypeId = plan.PlanTypeId;
                        dbPlan.TvVideoRoundSceneId = plan.TvVideoRoundSceneId;
                        db.Plan.Update(dbPlan);
                        db.SaveChanges();
                        transaction.Commit();
                        return NoContent();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError("更新预案异常，Message:{0}\r\nStackTrace{1}", ex.Message, ex.StackTrace);
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
                        Plan delObj = GetDbQuery(db).FirstOrDefault(t => t.PlanId.Equals(id));
                        if (delObj == null || !delObj.PlanId.Equals(id))
                        {
                            return NotFound();
                        }
                        delObj.Actions.ForEach(t => db.Set<PredefinedAction>().RemoveRange(t.PlanActions));
                        db.Set<PlanAction>().RemoveRange(delObj.Actions);
                        db.Plan.Remove(delObj);
                        db.SaveChanges();
                        transaction.Commit();

                        ////移除任务服务器的预案
                        //HttpClientHelper.Delete("http://localhost:5005/Task/PlanAction/" + id.ToString());
                        return NoContent();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError("删除预案异常，Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                        return BadRequest(ex);
                    }
                }
            }
        }

        [HttpGet]
        [Route("~/Plan/Find/planTypeId={planTypeId}")]
        public IEnumerable<Plan> Find(Guid planTypeId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return GetDbQuery(db).
                    Where(t => t.PlanTypeId.Equals(planTypeId)).
                    ToList();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    Plan plan = GetDbQuery(db).FirstOrDefault(t => t.PlanId.Equals(id));
                    if (plan == null || plan.PlanId.Equals(Guid.Empty))
                        return NotFound();
                    return new OkObjectResult(plan);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("获取预案{0}异常，Mesage:{1}\r\nStackTrace:{2}", id, ex.Message, ex.StackTrace);
                return BadRequest(ex);
            }
        }

        private IQueryable<Plan> GetDbQuery(AllInOneContext.AllInOneContext dbContext)
        {
            return dbContext.Plan.Include(t => t.Actions).ThenInclude(t => t.PlanActions).ThenInclude(t => t.Action).
                        Include(t => t.Actions).ThenInclude(t => t.PlanDevice).ThenInclude(t => t.DeviceType).
                        Include(t => t.Actions).ThenInclude(t => t.PlanDevice).ThenInclude(t => t.Organization).
                        Include(t => t.TvVideoRoundScene).ThenInclude(t => t.VideoRoundSections).ThenInclude(t => t.RoundMonitorySiteSettings).ThenInclude(t => t.MonitorySite);
                        //Include(t => t.Schedule).ThenInclude(t => t.ScheduleCycle).ThenInclude(t => t.TimePeriods).
                        //Include(t => t.Schedule).ThenInclude(t => t.ScheduleCycle).ThenInclude(t => t.CycleType);
        }
        

        [HttpGet]
        public IEnumerable<Plan> GeAll()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return GetDbQuery(db).Where(t => t.PlanTypeId != null).ToList();
            }
        }

    }
}
