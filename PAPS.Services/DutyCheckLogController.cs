using HttpClientEx;
using Infrastructure.Model;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PAPS.Data;
using PAPS.Model;
using System;
using System.Linq;


namespace PAPS.Services
{
    [Route("Paps/[controller]")]
    /// <summary>
    /// 查勤日志控制类
    /// </summary>
    public class DutyCheckLogController : Controller
    {

        /// <summary>
        /// 根据查勤日志ID获取查勤日志
        /// </summary>
        /// <param name="id">查勤日志ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                DutyCheckLog data = db.DutyCheckLog
                                    .Include(t => t.Apprises).ThenInclude(t => t.DutyCheckAppraise).ThenInclude(t=>t.AppraiseICO)
                                    .Include(t => t.Apprises).ThenInclude(t => t.DutyCheckAppraise).ThenInclude(t => t.AppraiseType)
                                    .Include(t => t.DutyCheckOperation).ThenInclude(t=>t.Attachments).ThenInclude(t=>t.Attachment)
                                    .Include(t => t.DutyCheckOperation).ThenInclude(t => t.Attachments).ThenInclude(t => t.AttachmentType)
                                    .Include(t => t.DutyCheckSiteSchedule)/*.ThenInclude(t=>t.CheckDutySite)*/
                                    .Include(t => t.DutyCheckSiteSchedule).ThenInclude(t => t.CheckMan)
                                    .Include(t => t.DutyCheckStaff)
                                    .Include(t => t.Organization)
                                    .Include(t => t.RecordType)
                                    .Include(t => t.Status)
                                    .Include(t=>t.CircularTypes).ThenInclude(t=>t.Dispose)
                                    .Include(t=>t.MainApprise)
                                    .FirstOrDefault(p => p.DutyCheckLogId.Equals(id));
                if (data == null)
                {
                    return NoContent();
                }

