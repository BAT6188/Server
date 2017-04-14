using AllInOneContext;
using Infrastructure.Model;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PAPS.Model;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;



namespace PAPS.Services
{
    [Route("Paps/[controller]")]
    /// <summary>
    /// 查勤包计划控制类
    /// </summary>
    public class DutyCheckPackageTimePlanController : Controller
    {
        /// <summary>
        /// 获取查勤计划
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAll()
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var data = db.DutyCheckPackageTimePlan
                                                    .Include(t => t.Schedule).ThenInclude(t => t.ScheduleCycle).ThenInclude(t => t.DayPeriods).ThenInclude(t => t.TimePeriods)
                                                    .Include(t => t.Schedule).ThenInclude(t => t.ScheduleCycle).ThenInclude(t => t.CycleType)
                                                    .Include(t => t.Schedule).ThenInclude(t => t.ScheduleType)
                                                    .Include(t => t.Organization);
                                                    
                    if (data == null)
                    {
                        return NoContent();
                    }
                    return new ObjectResult(data.ToList());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 根据查勤包计划ID获取查勤包计划
        /// </summary>
        /// <param name="id">查勤包计划ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    DutyCheckPackageTimePlan data = db.DutyCheckPackageTimePlan
                                                    .Include(t => t.Schedule).ThenInclude(t => t.ScheduleCycle).ThenInclude(t => t.DayPeriods).ThenInclude(t => t.TimePeriods)
                                                    .Include(t => t.Schedule).ThenInclude(t => t.ScheduleCycle).ThenInclude(t => t.CycleType)
                                                    .Include(t=>t.Schedule).ThenInclude(t=>t.ScheduleType)
                                                    .Include(t=>t.Organization)
                                                    .FirstOrDefault(p => p.DutyCheckPackageTimePlanId.Equals(id));
                    if (data == null)
                    {
                        return NoContent();
                    }
                    return new ObjectResult(data);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }


        /// <summary>
        /// 新增查勤包计划
        /// </summary>
        /// <param name="model">查勤包计划实体</param>
        /// <returns>返回值</returns>
        [HttpPost]
        public IActionResult Add([FromBody]DutyCheckPackageTimePlan model)
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
                        DutyCheckPackageTimePlan plan = db.DutyCheckPackageTimePlan
                            .FirstOrDefault(p => p.OrganizationId.Equals(model.OrganizationId));
                        if (plan != null) //每级节点只允许存在一个查勤包计划表
                        {
                            return BadRequest(new ApplicationException { ErrorCode = "Exist", ErrorMessage = "已存在查勤计划" });
                        }

                        db.DutyCheckPackageTimePlan.Add(model);
                        db.SaveChanges();

