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
using System.Threading.Tasks;

namespace PAPS.Services
{
    [Route("Paps/[controller]")]
    /// <summary>
    ///分队-查勤安排表控制类
    /// </summary>
    public class UnitDutyCheckScheduleController : Controller
    {

        /// <summary>
        /// 根据分队-查勤安排表ID获取分队-查勤安排表
        /// </summary>
        /// <param name="id">分队-查勤安排表ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    UnitDutyCheckSchedule data = db.UnitDutyCheckSchedule
                                            .Include(t => t.Lister)
                                            .Include(t => t.Organization)
                                            .Include(t => t.UnitDutyCheckScheduleDetails).ThenInclude(t=>t.CheckMan)
                                            .Include(t=>t.Schedule).ThenInclude(t=>t.ScheduleCycle).ThenInclude(t=>t.CycleType)
                                            .Include(t => t.Schedule).ThenInclude(t => t.ScheduleCycle).ThenInclude(t => t.DayPeriods).ThenInclude(t=>t.TimePeriods)
                                            .Include(t=>t.Schedule).ThenInclude(t=>t.ScheduleType)
                                            .FirstOrDefault(p => p.UnitDutyCheckScheduleId.Equals(id));
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
        /// 新增分队-查勤安排表
        /// </summary>
        /// <param name="model">分队-查勤安排表实体</param>
        /// <returns>返回值</returns>
        [HttpPost]
        public IActionResult Add([FromBody]UnitDutyCheckSchedule model)
        {

            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.UnitDutyCheckSchedule.Add(model);
                    db.SaveChanges();
                    return Created("", "OK");
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
        /// 修改分队-查勤安排表
        /// </summary>
        /// <param name="model">分队-查勤安排表实体</param>
        /// <returns>返回值</returns>
        [HttpPut]
        public IActionResult Update([FromBody]UnitDutyCheckSchedule model)
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

                        UnitDutyCheckSchedule dutyGroupSchedule = db.UnitDutyCheckSchedule
                                            .Include(t => t.Lister)
                                            .Include(t => t.Organization)
                                            .Include(t => t.UnitDutyCheckScheduleDetails).ThenInclude(t => t.CheckMan)
                                            .Include(t => t.Schedule).ThenInclude(t => t.ScheduleCycle).ThenInclude(t => t.CycleType)
                                            .Include(t => t.Schedule).ThenInclude(t => t.ScheduleCycle).ThenInclude(t => t.DayPeriods).ThenInclude(t => t.TimePeriods)
                                            .Include(t => t.Schedule).ThenInclude(t => t.ScheduleType)
                                            .FirstOrDefault(p => p.UnitDutyCheckScheduleId.Equals(model.UnitDutyCheckScheduleId));
                        if (dutyGroupSchedule == null)
                            return BadRequest();
                        //
                        dutyGroupSchedule.EndDate = model.EndDate;
                        dutyGroupSchedule.IsCancel = model.IsCancel;
                        dutyGroupSchedule.ListerId = model.ListerId;
                        dutyGroupSchedule.OrganizationId = model.OrganizationId;
                        dutyGroupSchedule.ScheduleId = model.ScheduleId;
                        dutyGroupSchedule.StartDate = model.StartDate;
                        dutyGroupSchedule.TabulationTime = model.TabulationTime;
                        //
                        RemoveDetails(db, model);
                        //
                        dutyGroupSchedule.UnitDutyCheckScheduleDetails = model.UnitDutyCheckScheduleDetails;
                        //
                        db.UnitDutyCheckSchedule.Update(dutyGroupSchedule);
                        db.SaveChanges();
                        tran.Commit();
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


        /// <summary>
        /// 解除明细表的关联
        /// </summary>
        /// <param name="db"></param>
        /// <param name="model"></param>
        private void RemoveDetails(AllInOneContext.AllInOneContext db, UnitDutyCheckSchedule model)
        {
            List<UnitDutyCheckScheduleDetail> delList = new List<UnitDutyCheckScheduleDetail>();
            if (model.UnitDutyCheckScheduleDetails != null)
            {
                foreach (UnitDutyCheckScheduleDetail detail in model.UnitDutyCheckScheduleDetails)
                {
                    UnitDutyCheckScheduleDetail del = db.Set<UnitDutyCheckScheduleDetail>().FirstOrDefault(p => p.UnitDutyCheckScheduleDetailId.Equals(detail.UnitDutyCheckScheduleDetailId));
                    if (del != null)
                    {
                        delList.Add(del);
                    }
                }
            }
            db.Set<UnitDutyCheckScheduleDetail>().RemoveRange(delList);
            db.SaveChanges();
        }


        /// <summary>
        ///  根据分队-查勤安排表ID删除分队-查勤安排表
        /// </summary>
        /// <param name="id">分队-查勤安排表ID</param>
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
                        UnitDutyCheckSchedule dutyGroupSchedule = db.UnitDutyCheckSchedule
                                            .Include(t => t.Lister)
                                            .Include(t => t.Organization)
                                            .Include(t => t.UnitDutyCheckScheduleDetails).ThenInclude(t => t.CheckMan)
                                            .Include(t => t.Schedule).ThenInclude(t => t.ScheduleCycle).ThenInclude(t => t.CycleType)
                                            .Include(t => t.Schedule).ThenInclude(t => t.ScheduleCycle).ThenInclude(t => t.DayPeriods).ThenInclude(t => t.TimePeriods)
                                            .Include(t => t.Schedule).ThenInclude(t => t.ScheduleType)
                                            .FirstOrDefault(p => p.UnitDutyCheckScheduleId.Equals(id));
                        if (dutyGroupSchedule == null)
                            return BadRequest();
                        RemoveDetails(db, dutyGroupSchedule);
                        //
                        db.UnitDutyCheckSchedule.Remove(dutyGroupSchedule);
                        db.SaveChanges();
                        tran.Commit();
                        return new NoContentResult();

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
        /// 根据参数获取分队-查勤安排表记录
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="currentPage">当前页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="organizationId">组织机构id</param>
        /// <param name="listerId">制表人</param>
        /// <param name="tabulationtime">制表时间</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Paps/UnitDutyCheckSchedule/Query")]
        public IActionResult GetDutyGroupScheduleByParameter(DateTime startTime, DateTime endTime, int currentPage,
            int pageSize, Guid organizationId, Guid? listerId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var Query = from p in db.UnitDutyCheckSchedule
                                            .Include(t => t.Lister)
                                            .Include(t => t.Organization)
                                            .Include(t => t.UnitDutyCheckScheduleDetails).ThenInclude(t => t.CheckMan)
                                            .Include(t => t.Schedule).ThenInclude(t => t.ScheduleCycle).ThenInclude(t => t.CycleType)
                                            .Include(t => t.Schedule).ThenInclude(t => t.ScheduleCycle).ThenInclude(t => t.DayPeriods).ThenInclude(t => t.TimePeriods)
                                            .Include(t => t.Schedule).ThenInclude(t => t.ScheduleType)
                                orderby p.StartDate descending
                                where p.StartDate >= startTime && p.EndDate <= endTime
                                && p.Organization.OrganizationId.Equals(organizationId)
                                && ((listerId == null) || p.Lister.StaffId.Equals(listerId))
                                && !p.IsCancel
                                select p;
                    if (currentPage == 0)
                        currentPage = 1;
                    if (pageSize <= 0)
                        pageSize = 10;

                    var data = Query.Skip(pageSize * (currentPage - 1)).Take(pageSize * currentPage).ToList();

                    //string txt = JsonConvert.SerializeObject(data);

                    if (data.ToList().Count == 0)
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
    }

}
