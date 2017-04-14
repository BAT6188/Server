using AllInOneContext;
using Infrastructure.Model;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PAPS.Model;
using System;
using System.Collections.Generic;
using System.Linq;


namespace PAPS.Services
{
    [Route("Paps/[controller]")]
    /// <summary>
    /// 值班安排表控制类
    /// </summary>
    public class DutyScheduleController : Controller
    {



        /// <summary>
        /// 根据值班安排表ID获取值班安排表
        /// </summary>
        /// <param name="id">值班安排表ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {

                    DutySchedule data = db.DutySchedule
                                        .Include(t => t.Lister)
                                        .Include(t => t.Organization)
                                        .Include(t => t.Schedule).ThenInclude(t => t.ScheduleCycle).ThenInclude(t => t.DayPeriods).ThenInclude(t => t.TimePeriods)
                                        .Include(t => t.Schedule).ThenInclude(t => t.ScheduleType)
                                        .Include(t => t.DutyScheduleDetails).ThenInclude(t=>t.CadreSchedule)
                                        .Include(t => t.DutyScheduleDetails).ThenInclude(t => t.OfficerSchedule)
                                        .Include(t => t.DutyScheduleDetails).ThenInclude(t => t.NetWatcherSchedule).ThenInclude(t=>t.Leader)
                                        .Include(t => t.DutyScheduleDetails).ThenInclude(t => t.NetWatcherSchedule).ThenInclude(t => t.Deputy)
                                        .Include(t => t.DutyScheduleDetails).ThenInclude(t => t.NetWatcherSchedule).ThenInclude(t => t.CheckTimePeriod)
                                        .FirstOrDefault(p => p.DutyScheduleId.Equals(id));
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
        /// 新增值班安排表
        /// </summary>
        /// <param name="model">值班安排表实体</param>
        /// <returns>返回值</returns>
        [HttpPost]
        public IActionResult Add([FromBody]DutySchedule model)
        {

            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.DutySchedule.Add(model);
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
        /// 修改值班安排表
        /// </summary>
        /// <param name="model">值班安排表实体</param>
        /// <returns>返回值</returns>
        [HttpPut]
        public IActionResult Update([FromBody]DutySchedule model)
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
                        DutySchedule dutySchedule = db.DutySchedule
                                            .Include(t => t.Lister)
                                            .Include(t => t.Organization)
                                            .Include(t => t.Schedule).ThenInclude(t => t.ScheduleCycle).ThenInclude(t => t.DayPeriods).ThenInclude(t => t.TimePeriods)
                                            .Include(t => t.Schedule).ThenInclude(t => t.ScheduleType)
                                        .Include(t => t.DutyScheduleDetails).ThenInclude(t => t.CadreSchedule)
                                        .Include(t => t.DutyScheduleDetails).ThenInclude(t => t.OfficerSchedule)
                                        .Include(t => t.DutyScheduleDetails).ThenInclude(t => t.NetWatcherSchedule).ThenInclude(t => t.Leader)
                                        .Include(t => t.DutyScheduleDetails).ThenInclude(t => t.NetWatcherSchedule).ThenInclude(t => t.Deputy)
                                        .Include(t => t.DutyScheduleDetails).ThenInclude(t => t.NetWatcherSchedule).ThenInclude(t => t.CheckTimePeriod)
                                            .FirstOrDefault(p => p.DutyScheduleId.Equals(model.DutyScheduleId));
                        if (dutySchedule == null)
                            return BadRequest();
                        //转换普通数据
                        dutySchedule.EndDate = model.EndDate;
                        dutySchedule.ListerId = model.ListerId;
                        dutySchedule.OrganizationId = model.OrganizationId;
                        dutySchedule.StartDate = model.StartDate;
                        dutySchedule.TabulationTime = model.TabulationTime;
                        dutySchedule.ScheduleId = model.ScheduleId;
                        //
                        RemoveLinkage(db, dutySchedule);
                        //
                        dutySchedule.DutyScheduleDetails = model.DutyScheduleDetails;
                        //
                        db.DutySchedule.Update(dutySchedule);
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

        private static void RemoveLinkage(AllInOneContext.AllInOneContext db, DutySchedule dutySchedule)
        {
            foreach (DutyScheduleDetail dsd in dutySchedule.DutyScheduleDetails)
            {
                if (dsd.NetWatcherSchedule != null)
                {
                    List<DutyCheckSchedule> delList = new List<DutyCheckSchedule>();
                    foreach (DutyCheckSchedule rp in dsd.NetWatcherSchedule)
                    {
                        DutyCheckSchedule del = db.Set<DutyCheckSchedule>()
                            .FirstOrDefault(p => p.DutyCheckScheduleId.Equals(rp.DutyCheckScheduleId));
                        if (del != null)
                        {
                            delList.Add(del);
                        }
                    }
                    db.Set<DutyCheckSchedule>().RemoveRange(delList);
                    db.SaveChanges();
                }
            }


            if (dutySchedule.DutyScheduleDetails != null)
            {

                List<DutyScheduleDetail> delList = new List<DutyScheduleDetail>();
                foreach (DutyScheduleDetail detail in dutySchedule.DutyScheduleDetails)
                {

                    DutyScheduleDetail del = db.Set<DutyScheduleDetail>().FirstOrDefault(p => p.DutyScheduleDetailId.Equals(detail.DutyScheduleDetailId));
                    if (del != null)
                    {
                        delList.Add(del);
                    }
                }
                db.Set<DutyScheduleDetail>().RemoveRange(delList);
                db.SaveChanges();
            }
        }

        /// <summary>
        ///  根据值班安排表ID删除值班安排表
        /// </summary>
        /// <param name="id">值班安排表ID</param>
        /// <returns>返回值</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteDutySchedule(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        DutySchedule dutySchedule = db.DutySchedule
                                                    .Include(t => t.Lister)
                                                    .Include(t => t.Organization)
                                                    .Include(t => t.Schedule).ThenInclude(t => t.ScheduleCycle).ThenInclude(t => t.DayPeriods).ThenInclude(t => t.TimePeriods)
                                                    .Include(t => t.Schedule).ThenInclude(t => t.ScheduleType)
                                                    .Include(t => t.DutyScheduleDetails).ThenInclude(t => t.CadreSchedule)
                                                    .Include(t => t.DutyScheduleDetails).ThenInclude(t => t.OfficerSchedule)
                                                    .Include(t => t.DutyScheduleDetails).ThenInclude(t => t.NetWatcherSchedule).ThenInclude(t => t.Leader)
                                                    .Include(t => t.DutyScheduleDetails).ThenInclude(t => t.NetWatcherSchedule).ThenInclude(t => t.Deputy)
                                                    .Include(t => t.DutyScheduleDetails).ThenInclude(t => t.NetWatcherSchedule).ThenInclude(t => t.CheckTimePeriod)
                                                    .FirstOrDefault(p => p.DutyScheduleId.Equals(id));
                        if (dutySchedule == null)
                        {
                            return NoContent();
                        }
                        //
                        RemoveLinkage(db, dutySchedule);
                        //
                        db.DutySchedule.Remove(dutySchedule);
                        db.SaveChanges();

                        tran.Commit();
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
        /// 根据参数获取值班安排表记录
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="organizationId">组织机构ID</param>
        /// <param name="listerId">制表人ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Paps/DutySchedule/Query")]
        public IActionResult GetDutyScheduleByParameter(DateTime startTime, DateTime endTime, Guid organizationId, Guid? listerId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var data = from p in db.DutySchedule
                                .Include(t => t.Lister)
                                .Include(t => t.Organization)
                                .Include(t => t.Schedule).ThenInclude(t => t.ScheduleCycle).ThenInclude(t => t.DayPeriods).ThenInclude(t => t.TimePeriods)
                                .Include(t => t.Schedule).ThenInclude(t => t.ScheduleType)
                                .Include(t => t.DutyScheduleDetails).ThenInclude(t => t.CadreSchedule)
                                .Include(t => t.DutyScheduleDetails).ThenInclude(t => t.OfficerSchedule)
                                .Include(t => t.DutyScheduleDetails).ThenInclude(t => t.NetWatcherSchedule).ThenInclude(t => t.Leader)
                                .Include(t => t.DutyScheduleDetails).ThenInclude(t => t.NetWatcherSchedule).ThenInclude(t => t.Deputy)
                                .Include(t => t.DutyScheduleDetails).ThenInclude(t => t.NetWatcherSchedule).ThenInclude(t => t.CheckTimePeriod)
                               orderby p.StartDate ascending
                                where p.StartDate >= startTime && p.EndDate <= endTime
                                && p.Organization.OrganizationId.Equals(organizationId)
                                && ((listerId==null) ||p.Lister.StaffId.Equals(listerId))
                                select p;

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
