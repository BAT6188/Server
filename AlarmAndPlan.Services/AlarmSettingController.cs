using AlarmAndPlan.Model;
using Infrastructure.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlarmAndPlan.Services
{
    [Route("Alarm/[controller]")]
    public class AlarmSettingController : Controller
    {
        private ILogger<AlarmSettingController> _logger;
        public AlarmSettingController(ILogger<AlarmSettingController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Add([FromBody]AlarmSetting alarmSetting)
        {
            if (alarmSetting == null)
            {
                return BadRequest();
            }
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    _logger.LogInformation("开始添加报警设置....");
                    //if (db.AlarmSetting.Where(s => s.AlarmSettingId.Equals(alarmSetting.AlarmSettingId)).Count() > 0)
                    //    return BadRequest("alarmSetting id is been used!");
                    db.AlarmSetting.Add(alarmSetting);
                    db.SaveChanges();
                    _logger.LogInformation("完成添加报警设置......");
                }
                catch (Exception ex)
                {
                    _logger.LogError("添加报警设置异常，Message:{0}\r\n StackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(ex.Message);
                }
            }
            return CreatedAtAction("", alarmSetting);
        }

        [HttpPut]
        public IActionResult Update([FromBody]AlarmSetting alarmSetting)
        {
            if (alarmSetting == null)
            {
                return BadRequest();
            }

            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    _logger.LogInformation("开始更新报警设置....");
                    db.AlarmSetting.Update(alarmSetting);
                    db.SaveChanges();
                    _logger.LogInformation("完成更新报警设置....");
                }
                catch (Exception ex)
                {
                    _logger.LogError("更新报警设置异常，Message:{0}\r\n{StackTrace{1}}", ex.Message, ex.StackTrace);
                    return BadRequest(ex.Message);
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                _logger.LogInformation("开始删除报警设置{0}", id);
                try
                {
                    AlarmSetting delObj = db.AlarmSetting.FirstOrDefault(s => s.AlarmSettingId.Equals(id));
                    if (delObj == null || delObj.AlarmSettingId.Equals(Guid.Empty))
                    {
                        return NotFound();
                    }
                    List<Guid> removePlanIds = new List<Guid>();
                    List<Guid> removeScheduleIds = new List<Guid>();
                    if (delObj.BeforePlanId != null)
                        removePlanIds.Add(delObj.BeforePlanId.Value);
                    if (delObj.EmergencyPlanId != null)
                        removePlanIds.Add(delObj.EmergencyPlanId.Value);
                    removeScheduleIds.Add(delObj.ScheduleId);
                    db.AlarmSetting.Remove(delObj);
                    RemoveAlarmSettingPlan(db, removePlanIds);
                    RemoveAlarmSettingSchedule(db, removeScheduleIds);

                    db.SaveChanges();
                    _logger.LogInformation("完成删除报警设置{0}", id);
                    return NoContent();
                }
                catch (Exception ex)
                {
                    _logger.LogError("删除报警设置异常，Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(ex);
                }
            }
        }

        /// <summary>
        /// 删除设备的报警设置
        /// </summary>
        /// <param name="alarmSourceId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("~/Alarm/AlarmSetting/alarmSourceId={alarmSourceId}")]
        public IActionResult RemoveByAlarmSourceId(Guid alarmSourceId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    _logger.LogInformation("开始删除设备{0}报警设置...", alarmSourceId);
                    List<AlarmSetting> deviceAlarmSettings = db.AlarmSetting.Where(p => p.AlarmSettingId.Equals(alarmSourceId)).ToList();
                    List<Guid> removePlanIds = new List<Guid>();
                    List<Guid> removeScheduleIds = new List<Guid>();
                    foreach (var setting in deviceAlarmSettings)
                    {
                        if (setting.BeforePlanId != null)
                            removePlanIds.Add(setting.BeforePlanId.Value);
                        if (setting.EmergencyPlanId != null)
                            removePlanIds.Add(setting.EmergencyPlanId.Value);
                        removeScheduleIds.Add(setting.ScheduleId);
                    }
                    db.AlarmSetting.RemoveRange(deviceAlarmSettings);

                    RemoveAlarmSettingPlan(db, removePlanIds);
                    RemoveAlarmSettingSchedule(db, removeScheduleIds);
                 
                    db.SaveChanges();
                    _logger.LogInformation("完成删除设备{0}报警设置...", alarmSourceId);
                }
                catch (Exception ex)
                {
                    _logger.LogError("删除设备{0}报警设置异常，Message:{1}\r\nStackTrace:{2}", alarmSourceId, ex.Message, ex.StackTrace);
                }
            }
            return NoContent();
        }

        //[HttpGet]
        //public IEnumerable<AlarmSetting> GetAll()
        //{
        //    using (var db = new AllInOneContext.AllInOneContext())
        //    {
        //        return db.AlarmSetting;
        //    }
        //}

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                AlarmSetting setting = db.AlarmSetting.Include(t=>t.EmergencyPlan).
                    Include(t=>t.BeforePlan).
                    FirstOrDefault(t => t.AlarmSettingId.Equals(id));
                if (setting == null || setting.AlarmSettingId.Equals(Guid.Empty))
                {
                    return NotFound();
                }
                return new OkObjectResult(setting);
            }
        }

        [HttpGet]
        [Route("~/Alarm/AlarmSetting/alarmSourceId={alarmSourceId}")]
        public IActionResult GetByAlarmSourceId(Guid alarmSourceId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    var setting = db.AlarmSetting.
                        Include(t=>t.Schedule).ThenInclude(t=>t.ScheduleCycle).ThenInclude(t=>t.DayPeriods).ThenInclude(t=>t.TimePeriods).
                        Include(t => t.EmergencyPlan).
                        Include(t => t.BeforePlan).Where(t => t.AlarmSourceId.Equals(alarmSourceId)).ToList();
                    //var setting = (from a in db.AlarmSetting
                    //               join b in db.Plan on a.EmergencyPlanId equals b.PlanId into emgercyPlans
                    //               from p1 in emgercyPlans.DefaultIfEmpty()
                    //               join c in db.Plan on a.BeforePlanId equals c.PlanId into beforePlans
                    //               from p2 in beforePlans
                    //               where a.AlarmSettingId.Equals(alarmSourceId)
                    //               select a).FirstOrDefault();

                    return new OkObjectResult(setting);
                }
                catch (Exception ex)
                {
                    _logger.LogError("查询设备报警配置异常，Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(ex);
                }
            }
        }

        ///// <summary>
        ///// 移除报警配置的预案
        ///// <param name="dbContext"></param>
        ///// <param name="planId"></param>
        ///// </summary>
        //private void RemoveAlarmSettingPlan(AllInOneContext.AllInOneContext dbContext, Guid planId)
        //{
        //    Plan delObj = GetPlanById(dbContext, planId);
        //    if (delObj != null && !delObj.PlanId.Equals(planId))
        //    {
        //        delObj.Actions.ForEach(t => dbContext.Set<PredefinedAction>().RemoveRange(t.PlanActions));
        //        dbContext.Set<PlanAction>().RemoveRange(delObj.Actions);
        //        dbContext.Plan.Remove(delObj);
        //        dbContext.SaveChanges();
        //    }
        //}

        ///// <summary>
        ///// 获取设备可配置的报警类型,有相同功能的接口，取消2016-11-30
        ///// </summary>
        ///// <param name="deviceTypeId"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("~/Alarm/AlarmSetting/AlarmType/deviceTypeId={deviceTypeId}")]
        //public IEnumerable<SystemOption> GetAlarmTypeByDeviceTypeId(Guid deviceTypeId)
        //{
        //    try
        //    {
        //        using (var db = new AllInOneContext.AllInOneContext())
        //        {
        //            var alarmTypes = db.Set<DeviceAlarmMapping>().Include(t => t.AlarmType).Where(t => t.DeviceTypeId.Equals(deviceTypeId)).
        //                OrderBy(t => t.AlarmType.SystemOptionCode).Select(t => t.AlarmType).ToList();
        //            return alarmTypes;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("获取设备类型可配置的报警类型数据异常，Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
        //        return null;
        //    }
        //}

        private Plan GetPlanById(AllInOneContext.AllInOneContext dbContext, Guid planId)
        {
            Plan plan = dbContext.Plan.Include(t => t.Actions).ThenInclude(t => t.PlanActions).
                        FirstOrDefault(t => t.PlanId.Equals(planId));
            return plan;
        }

        /// <summary>
        /// 根据设备类型id获取设备的报警类型定义
        /// </summary>
        /// <param name="deviceTypeId"></param>
        /// <returns></returns>
        [Route("~/Alarm/AlarmType/deviceTypeId={deviceTypeId}")]
        public IEnumerable<DeviceAlarmMapping> GetDeviceAlarmTypeMapping(Guid deviceTypeId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var deviceType = db.SystemOption.FirstOrDefault(t => t.SystemOptionId.Equals(deviceTypeId));
                if (deviceType != null)
                {
                    return db.DeviceAlarmMapping.Include(t => t.AlarmType).Include(t => t.DeviceType).Where(t => t.DeviceTypeId.Equals(deviceType.SystemOptionId) ||
                        t.DeviceTypeId.Equals(deviceType.ParentSystemOptionId)).OrderBy(t => t.AlarmType.SystemOptionCode).ToList();
                }
                return null;
            }
        }

        /// <summary>
        /// 根据设备类型id获取设备的报警类型定义，精简数据，仅返回报警类型
        /// </summary>
        /// <param name="deviceTypeId"></param>
        /// <returns></returns>
        [Route("~/Alarm/AlarmType/Option/deviceTypeId={deviceTypeId}")]
        public IEnumerable<SystemOption> GetDeviceAlarmOption(Guid deviceTypeId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var deviceType = db.SystemOption.FirstOrDefault(t => t.SystemOptionId.Equals(deviceTypeId));
                if (deviceType != null)
                {
                    return db.DeviceAlarmMapping.Include(t => t.AlarmType).Include(t=>t.DeviceType).Where(t => t.DeviceTypeId.Equals(deviceTypeId) ||
                        t.DeviceTypeId.Equals(deviceType.ParentSystemOptionId)).OrderBy(t => t.AlarmType.SystemOptionCode).Select(t => t.AlarmType).ToList();
                }
                return null;
            }
        }


        /// <summary>
        /// 移除报警配置的预案
        /// </summary>
        private void RemoveAlarmSettingPlan(AllInOneContext.AllInOneContext dbContext,List<Guid> planIds)
        {
            var plans = dbContext.Plan.Include(t => t.Actions).ThenInclude(t => t.PlanActions).ThenInclude(t => t.Action).
                       Include(t => t.Actions).ThenInclude(t => t.PlanDevice).ThenInclude(t => t.DeviceType).Where(t => planIds.Contains(t.PlanId)).ToList();
            foreach (var plan in plans)
            {
                plan.Actions.ForEach(t => dbContext.Set<PredefinedAction>().RemoveRange(t.PlanActions));
                dbContext.Set<PlanAction>().RemoveRange(plan.Actions);
                dbContext.Plan.Remove(plan);
            }
            dbContext.SaveChanges();
        }

        /// <summary>
        /// 移除报警关联的排程
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="planIds"></param>
        private void RemoveAlarmSettingSchedule(AllInOneContext.AllInOneContext dbContext, List<Guid> scheduleIds)
        {
            var shcedules = dbContext.Schedule
                                        .Include(t => t.ScheduleCycle).ThenInclude(t => t.DayPeriods).ThenInclude(t => t.TimePeriods)
                                        .Where(t => scheduleIds.Contains(t.ScheduleId)).ToList();
            shcedules.ForEach(t => {
                dbContext.Schedule.Remove(t);
                if (t.ScheduleCycle != null)
                {
                    dbContext.Set<ScheduleCycle>().Remove(t.ScheduleCycle);
                    if (t.ScheduleCycle.DayPeriods != null && t.ScheduleCycle.DayPeriods.Count > 0)
                    {
                        dbContext.Set<DayPeriod>().RemoveRange(t.ScheduleCycle.DayPeriods);
                        t.ScheduleCycle.DayPeriods.ForEach(f => dbContext.Set<TimePeriod>().RemoveRange(f.TimePeriods));
                    }
                }
            });
            dbContext.SaveChanges();
        }

        /// <summary>
        /// 批量保存报警配置
        /// 1.对比设备已有报警配置，移除要删除的报警配置；2.移除设备的所有排程
        /// </summary>
        /// <param name="alarmSettings"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("~/AlarmSetting/Save")]
        public IActionResult SaveAlarmSetting([FromBody]List<AlarmSetting> alarmSettings)
        {
            if (alarmSettings == null)
            {
                return BadRequest();
            }

            using (var db = new AllInOneContext.AllInOneContext())
            {
                using (var tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        _logger.LogInformation("开始批量更新报警设置....");
                        var alarmSettingGroup = alarmSettings.GroupBy(p => p.AlarmSourceId);
                        List<AlarmSetting> realRemoveAlarmSettings = new List<AlarmSetting>();
                        foreach (var alarmSetting in alarmSettingGroup)
                        {
                            var removeAlarmSettings = db.AlarmSetting.Where(t => t.AlarmSourceId.Equals(alarmSetting.Key)).ToList();
                            db.AlarmSetting.RemoveRange(removeAlarmSettings);
                            var saveAlarmSettingIds = alarmSetting.Select(t => t.AlarmSettingId).ToList();
                            realRemoveAlarmSettings.AddRange(removeAlarmSettings.Where(t => !saveAlarmSettingIds.Contains(t.AlarmSettingId)).ToList());
                        }
                        db.SaveChanges();

                        //移除实际已删除的设置的排程，预案
                        List<Guid> removePlanIds = new List<Guid>();
                        List<Guid> removeScheduleIds = new List<Guid>();
                        foreach (var alarmSetting in realRemoveAlarmSettings)
                        {
                            if (alarmSetting.BeforePlanId != null)
                                removePlanIds.Add(alarmSetting.BeforePlanId.Value);
                            if (alarmSetting.EmergencyPlanId != null)
                                removePlanIds.Add(alarmSetting.EmergencyPlanId.Value);
                            removeScheduleIds.Add(alarmSetting.ScheduleId);
                        }
                        alarmSettings.ForEach(t => removeScheduleIds.Add(t.ScheduleId)); //原有记录的排程先移除，后插入
                        RemoveAlarmSettingPlan(db, removePlanIds);
                        RemoveAlarmSettingSchedule(db, removeScheduleIds);

                        db.AlarmSetting.AddRange(alarmSettings);
                        db.SaveChanges();
                        tran.Commit();
                        _logger.LogInformation("完成批量更新报警设置....");
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        _logger.LogError("保存批量报警设置异常，Message:{0}\r\n{StackTrace{1}}", ex.Message, ex.StackTrace);
                        return BadRequest(ex.Message);
                    }
                }
            }
            return NoContent();
        }

        /// <summary>
        /// 获取用户定义的报警类型
        /// </summary>
        /// <param name="deviceTypeId"></param>
        /// <returns></returns>
        [Route("~/Alarm/AlarmType/Option/Selfdefine")]
        public IEnumerable<SystemOption> GetUserDefineAlarmType()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var alarmCatalogId = Guid.Parse("a0002016-e009-b019-e001-abcdef000108");
                return db.SystemOption.Where(t => alarmCatalogId.Equals(t.ParentSystemOptionId) && !t.Predefine).ToList();
            }
        }

    }
}
