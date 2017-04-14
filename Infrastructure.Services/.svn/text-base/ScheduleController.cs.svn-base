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
    /// 任务排程程序控制类
    /// </summary>
    public class ScheduleController : Controller
    {
        private readonly ILogger<ScheduleController> _logger;
        public ScheduleController(ILogger<ScheduleController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 获取所有任务排程
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Schedule> GetAll()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var data = db.Schedule
                             .Include(t => t.ScheduleType)
                             .Include(t => t.ScheduleCycle).ThenInclude(t => t.DayPeriods).ThenInclude(t => t.TimePeriods)
                             .Include(t => t.ScheduleCycle).ThenInclude(t => t.CycleType);
                return data.ToList();
            }
        }

        /// <summary>
        /// 根据任务排程ID获取任务排程
        /// </summary>
        /// <param name="id">任务排程ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                Schedule data = db.Schedule
                             .Include(t => t.ScheduleType)
                             .Include(t => t.ScheduleCycle).ThenInclude(t => t.DayPeriods).ThenInclude(t => t.TimePeriods)
                             .Include(t => t.ScheduleCycle).ThenInclude(t => t.CycleType)
                            .FirstOrDefault(p => p.ScheduleId.Equals(id));
                if (data == null)
                {
                    return NoContent();
                }

                return new ObjectResult(data);
            }
        }


        /// <summary>
        /// 新增任务排程
        /// </summary>
        /// <param name="model">任务排程实体</param>
        /// <returns>返回值</returns>
        [HttpPost]
        public IActionResult Add([FromBody]Schedule model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    if (model.ScheduleCycle != null)
                    {
                        //补全数据
                        SystemOption cycletype = db.Set<SystemOption>()
                                                      .FirstOrDefault(p => p.SystemOptionId.Equals(model.ScheduleCycle.CycleTypeId));
                        Schedule Schedule = model;
                        if (cycletype != null)
                        {
                            Schedule.ScheduleCycle.CycleType = cycletype;
                        }
                        DateTime? executeTime = ScheduleUtility.GetExecuteTime(Schedule, DateTime.Now);
                        model.ScheduleCycle.NextExecute = executeTime;
                    }
                    db.Schedule.Add(model);
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
        /// 修改任务排程
        /// </summary>
        /// <param name="model">任务排程实体</param>
        /// <returns>返回值</returns>
        [HttpPut]
        public IActionResult Update([FromBody]Schedule model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        if (model.ScheduleCycle != null)
                        { 
                           //补全数据
                            SystemOption cycletype = db.Set<SystemOption>()
                                                          .FirstOrDefault(p => p.SystemOptionId.Equals(model.ScheduleCycle.CycleTypeId));
                            Schedule Schedule = model;
                            if (cycletype != null)
                            {
                                Schedule.ScheduleCycle.CycleType = cycletype;
                            }
                            DateTime? executeTime = ScheduleUtility.GetExecuteTime(Schedule, DateTime.Now);
                            model.ScheduleCycle.NextExecute = executeTime;
                        }
                        //转换一般数据
                        Schedule shedule = db.Schedule
                                        .Include(t => t.ScheduleType)
                                        .Include(t => t.ScheduleCycle).ThenInclude(t => t.DayPeriods)
                                        .Include(t => t.ScheduleCycle).ThenInclude(t => t.CycleType)
                                        .FirstOrDefault(p => p.ScheduleId.Equals(model.ScheduleId));

                        if (shedule == null)
                        {
                            return BadRequest();
                        }

                        shedule.EffectiveTime = model.EffectiveTime;
                        shedule.ExpirationTime = model.ExpirationTime;
                        shedule.ScheduleTypeId = model.ScheduleTypeId;
                        shedule.ScheduleName = model.ScheduleName;
                        //
                        ScheduleCycle scheduleCycle = db.Set<ScheduleCycle>()
                                                    .Include(t => t.DayPeriods)
                                                    .FirstOrDefault(p => p.ScheduleCycleId.Equals(shedule.ScheduleCycle.ScheduleCycleId));
                        //转换ScheduleCycle
                        scheduleCycle.CycleTypeId = model.ScheduleCycle.CycleTypeId;
                        scheduleCycle.Days = model.ScheduleCycle.Days;
                        scheduleCycle.DaysJson = model.ScheduleCycle.DaysJson;
                        scheduleCycle.LastExecute = model.ScheduleCycle.LastExecute;
                        scheduleCycle.Months = model.ScheduleCycle.Months;
                        scheduleCycle.MonthsJson = model.ScheduleCycle.MonthsJson;
                        scheduleCycle.NextExecute = model.ScheduleCycle.NextExecute;
                        scheduleCycle.WeekDayJson = model.ScheduleCycle.WeekDayJson;
                        scheduleCycle.WeekDays = model.ScheduleCycle.WeekDays;
                        //
                        RemoveList(db, shedule);
                        //
                        scheduleCycle.DayPeriods = model.ScheduleCycle.DayPeriods;
                        shedule.ScheduleCycle = scheduleCycle;
                        db.Schedule.Update(shedule);
                        db.SaveChanges();
                        //
                        tran.Commit();
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

        /// <summary>
        /// 手动移除实体中的List属性
        /// </summary>
        /// <param name="db"></param>
        /// <param name="shedule"></param>
        private static void RemoveList(AllInOneContext.AllInOneContext db, Schedule shedule)
        {
            List<DayPeriod> removeDay = new List<DayPeriod>();
            List<TimePeriod> removeTime = new List<TimePeriod>();
            foreach (DayPeriod tp in shedule.ScheduleCycle.DayPeriods)
            {
                DayPeriod dayPeriod = db.Set<DayPeriod>().Include(t => t.TimePeriods).FirstOrDefault(p => p.DayPeriodId.Equals(tp.DayPeriodId));
                if (dayPeriod != null)
                {
                    removeDay.Add(dayPeriod);
                    removeTime.AddRange(tp.TimePeriods);
                }
            }
            if (removeTime.Count > 0)
            {
                db.Set<TimePeriod>().RemoveRange(removeTime);
                db.Set<DayPeriod>().RemoveRange(removeDay);
                db.SaveChanges();
            }
        }


        /// <summary>
        ///  根据任务排程ID删除任务排程
        /// </summary>
        /// <param name="id">任务排程ID</param>
        /// <returns>返回值</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        Schedule data = db.Schedule
                                        .Include(t => t.ScheduleType)
                                        .Include(t => t.ScheduleCycle).ThenInclude(t => t.DayPeriods)
                                        .Include(t => t.ScheduleCycle).ThenInclude(t => t.CycleType)
                                        .FirstOrDefault(p => p.ScheduleId.Equals(id));

                        if (data == null)
                        {
                            return NoContent();
                        }
                        //移除LIST
                        RemoveList(db, data);
                        //移除ScheduleCycle
                        ScheduleCycle scheduleCycle = db.Set<ScheduleCycle>()
                                                    .FirstOrDefault(p => p.ScheduleCycleId.Equals(data.ScheduleCycle.ScheduleCycleId));
                        if(scheduleCycle!=null)
                        db.Set<ScheduleCycle>().Remove(scheduleCycle);
                        db.Schedule.Remove(data);
                        db.SaveChanges();
                        tran.Commit();
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


        /// <summary>
        /// 检查人员是否正常换岗
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Infrastructure/Schedule/typeCode={typeCode}")]
        public IActionResult GetScheduleByType(string typeCode)
        {
            if (typeCode == "")
                return NoContent();
            using (var db = new AllInOneContext.AllInOneContext())
            {

                var data = db.Schedule
                         .Include(t => t.ScheduleType)
                         .Include(t => t.ScheduleCycle).ThenInclude(t => t.DayPeriods).ThenInclude(t => t.TimePeriods)
                         .Include(t => t.ScheduleCycle).ThenInclude(t => t.CycleType)
                         .Where(p => p.ScheduleType.SystemOptionCode == typeCode);
                if (data.Count() == 0)
                    return NoContent();

                return new ObjectResult(data.ToList());


            }
        }
    }
}
