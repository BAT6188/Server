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
    /// 分队勤务分组安排表控制类
    /// </summary>
    public class DutyGroupScheduleController : Controller
    {



        /// <summary>
        /// 根据分队勤务分组安排表ID获取分队勤务分组安排表
        /// </summary>
        /// <param name="id">分队勤务分组安排表ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    DutyGroupSchedule data = db.DutyGroupSchedule
                                            .Include(t => t.Lister)
                                            .Include(t => t.Organization)
                                            .Include(t => t.DutyGroupScheduleDetails).ThenInclude(t=>t.CheckMan)
                                            .Include(t => t.DutyGroupScheduleDetails).ThenInclude(t => t.CheckDutySiteSchedule)/*.ThenInclude(t => t.CheckDutySite).ThenInclude(t => t.Camera)*/
                                            .Include(t => t.DutyGroupScheduleDetails).ThenInclude(t => t.CheckDutySiteSchedule).ThenInclude(t => t.CheckMan)
                                            .Include(t => t.DutyGroupScheduleDetails).ThenInclude(t => t.CheckDutySiteSchedule).ThenInclude(t => t.SiteOrganization)
                                            .Include(t=>t.Schedule).ThenInclude(t=>t.ScheduleCycle).ThenInclude(t=>t.CycleType)
                                            .Include(t => t.Schedule).ThenInclude(t => t.ScheduleCycle).ThenInclude(t => t.DayPeriods).ThenInclude(t=>t.TimePeriods)
                                            .Include(t=>t.Schedule).ThenInclude(t=>t.ScheduleType)
                                            .Include(t=>t.Reservegroup).ThenInclude(t=>t.Staff)
                                            .Include(t => t.EmergencyTeam).ThenInclude(t => t.Staff)
                                            .FirstOrDefault(p => p.DutyGroupScheduleId.Equals(id));
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
        /// 新增分队勤务分组安排表
        /// </summary>
        /// <param name="model">分队勤务分组安排表实体</param>
        /// <returns>返回值</returns>
        [HttpPost]
        public IActionResult Add([FromBody]DutyGroupSchedule model)
        {

            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    //判断是否已存在编排
                    if (DutyGroupScheduleHelper.IsExist(model.StartDate, model.EndDate, model.OrganizationId))
                        return BadRequest(new ApplicationException { ErrorCode = "DataError", ErrorMessage = "当前时间已进行编排" });

                    if (CompletionData(model, db))
                    {
                        return BadRequest(new ApplicationException { ErrorCode = "DataError", ErrorMessage = "编排哨位节点下的监控点数据不存在" });
                    }
                    //
                    db.DutyGroupSchedule.Add(model);
                    db.SaveChanges();
                    //生成日数据
                    Task.Factory.StartNew(() =>
                    {
                        DutyGroupScheduleHelper.GetAbsoultedDutyCheckLog(model);

                    });
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
        /// 自动匹配该组织节点下的摄像机数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="db"></param>
        private static bool CompletionData(DutyGroupSchedule model, AllInOneContext.AllInOneContext db)
        {
            bool isCompleted = false;
            foreach (DutyGroupScheduleDetail dgsd in model.DutyGroupScheduleDetails)
            {
                foreach (DutyCheckSiteSchedule dcss in dgsd.CheckDutySiteSchedule)
                {
                    MonitorySite ms = DutyGroupScheduleHelper.GetMonitorySiteByOrganizationId(dcss.SiteOrganizationId.Value, db);
                    if (ms != null)
                    {
                        dcss.CheckDutySiteId = ms.MonitorySiteId;
                    }
                    else
                    {
                        isCompleted = true;
                    }
                }

            }
            return isCompleted;
        }

        /// <summary>
        /// 修改分队勤务分组安排表
        /// </summary>
        /// <param name="model">分队勤务分组安排表实体</param>
        /// <returns>返回值</returns>
        [HttpPut]
        public IActionResult Update([FromBody]DutyGroupSchedule model)
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

                        DutyGroupSchedule dutyGroupSchedule = db.DutyGroupSchedule
                                            .Include(t => t.Lister)
                                            .Include(t => t.Organization)
                                            .Include(t => t.DutyGroupScheduleDetails).ThenInclude(t => t.CheckMan)
                                            .Include(t => t.DutyGroupScheduleDetails).ThenInclude(t => t.CheckDutySiteSchedule)/*.ThenInclude(t => t.CheckDutySite).ThenInclude(t => t.Camera)*/
                                            .Include(t => t.DutyGroupScheduleDetails).ThenInclude(t => t.CheckDutySiteSchedule).ThenInclude(t => t.CheckMan)
                                            .Include(t => t.Reservegroup).ThenInclude(t => t.Staff)
                                            .Include(t => t.EmergencyTeam).ThenInclude(t => t.Staff)
                                            .FirstOrDefault(p => p.DutyGroupScheduleId.Equals(model.DutyGroupScheduleId));
                        if (dutyGroupSchedule == null)
                            return BadRequest();

                        //
                        if (dutyGroupSchedule.StartDate <= model.StartDate && dutyGroupSchedule.EndDate >= model.EndDate)
                        {
                            //判断是否为进行中
                            Delete(dutyGroupSchedule.DutyGroupScheduleId);
                        }
                        //删除未进行的查勤记录
                        var del = db.DutyCheckLog
                                .Where(p => p.PlanDate >= DateTime.Now
                                && p.PlanDate <= dutyGroupSchedule.EndDate
                                && p.StatusId != new Guid("361ADFE9-E58A-4C88-B191-B742CC212443"));

                        if (del != null && del.Count() != 0)
                        {
                            db.DutyCheckLog.RemoveRange(del.ToList());
                            db.SaveChanges();
                        }
                        //执行新增流程，重新对实体内属性GUID赋值
                        DutyGroupSchedule newModel = UpdateData(model);
                        //
                        if (CompletionData(newModel, db))
                        {
                            return BadRequest(new ApplicationException { ErrorCode = "DataError", ErrorMessage = "数据不齐" });
                        }
                        //
                        db.DutyGroupSchedule.Add(newModel);
                        db.SaveChanges();
                        //生成日数据
                        Task.Factory.StartNew(() =>
                        {
                            DutyGroupScheduleHelper.GetAbsoultedDutyCheckLog(newModel);

                        });
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
        /// 更新数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static DutyGroupSchedule UpdateData(DutyGroupSchedule model)
        {
            DutyGroupSchedule newModel = new DutyGroupSchedule();
            newModel.DutyGroupScheduleId = Guid.NewGuid();
            newModel.EndDate = model.EndDate;
            newModel.ListerId = model.ListerId;
            newModel.OrganizationId = model.OrganizationId;
            newModel.ScheduleId = model.ScheduleId;
            newModel.StartDate = model.StartDate;
            newModel.TabulationTime = model.TabulationTime;
            //
            //明细
            List<DutyGroupScheduleDetail> list = new List<DutyGroupScheduleDetail>();
            foreach (DutyGroupScheduleDetail dgsd in model.DutyGroupScheduleDetails)
            {
                dgsd.DutyGroupScheduleDetailId = Guid.NewGuid();
                List<DutyCheckSiteSchedule> dcss = new List<DutyCheckSiteSchedule>();
                foreach (DutyCheckSiteSchedule dcs in dgsd.CheckDutySiteSchedule)
                {
                    dcs.DutyCheckSiteScheduleId = Guid.NewGuid();
                    dcss.Add(dcs);
                }
                dgsd.CheckDutySiteSchedule = dcss;
                list.Add(dgsd);
            }
            //备勤组
            List<Reservegroup> reservegroup = new List<Reservegroup>();
            foreach (Reservegroup rg in model.Reservegroup)
            {
                rg.DutyGroupScheduleId = Guid.NewGuid();
                reservegroup.Add(rg);
            }
            newModel.Reservegroup = reservegroup;
            //应急小组
            List<EmergencyTeam> emergencyteam = new List<EmergencyTeam>();
            foreach (EmergencyTeam et in model.EmergencyTeam)
            {
                et.DutyGroupScheduleId = Guid.NewGuid();
                emergencyteam.Add(et);
            }
            newModel.EmergencyTeam = emergencyteam;
            newModel.DutyGroupScheduleDetails = list;
            return newModel;
        }

        private static void RemoveLinkage(AllInOneContext.AllInOneContext db, DutyGroupSchedule dutyGroupSchedule)
        {
            //删除未进行的查勤记录
            var dellog = db.DutyCheckLog
                    .Where(p => p.PlanDate >= dutyGroupSchedule.StartDate
                    && p.PlanDate <= dutyGroupSchedule.EndDate
                    && p.StatusId == new Guid("361ADFE9-E58A-4C88-B191-B742CC212443"));

            if (dellog != null && dellog.Count() != 0)
            {
                db.DutyCheckLog.RemoveRange(dellog.ToList());
                db.SaveChanges();
            }

            List<DutyGroupScheduleDetail> mainList = new List<DutyGroupScheduleDetail>();
            if (dutyGroupSchedule.DutyGroupScheduleDetails != null)
            {
                List<DutyCheckSiteSchedule> dutyCheckSiteSchedulelList = new List<DutyCheckSiteSchedule>();
                foreach (DutyGroupScheduleDetail detail in dutyGroupSchedule.DutyGroupScheduleDetails)
                {

                    // 解除DutyCheckSiteSchedule Many To Many
                    if (detail.CheckDutySiteSchedule != null)
                    {

                        foreach (DutyCheckSiteSchedule rp in detail.CheckDutySiteSchedule)
                        {
                            DutyCheckSiteSchedule del = db.Set<DutyCheckSiteSchedule>()
                                .FirstOrDefault(p => p.DutyCheckSiteScheduleId.Equals(rp.DutyCheckSiteScheduleId));
                            if (del != null)
                            {
                                dutyCheckSiteSchedulelList.Add(del);
                            }
                        }
                    }
                }
                db.Set<DutyCheckSiteSchedule>().RemoveRange(dutyCheckSiteSchedulelList);
                db.SaveChanges();

                // DutyGroupScheduleDetail Many To Many
                List<DutyGroupScheduleDetail> detailList = new List<DutyGroupScheduleDetail>();
                foreach (DutyGroupScheduleDetail detail in dutyGroupSchedule.DutyGroupScheduleDetails)
                {

                    DutyGroupScheduleDetail del = db.Set<DutyGroupScheduleDetail>()
                                .FirstOrDefault(p => p.DutyGroupScheduleDetailId.Equals(detail.DutyGroupScheduleDetailId));
                    if (del != null)
                    {
                        detailList.Add(del);
                    }
                }
                db.Set<DutyGroupScheduleDetail>().RemoveRange(detailList);
                db.SaveChanges();
            }



                // 解除EmergencyTeam Many To Many
                if (dutyGroupSchedule.EmergencyTeam != null)
                {
                    List<EmergencyTeam> delList = new List<EmergencyTeam>();
                    foreach (EmergencyTeam rp in dutyGroupSchedule.EmergencyTeam)
                    {
                        EmergencyTeam del = db.Set<EmergencyTeam>()
                            .FirstOrDefault(p => p.DutyGroupScheduleId.Equals(rp.DutyGroupScheduleId) && p.StaffId.Equals(rp.StaffId));
                        if (del != null)
                        {
                            delList.Add(del);
                        }
                    }
                    db.Set<EmergencyTeam>().RemoveRange(delList);
                    db.SaveChanges();
                }

                // 解除Reservegroup Many To Many
                if (dutyGroupSchedule.Reservegroup != null)
                {
                    List<Reservegroup> delList = new List<Reservegroup>();
                    foreach (Reservegroup rp in dutyGroupSchedule.Reservegroup)
                    {
                        Reservegroup del = db.Set<Reservegroup>()
                            .FirstOrDefault(p => p.DutyGroupScheduleId.Equals(rp.DutyGroupScheduleId) && p.StaffId.Equals(rp.StaffId));
                        if (del != null)
                        {
                            delList.Add(del);
                        }
                    }
                    db.Set<Reservegroup>().RemoveRange(delList);
                    db.SaveChanges();
                }
        }

        /// <summary>
        ///  根据分队勤务分组安排表ID删除分队勤务分组安排表
        /// </summary>
        /// <param name="id">分队勤务分组安排表ID</param>
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
                        DutyGroupSchedule dutyGroupSchedule = db.DutyGroupSchedule
                                            .Include(t => t.Lister)
                                            .Include(t => t.Organization)
                                            .Include(t => t.DutyGroupScheduleDetails).ThenInclude(t => t.CheckMan)
                                            .Include(t => t.DutyGroupScheduleDetails).ThenInclude(t => t.CheckDutySiteSchedule)/*.ThenInclude(t => t.CheckDutySite).ThenInclude(t => t.Camera)*/
                                            .Include(t => t.DutyGroupScheduleDetails).ThenInclude(t => t.CheckDutySiteSchedule).ThenInclude(t => t.CheckMan)
                                            .Include(t => t.Reservegroup).ThenInclude(t => t.Staff)
                                            .Include(t => t.EmergencyTeam).ThenInclude(t => t.Staff)
                                            .FirstOrDefault(p => p.DutyGroupScheduleId.Equals(id));
                        if (dutyGroupSchedule == null)
                        {
                            return NoContent();
                        }
                        if (DutyGroupScheduleHelper.IsInWork(dutyGroupSchedule.StartDate, dutyGroupSchedule.EndDate, dutyGroupSchedule.OrganizationId) > 0)
                        {
                            dutyGroupSchedule.IsCancel = true;
                            db.DutyGroupSchedule.Update(dutyGroupSchedule);
                            db.SaveChanges();
                        }
                        else
                        {
                            //
                            RemoveLinkage(db, dutyGroupSchedule);
                            //
                            db.DutyGroupSchedule.Remove(dutyGroupSchedule);
                            db.SaveChanges();
                        }
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
        /// 根据参数获取分队勤务分组安排表记录
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
        [Route("~/Paps/DutyGroupSchedule/Query")]
        public IActionResult GetDutyGroupScheduleByParameter(DateTime startTime, DateTime endTime, int currentPage,
            int pageSize, Guid organizationId, Guid? listerId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var Query = from p in db.DutyGroupSchedule
                                .Include(t => t.Lister)
                                .Include(t => t.Organization)
                                .Include(t => t.DutyGroupScheduleDetails).ThenInclude(t => t.CheckMan)
                                .Include(t => t.DutyGroupScheduleDetails).ThenInclude(t => t.CheckDutySiteSchedule)/*.ThenInclude(t => t.CheckDutySite).ThenInclude(t=>t.Camera)*/
                                .Include(t => t.DutyGroupScheduleDetails).ThenInclude(t => t.CheckDutySiteSchedule).ThenInclude(t => t.CheckMan)
                                .Include(t => t.DutyGroupScheduleDetails).ThenInclude(t => t.CheckDutySiteSchedule).ThenInclude(t => t.SiteOrganization)
                                .Include(t => t.Schedule).ThenInclude(t => t.ScheduleCycle).ThenInclude(t => t.DayPeriods).ThenInclude(t => t.TimePeriods)
                                .Include(t => t.Schedule).ThenInclude(t => t.ScheduleType)
                                .Include(t => t.Reservegroup).ThenInclude(t => t.Staff)
                                .Include(t => t.EmergencyTeam).ThenInclude(t => t.Staff)
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

        /// <summary>
        /// 获取日安排
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="organizationId">组织机构ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Paps/DutyGroupSchedule/date={date}&organizationId={organizationId}")]
        public IActionResult GetDayDutyGroupSchedule(DateTime date,Guid organizationId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    DateTime time = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);

                    var data = from p in db.DutyCheckLog
                                    .Include(t => t.Apprises).ThenInclude(t=>t.DutyCheckAppraise).ThenInclude(t => t.AppraiseICO)
                                    .Include(t => t.Apprises).ThenInclude(t => t.DutyCheckAppraise).ThenInclude(t => t.AppraiseType)
                                    .Include(t => t.DutyCheckOperation).ThenInclude(t => t.Attachments).ThenInclude(t => t.Attachment)
                                    .Include(t => t.DutyCheckOperation).ThenInclude(t => t.Attachments).ThenInclude(t => t.AttachmentType)
                                    .Include(t => t.DutyCheckSiteSchedule)/*.ThenInclude(t => t.CheckDutySite)*/
                                    .Include(t => t.DutyCheckSiteSchedule).ThenInclude(t => t.SiteOrganization)
                                    .Include(t => t.DutyCheckSiteSchedule).ThenInclude(t => t.CheckMan)
                                    .Include(t => t.DutyCheckStaff)
                                    .Include(t => t.Organization)
                                    .Include(t => t.RecordType)
                                    .Include(t => t.Status)
                                    
                              where p.PlanDate.Equals(time) && p.OrganizationId.Equals(organizationId)
                              select p;
                    if (data.ToList().Count == 0)
                    {
                        return NoContent();
                    }

                    List<DayDutyGroupScheduleView> list = new List<DayDutyGroupScheduleView>();
                    foreach (DutyCheckLog log in data.ToList())
                    {
                        string Sentinel = "网络查勤员";
                        if (log.DutyCheckSiteSchedule!=null)
                        {
                            Sentinel = log.DutyCheckSiteSchedule.SiteOrganization.OrganizationShortName;
                        }
                        DayDutyGroupScheduleView View = new DayDutyGroupScheduleView
                        {
                            DutyCheckLogId = log.DutyCheckLogId,
                            TimeInterval = log.TimePeriod.StartTime.ToString("HH:mm:ss") + "-" + log.TimePeriod.EndTime.ToString("HH:mm:ss"),
                            StaffName = log.DutyCheckStaff.StaffName,
                            Sentinel = Sentinel,
                            OrderNo=log.TimePeriod.OrderNo
                        };
                        list.Add(View);
                    }
                    var orderbylist = list.OrderBy(p => p.OrderNo);

                    return new ObjectResult(orderbylist);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }

        }




        /// <summary>
        /// 手动调整日安排中的人员排班
        /// </summary>
        /// <param name="dutyCheckLogId">查勤日志ID</param>
        /// <param name="staffId">人员ID</param>
        /// <returns></returns>
        [HttpPut]
        [Route("~/Paps/DutyGroupSchedule/dutyCheckLogId={dutyCheckLogId}&staffId={staffId}")]
        public IActionResult UpdateDaySchedule(Guid dutyCheckLogId,Guid staffId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        DutyCheckLog log = db.DutyCheckLog
                                          .Include(t=>t.DutyCheckSiteSchedule) 
                                          .FirstOrDefault(p => p.DutyCheckLogId.Equals(dutyCheckLogId));
                            //查询当天的同一时段内是否已存在该人员排班
                            var list = db.DutyCheckLog
                                .Where(p => p.PlanDate.Equals(log.PlanDate)
                                && p.TimePeriodJson.Equals(log.TimePeriodJson)
                                && p.DutyCheckLogId != dutyCheckLogId
                                && p.DutyCheckStaffId.Equals(staffId));
                            if (list.Count() != 0)
                            {
                                return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "同一时段已存在该人员排班" });

                            }
                            log.DutyCheckStaffId = staffId;
                            if (log.DutyCheckSiteSchedule != null)
                            {
                                log.DutyCheckSiteSchedule.CheckManId = staffId;
                            }
                            db.DutyCheckLog.Update(log);
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
    }

}