                        tran.Commit();
                        DutyCheckPackageHelper.AllocationDutychekPackage(plan.OrganizationId, DateTime.Now);
                        return Created("", "OK");
                    }
                }
            }
            catch (DbUpdateException dbEx)
            {
                return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 修改查勤包计划
        /// </summary>
        /// <param name="model">查勤包计划实体</param>
        /// <returns>返回值</returns>
        [HttpPut]
        public IActionResult Update([FromBody]DutyCheckPackageTimePlan model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        DutyCheckPackageTimePlan plan = db.DutyCheckPackageTimePlan
                                                        .Include(t => t.Schedule).ThenInclude(t => t.ScheduleCycle).ThenInclude(t => t.DayPeriods).ThenInclude(t => t.TimePeriods)
                                                        .Include(t => t.Schedule).ThenInclude(t => t.ScheduleCycle).ThenInclude(t => t.CycleType)
                                                        .Include(t => t.Organization)
                                                        .FirstOrDefault(p => p.DutyCheckPackageTimePlanId.Equals(model.DutyCheckPackageTimePlanId));
                        if (plan == null)
                        {
                            return BadRequest();
                        }
                        //转换普通数据
                        plan.OrganizationId = model.OrganizationId;
                        plan.RandomRate = model.RandomRate;

                        //更新排程中查勤设置
                        UpdateSchedule(model,db,plan);
                        //
                        plan.ScheduleId = model.ScheduleId;
                        //
                        DutyCheckPackageHelper.DutyCheckPackageTimePlanOnChange(plan.OrganizationId);
                        //
                        db.DutyCheckPackageTimePlan.Update(plan);
                        db.SaveChanges();

                        transaction.Commit();
                        DutyCheckPackageHelper.AllocationDutychekPackage(plan.OrganizationId, DateTime.Now);
                        return new NoContentResult();
                    }
                }
            }
            catch (DbUpdateException dbEx)
            {
                return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }

        private void UpdateSchedule(DutyCheckPackageTimePlan model, AllInOneContext.AllInOneContext db, DutyCheckPackageTimePlan oldPlan)
        {
            if (model.Schedule == null)
                return;
            Guid scheduleId = oldPlan.ScheduleId;
            if (oldPlan.ScheduleId != model.ScheduleId)
            {
                scheduleId = model.ScheduleId;
            }

            //转换一般数据
            Schedule shedule = db.Schedule
                            .Include(t => t.ScheduleType)
                            .Include(t => t.ScheduleCycle).ThenInclude(t => t.DayPeriods)
                            .Include(t => t.ScheduleCycle).ThenInclude(t => t.CycleType)
                            .FirstOrDefault(p => p.ScheduleId.Equals(scheduleId));

            if (shedule == null)
            {
                return;
            }

            shedule.EffectiveTime = model.Schedule.EffectiveTime;
            shedule.ExpirationTime = model.Schedule.ExpirationTime;
            shedule.ScheduleTypeId = model.Schedule.ScheduleTypeId;
            shedule.ScheduleName = model.Schedule.ScheduleName;
            //
            ScheduleCycle scheduleCycle = db.Set<ScheduleCycle>()
                                        .Include(t => t.DayPeriods)
                                        .FirstOrDefault(p => p.ScheduleCycleId.Equals(shedule.ScheduleCycle.ScheduleCycleId));
            //转换ScheduleCycle
            scheduleCycle.CycleTypeId = model.Schedule.ScheduleCycle.CycleTypeId;
            scheduleCycle.Days = model.Schedule.ScheduleCycle.Days;
            scheduleCycle.DaysJson = model.Schedule.ScheduleCycle.DaysJson;
            scheduleCycle.LastExecute = model.Schedule.ScheduleCycle.LastExecute;
            scheduleCycle.Months = model.Schedule.ScheduleCycle.Months;
            scheduleCycle.MonthsJson = model.Schedule.ScheduleCycle.MonthsJson;
            scheduleCycle.NextExecute = model.Schedule.ScheduleCycle.NextExecute;
            scheduleCycle.WeekDayJson = model.Schedule.ScheduleCycle.WeekDayJson;
            scheduleCycle.WeekDays = model.Schedule.ScheduleCycle.WeekDays;
            //
            UpdateTimePeriod(db, shedule, model);
            //
            //scheduleCycle.DayPeriods = model.Schedule.ScheduleCycle.DayPeriods;
            shedule.ScheduleCycle = scheduleCycle;
            db.Schedule.Update(shedule);
            db.SaveChanges();
        }

        /// <summary>
        /// 手动移除实体中的List属性
        /// </summary>
        /// <param name="db"></param>
        /// <param name="shedule"></param>
        private static void UpdateTimePeriod(AllInOneContext.AllInOneContext db, Schedule shedule, DutyCheckPackageTimePlan model)
        {
            foreach (DayPeriod dp in model.Schedule.ScheduleCycle.DayPeriods)
            {
                foreach (TimePeriod tp in dp.TimePeriods)
                {
                    TimePeriod timePeriod = db.Set<TimePeriod>().FirstOrDefault(p => p.TimePeriodId.Equals(tp.TimePeriodId));
                    if (timePeriod != null)
                    {
                        timePeriod.ExtraJson = tp.ExtraJson;
                        db.Set<TimePeriod>().Update(timePeriod);
                        db.SaveChanges();
                    }
                }
            }
        }



        /// <summary>
        ///  根据查勤包计划ID删除查勤包计划
        /// </summary>
        /// <param name="id">查勤包计划ID</param>
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
                        DutyCheckPackageTimePlan plan = db.DutyCheckPackageTimePlan
                                                        .Include(t => t.Schedule).ThenInclude(t => t.ScheduleCycle).ThenInclude(t => t.DayPeriods).ThenInclude(t => t.TimePeriods)
                                                        .Include(t => t.Schedule).ThenInclude(t => t.ScheduleCycle).ThenInclude(t => t.CycleType)
                                                        .Include(t => t.Organization)
                                                        .FirstOrDefault(p => p.DutyCheckPackageTimePlanId.Equals(id));
                        if (plan == null)
                        {
                            return NoContent();
                        }

                        db.DutyCheckPackageTimePlan.Remove(plan);
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
        /// 根据组织机构ID本机查勤包计划
        /// </summary>
        /// <param name="organizationId">组织机构ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Paps/DutyCheckPackageTimePlan/organizationId={organizationId}")]
        public IActionResult GetDutyCheckPackageTimePlanByOrganization(Guid organizationId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    DutyCheckPackageTimePlan data = db.DutyCheckPackageTimePlan
                                                    .Include(t => t.Schedule).ThenInclude(t => t.ScheduleCycle).ThenInclude(t => t.DayPeriods).ThenInclude(t => t.TimePeriods)
                                                    .Include(t => t.Schedule).ThenInclude(t => t.ScheduleCycle).ThenInclude(t => t.CycleType)
                                                    .Include(t => t.Organization)
                                                    .Include(t => t.Schedule).ThenInclude(t => t.ScheduleType)
                        .FirstOrDefault(p => p.OrganizationId.Equals(organizationId));
                    if (data == null)
                    {
                        return NoContent();
                    }
                    return new ObjectResult(data);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }


        /// <summary>
        /// 根据组织机构ID查勤监控点数量
        /// </summary>
        /// <param name="addressId">组织机构ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Paps/DutyCheckPackageTimePlan/addressId={addressId}")]
        public IActionResult GetAllMonitorSiteCountPlanByOrganization(Guid addressId)
        {
            try
            {
                //int count = DutyCheckPackageHelper.GetAllMonitorySite(addressId).Count;
                int count = DutyCheckPackageHelper.GetAllMonitorySite(addressId);
                return new ObjectResult(count);

            }
            catch (Exception ex)
            {
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }





    }
}