                return new ObjectResult(data);
            }
        }


        /// <summary>
        /// 新增查勤日志
        /// </summary>
        /// <param name="model">查勤日志实体</param>
        /// <returns>返回值</returns>
        [HttpPost]
        public IActionResult Add([FromBody]DutyCheckLog model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.DutyCheckLog.Add(model);
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
        /// 修改查勤日志
        /// </summary>
        /// <param name="model">查勤日志实体</param>
        /// <returns>返回值</returns>
        [HttpPut]
        public IActionResult Update([FromBody]DutyCheckLog model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    DutyCheckLog log=db.DutyCheckLog
                                     .Include(t => t.Apprises).ThenInclude(t => t.DutyCheckAppraise).ThenInclude(t => t.AppraiseICO)
                                    .Include(t => t.Apprises).ThenInclude(t => t.DutyCheckAppraise).ThenInclude(t => t.AppraiseType)
                                    .Include(t => t.DutyCheckOperation).ThenInclude(t => t.Attachments).ThenInclude(t => t.Attachment)
                                    .Include(t => t.DutyCheckOperation).ThenInclude(t => t.Attachments).ThenInclude(t => t.AttachmentType)
                                    .Include(t => t.DutyCheckSiteSchedule)/*.ThenInclude(t=>t.CheckDutySite)*/
                                    .Include(t => t.DutyCheckSiteSchedule).ThenInclude(t => t.CheckMan)
                                    .Include(t => t.DutyCheckStaff)
                                    .Include(t => t.Organization)
                                    .Include(t => t.RecordType)
                                    .Include(t => t.Status)
                                    .Include(t => t.CircularTypes).ThenInclude(t => t.Dispose)
                                    .Include(t => t.MainApprise)
                                    .FirstOrDefault(p => p.DutyCheckLogId.Equals(model.DutyCheckLogId));
                    if (log == null)
                        return BadRequest();
                    //转换数据
                    log.Apprises = model.Apprises;
                    log.CircularTypes = model.CircularTypes;
                    log.Description = model.Description;
                    log.DutyCheckOperationId = model.DutyCheckOperationId;
                    log.DutyCheckSiteScheduleId = model.DutyCheckSiteScheduleId;
                    log.DutyCheckStaffId = model.DutyCheckStaffId;
                    log.MainAppriseId = model.MainAppriseId;
                    log.RecordTime = model.RecordTime;
                    log.StatusId = model.StatusId;
                    //
                    db.DutyCheckLog.Update(log);
                    db.SaveChanges();

                    //判断是否为查勤包查勤，刷新查勤进度
                    if (model.RecordTypeId.Equals(new Guid("a0002016-e009-b019-e001-abcd18000001"))) //网络查勤
                    {
                        CheckPackageProcess(model.OrganizationId);
                    }
                    return new NoContentResult();
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
        ///  根据查勤日志ID删除查勤日志
        /// </summary>
        /// <param name="id">查勤日志ID</param>
        /// <returns>返回值</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteDutyCheckLog(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    DutyCheckLog data = db.DutyCheckLog.FirstOrDefault(p => p.DutyCheckLogId == id);
                    if (data == null)
                    {
                        return NoContent();
                    }
                    db.DutyCheckLog.Remove(data);
                    db.SaveChanges();
                    return new NoContentResult();
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }



        /// <summary>
        /// 根据参数获取查勤日志记录
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="currentPage">当前页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="filedStaffId">查勤日志人id</param>
        /// <param name="dutyCheckSiteScheduleId">实际执勤人id</param>
        /// <param name="organizationId">组织机构id</param>
        /// <param name="appriseId">评价id</param>
        /// <param name="recordTypeId">记录类型id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Paps/DutyCheckLog/Query")]
        public IActionResult GetDutyCheckLogByParameter(DateTime startTime, DateTime endTime, int currentPage,
            int pageSize, Guid?[] filedStaffId, Guid?[] dutyCheckSiteScheduleId, Guid? organizationId,
            Guid?[] appriseId, Guid?[] recordTypeId)
        {
            try
            {
                if (filedStaffId == null || filedStaffId.Length == 0)
                {

                }

                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var query = from p in db.DutyCheckLog
                                    .Include(t => t.Apprises).ThenInclude(t => t.DutyCheckAppraise).ThenInclude(t => t.AppraiseICO)
                                    .Include(t => t.Apprises).ThenInclude(t => t.DutyCheckAppraise).ThenInclude(t => t.AppraiseType)
                                    .Include(t => t.DutyCheckOperation).ThenInclude(t => t.Attachments).ThenInclude(t => t.Attachment)
                                    .Include(t => t.DutyCheckOperation).ThenInclude(t => t.Attachments).ThenInclude(t => t.AttachmentType)
                                    .Include(t => t.DutyCheckSiteSchedule)/*.ThenInclude(t => t.CheckDutySite)*/
                                    .Include(t => t.DutyCheckSiteSchedule).ThenInclude(t => t.CheckMan)
                                    .Include(t => t.DutyCheckStaff)
                                    .Include(t => t.Organization)
                                    .Include(t => t.RecordType)
                                    .Include(t => t.CircularTypes).ThenInclude(t => t.Dispose)
                                    .Include(t => t.MainApprise)
                                    .Include(t => t.AppraiseICO)
                                orderby p.RecordTime descending
                                where p.RecordTime >= startTime && p.RecordTime <= endTime
                                && ((filedStaffId == null || filedStaffId.Length == 0) || filedStaffId.Contains(p.DutyCheckStaff.StaffId))
                                && ((dutyCheckSiteScheduleId == null || dutyCheckSiteScheduleId.Length == 0 || p.DutyCheckSiteSchedule.CheckMan == null) || dutyCheckSiteScheduleId.Contains(p.DutyCheckSiteSchedule.CheckMan.StaffId))
                                && ((organizationId == null) || p.Organization.OrganizationId.Equals(organizationId))
                                && ( p.Status.SystemOptionId.Equals(new Guid("24AC9875-C463-47B6-8147-5845874C3CAF"))) //已结束 只显示已结束的记录
                                && ((appriseId == null || appriseId.Length == 0) || appriseId.Contains(p.MainAppriseId))
                                && ((recordTypeId == null || recordTypeId.Length == 0) || recordTypeId.Contains(p.RecordType.SystemOptionId))
                                select p;
                    if (currentPage == 0)
                        currentPage = 1;
                    if (pageSize <= 0)
                        pageSize = 10;

                    var data = query.Skip(pageSize * (currentPage - 1)).Take(pageSize * currentPage).ToList();

                    //if (data.ToList().Count == 0)
                    //{
                    //    return NoContent();
                    //}
                    QueryPagingRecord queryPagingRecord = new QueryPagingRecord
                    {
                        SumRecordCount = query.Count(),
                        Record = data.ToList()
                    };
                    return new ObjectResult(queryPagingRecord);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }


        /// <summary>
        /// 上报至上级机构
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("~/Paps/DutyCheckLog/Report")]
        [HttpPost]
        public IActionResult ReportToParentOrganization(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    DutyCheckLog data = db.DutyCheckLog
                                        .Include(t=>t.Organization)
                                        .FirstOrDefault(p => p.DutyCheckLogId == id);
                    if (data == null)
                    {
                        return NoContent();
                    }
                    Organization org = db.Organization
                                        .Include(t => t.ParentOrganization).ThenInclude(t => t.Center)
                                        .FirstOrDefault(p => p.OrganizationId.Equals(data.Organization.ParentOrganizationId));

                    if (org == null || org.Center == null || org.Center.EndPoints == null || org.Center.EndPoints.Count == 0)

                        if (org == null || org.Center == null || org.Center.EndPoints == null || org.Center.EndPoints.Count == 0)
                        return NoContent();

                    string url = string.Format("http://{0}:{1}/PAPS/DutyCheckLog", org.Center.EndPoints[0].IPAddress, org.Center.EndPoints[0].Port);
                    //更新状态
                    data.StatusId = new Guid("8E392E33-B63E-7801-685A-76634DDFF511");
                    db.SaveChanges();
                    //
                    data.OrganizationId = org.OrganizationId;
                    var result = HttpClientHelper.Post<DutyCheckLog>(data, url);
                    if (result.Success)
                    {
                        return new NoContentResult();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }



        ///// <summary>
        ///// 获取查勤进度
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("~/Paps/dutyCheckLog/organizationId={organizationId}")]
        //public IActionResult GetDutyCheckLogByProcess(Guid organizationId)
        //{
        //    using (var db = new AllInOneContext.AllInOneContext())
        //    {
        //        DutyCheckLog data = db.DutyCheckLog
        //                            .Include(t => t.Apprise).ThenInclude(t => t.AppraiseICO)
        //                            .Include(t => t.Apprise).ThenInclude(t => t.AppraiseType)
        //                            .Include(t => t.DutyCheckOperation).ThenInclude(t => t.Attachments).ThenInclude(t => t.Attachment)
        //                            .Include(t => t.DutyCheckOperation).ThenInclude(t => t.Attachments).ThenInclude(t => t.AttachmentType)
        //                            .Include(t => t.DutyCheckSiteSchedule).ThenInclude(t => t.CheckDutySite)
        //                            .Include(t => t.DutyCheckSiteSchedule).ThenInclude(t => t.CheckMan)
        //                            .Include(t => t.DutyCheckStaff)
        //                            .Include(t => t.Organization)
        //                            .Include(t => t.RecordType)
        //                            .Include(t => t.Status)
        //                            .FirstOrDefault(
        //                            p => p.OrganizationId.Equals(organizationId)
        //                            && );
        //        if (data == null)
        //        {
        //            return NoContent();
        //        }

        //        return new ObjectResult(data);
        //    }
        //}


        /// <summary>
        /// 检查查勤进度
        /// </summary>
        private  void CheckPackageProcess(Guid organizationId)
        {
            try
            {
                if (organizationId == null || organizationId == new Guid())
                    return;

                using (var db = new AllInOneContext.AllInOneContext())
                {
                    DutyCheckPackage package = db.DutyCheckPackage
                                .Include(t => t.DutyCheckPackLogs).ThenInclude(t => t.DutyCheckLog).ThenInclude(t => t.Status)
                                .FirstOrDefault(p => p.StartTime <= DateTime.Now && p.EndTime >= DateTime.Now);
                    if (package == null)
                    {
                        MQPulish.PublishMessage("DutyCheckPackageProcess", new DutyCheckPackageProcess { ProcessStatus = 0, Description = "尚未开始" });
                        return;
                    }

                    var endLogs = package.DutyCheckPackLogs.Where(p => p.DutyCheckLog.Status.SystemOptionCode.Equals("16000003")).Count();
                    int status = 0;
                    if (endLogs > 0)
                    {
                        status = 2;
                    }
                    DutyCheckPackageProcess process = new DutyCheckPackageProcess
                    {
                        Description = "",
                        EndTime = package.EndTime,
                        StartTime = package.StartTime,
                        Total = package.DutyCheckPackLogs.Count(),
                        CompetedCount = endLogs,
                        ProcessStatus = status
                    };

                    MQPulish.PublishMessage("DutyCheckPackageProcess", process);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("根据组织机构ID获取查勤进度状态：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// 获取当前检索条件的记录总数
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="currentPage">当前页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="filedStaffId">查勤日志人id</param>
        /// <param name="dutyCheckSiteScheduleId">实际执勤人id</param>
        /// <param name="organizationId">组织机构id</param>
        /// <param name="appriseId">评价id</param>
        /// <param name="recordTypeId">记录类型id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Paps/DutyCheckLog/QuerySum")]
        public IActionResult GetSumDutyCheckLogCount(DateTime startTime, DateTime endTime, Guid?[] filedStaffId, Guid?[] dutyCheckSiteScheduleId, Guid? organizationId,
            Guid?[] appriseId, Guid?[] recordTypeId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var query = from p in db.DutyCheckLog
                                    .Include(t => t.Apprises).ThenInclude(t => t.DutyCheckAppraise).ThenInclude(t => t.AppraiseICO)
                                    .Include(t => t.Apprises).ThenInclude(t => t.DutyCheckAppraise).ThenInclude(t => t.AppraiseType)
                                    .Include(t => t.DutyCheckOperation).ThenInclude(t => t.Attachments).ThenInclude(t => t.Attachment)
                                    .Include(t => t.DutyCheckOperation).ThenInclude(t => t.Attachments).ThenInclude(t => t.AttachmentType)
                                    .Include(t => t.DutyCheckSiteSchedule)
                                    .Include(t => t.DutyCheckSiteSchedule).ThenInclude(t => t.CheckMan)
                                    .Include(t => t.DutyCheckStaff)
                                    .Include(t => t.Organization)
                                    .Include(t => t.RecordType)
                                    .Include(t => t.Status)
                                    .Include(t => t.CircularTypes).ThenInclude(t => t.Dispose)
                                    .Include(t => t.MainApprise)
                                orderby p.RecordTime descending
                                where p.RecordTime >= startTime && p.RecordTime <= endTime
                                && ((filedStaffId == null || filedStaffId.Length == 0) || filedStaffId.Contains(p.DutyCheckStaff.StaffId))
                                && ((dutyCheckSiteScheduleId == null || dutyCheckSiteScheduleId.Length == 0 || p.DutyCheckSiteSchedule.CheckMan == null) || dutyCheckSiteScheduleId.Contains(p.DutyCheckSiteSchedule.CheckMan.StaffId))
                                && ((organizationId == null) || p.Organization.OrganizationId.Equals(organizationId))
                                && (p.Status.SystemOptionId.Equals(new Guid("24AC9875-C463-47B6-8147-5845874C3CAF"))) //已结束 只显示已结束的记录
                                && ((appriseId == null || appriseId.Length == 0) || appriseId.Contains(p.MainAppriseId))
                                && ((recordTypeId == null || recordTypeId.Length == 0) || recordTypeId.Contains(p.RecordType.SystemOptionId))
                                select p;

                    return new ObjectResult(query.Count());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }
    }
}
