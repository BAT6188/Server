using HttpClientEx;
using Infrastructure.Data;
using Infrastructure.Model;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PAPS.Data;
using PAPS.Model;
using Resources.Model;
using Surveillance.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PAPS.Services
{
    [Route("Paps/[controller]")]
    /// <summary>
    /// 查勤控制类
    /// </summary>
    public class DutyCheckController: Controller
    {

        private readonly ILogger<DutyCheckController> _logger;
        public DutyCheckController(ILogger<DutyCheckController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 获取值班交接情况
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Paps/DutyCheck/Duty")]
        public IActionResult GetDutyDetail(Guid sentinelId)
        {
            try
            {
                DutyDetail dutyDetail = GetDutyDetailBySentinelId(sentinelId);
                if (dutyDetail == null)
                    return NoContent();
                return new ObjectResult(dutyDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError("获取值班交接情况：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }

        public  DutyDetail GetDutyDetailBySentinelId(Guid sentinelId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                DateTime time = DateTime.Now;
                var Sentinel = db.IPDeviceInfo
                            .FirstOrDefault(p => p.IPDeviceInfoId.Equals(sentinelId));
                if (Sentinel == null)
                {
                    return null;
                }
                //取当前记录
                var logs = db.DutyCheckLog
                                 .Include(t => t.DutyCheckSiteSchedule).ThenInclude(t => t.CheckMan).ThenInclude(t => t.Organization)
                                 .Include(t => t.DutyCheckSiteSchedule).ThenInclude(t => t.CheckMan).ThenInclude(t => t.PositionType)
                                 .Include(t => t.DayPeriod).ThenInclude(t => t.TimePeriods)
                                 .Where(p => p.DutyCheckSiteSchedule != null
                                 && new DateTime(p.PlanDate.Value.Year, p.PlanDate.Value.Month, p.PlanDate.Value.Day, p.TimePeriod.StartTime.Hour, p.TimePeriod.StartTime.Minute, p.TimePeriod.StartTime.Second) <= time
                                 && new DateTime(p.PlanDate.Value.Year, p.PlanDate.Value.Month, p.PlanDate.Value.Day, p.TimePeriod.EndTime.Hour, p.TimePeriod.EndTime.Minute, p.TimePeriod.EndTime.Second) >= time);
                if (logs == null)
                    return null;
                DutyCheckLog log = null; 
                foreach (DutyCheckLog dcl in logs) //由于DutyCheckSiteScheduleId也可能为空，暂这样处理
                {
                    if (dcl.DutyCheckSiteSchedule != null && dcl.DutyCheckSiteSchedule.SiteOrganizationId.Equals(Sentinel.OrganizationId))
                    {
                        log = dcl;
                        break;
                    }
                }
                if (log == null)
                    return null;
                //取前一时段的记录
                int day = 0;
                int oldOrderNo = log.TimePeriod.OrderNo - 1; 
                if (oldOrderNo <= 0)
                {
                    oldOrderNo = log.DayPeriod.TimePeriods.Max(p => p.OrderNo);
                    day = -1;
                }
                TimePeriod oldTimePeriod = log.DayPeriod.TimePeriods.Find(p => p.OrderNo.Equals(oldOrderNo));
                //
                DateTime oldStartTime = new DateTime(time.AddDays(day).Date.Year, time.AddDays(day).Date.Month, time.AddDays(day).Date.Day, oldTimePeriod.StartTime.Hour, oldTimePeriod.StartTime.Minute, oldTimePeriod.StartTime.Second);
                DateTime oldEndTime = new DateTime(time.AddDays(day).Date.Year, time.AddDays(day).Date.Month, time.AddDays(day).Date.Day, oldTimePeriod.EndTime.Hour, oldTimePeriod.EndTime.Minute, oldTimePeriod.EndTime.Second);
                DutyCheckLog oldlog = null;
                var oldlogs = db.DutyCheckLog
                                 .Include(t => t.DutyCheckSiteSchedule).ThenInclude(t => t.CheckMan).ThenInclude(t => t.Organization)
                                 .Include(t => t.DutyCheckSiteSchedule).ThenInclude(t => t.CheckMan).ThenInclude(t => t.PositionType)
                                 .Include(t => t.DayPeriod).ThenInclude(t => t.TimePeriods)
                                 .Where(p => p.DutyCheckSiteSchedule != null
                                 && new DateTime(p.PlanDate.Value.Year, p.PlanDate.Value.Month, p.PlanDate.Value.Day, p.TimePeriod.StartTime.Hour, p.TimePeriod.StartTime.Minute, p.TimePeriod.StartTime.Second) <= oldStartTime
                                 && new DateTime(p.PlanDate.Value.Year, p.PlanDate.Value.Month, p.PlanDate.Value.Day, p.TimePeriod.EndTime.Hour, p.TimePeriod.EndTime.Minute, p.TimePeriod.EndTime.Second) >= oldEndTime);

                if (oldlogs != null)
                {
                    foreach (DutyCheckLog dcl in oldlogs)
                    {
                        if (dcl.DutyCheckSiteSchedule != null && dcl.DutyCheckSiteSchedule.SiteOrganizationId.Equals(Sentinel.OrganizationId))
                        {
                            oldlog = dcl;
                            break;
                        }
                    }
                }
                //
                DutyDetail dutyDetail = new DutyDetail();
                dutyDetail.Sentinelid = sentinelId;
                dutyDetail.OnDutyStaff = log.DutyCheckSiteSchedule.CheckMan;
                if (oldlog != null)
                {
                    dutyDetail.OffDutyStaff = oldlog.DutyCheckSiteSchedule.CheckMan;
                }
                return dutyDetail;
            }
        }

        public DutyDetail GetDutyDetailBySentinelId(Guid sentinelId,DateTime time)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var Sentinel = db.IPDeviceInfo
                            .FirstOrDefault(p => p.IPDeviceInfoId.Equals(sentinelId));
                if (Sentinel == null)
                {
                    return null;
                }
                //取当前记录
                var logs = db.DutyCheckLog
                                 .Include(t => t.DutyCheckSiteSchedule).ThenInclude(t => t.CheckMan).ThenInclude(t => t.Organization)
                                 .Include(t => t.DutyCheckSiteSchedule).ThenInclude(t => t.CheckMan).ThenInclude(t => t.PositionType)
                                 .Include(t => t.DayPeriod).ThenInclude(t => t.TimePeriods)
                                 .Where(p => p.DutyCheckSiteSchedule != null
                                 && new DateTime(p.PlanDate.Value.Year, p.PlanDate.Value.Month, p.PlanDate.Value.Day, p.TimePeriod.StartTime.Hour, p.TimePeriod.StartTime.Minute, p.TimePeriod.StartTime.Second) <= time
                                 && new DateTime(p.PlanDate.Value.Year, p.PlanDate.Value.Month, p.PlanDate.Value.Day, p.TimePeriod.EndTime.Hour, p.TimePeriod.EndTime.Minute, p.TimePeriod.EndTime.Second) >= time);
                if (logs == null)
                    return null;
                DutyCheckLog log = null;
                foreach (DutyCheckLog dcl in logs) //由于DutyCheckSiteScheduleId也可能为空，暂这样处理
                {
                    if (dcl.DutyCheckSiteSchedule != null && dcl.DutyCheckSiteSchedule.SiteOrganizationId.Equals(Sentinel.OrganizationId))
                    {
                        log = dcl;
                        break;
                    }
                }
                if (log == null)
                    return null;
                //取前一时段的记录
                int day = 0;
                int oldOrderNo = log.TimePeriod.OrderNo - 1;
                if (oldOrderNo <= 0)
                {
                    oldOrderNo = log.DayPeriod.TimePeriods.Max(p => p.OrderNo);
                    day = -1;
                }
                TimePeriod oldTimePeriod = log.DayPeriod.TimePeriods.Find(p => p.OrderNo.Equals(oldOrderNo));
                //
                DateTime oldStartTime = new DateTime(time.AddDays(day).Date.Year, time.AddDays(day).Date.Month, time.AddDays(day).Date.Day, oldTimePeriod.StartTime.Hour, oldTimePeriod.StartTime.Minute, oldTimePeriod.StartTime.Second);
                DateTime oldEndTime = new DateTime(time.AddDays(day).Date.Year, time.AddDays(day).Date.Month, time.AddDays(day).Date.Day, oldTimePeriod.EndTime.Hour, oldTimePeriod.EndTime.Minute, oldTimePeriod.EndTime.Second);
                DutyCheckLog oldlog = null;
                var oldlogs = db.DutyCheckLog
                                 .Include(t => t.DutyCheckSiteSchedule).ThenInclude(t => t.CheckMan).ThenInclude(t => t.Organization)
                                 .Include(t => t.DutyCheckSiteSchedule).ThenInclude(t => t.CheckMan).ThenInclude(t => t.PositionType)
                                 .Include(t => t.DayPeriod).ThenInclude(t => t.TimePeriods)
                                 .Where(p => p.DutyCheckSiteSchedule != null
                                 && new DateTime(p.PlanDate.Value.Year, p.PlanDate.Value.Month, p.PlanDate.Value.Day, p.TimePeriod.StartTime.Hour, p.TimePeriod.StartTime.Minute, p.TimePeriod.StartTime.Second) <= oldStartTime
                                 && new DateTime(p.PlanDate.Value.Year, p.PlanDate.Value.Month, p.PlanDate.Value.Day, p.TimePeriod.EndTime.Hour, p.TimePeriod.EndTime.Minute, p.TimePeriod.EndTime.Second) >= oldEndTime);

                if (oldlogs != null)
                {
                    foreach (DutyCheckLog dcl in oldlogs)
                    {
                        if (dcl.DutyCheckSiteSchedule != null && dcl.DutyCheckSiteSchedule.SiteOrganizationId.Equals(Sentinel.OrganizationId))
                        {
                            oldlog = dcl;
                            break;
                        }
                    }
                }
                //
                DutyDetail dutyDetail = new DutyDetail();
                dutyDetail.Sentinelid = sentinelId;
                dutyDetail.OnDutyStaff = log.DutyCheckSiteSchedule.CheckMan;
                if (oldlog != null)
                {
                    dutyDetail.OffDutyStaff = oldlog.DutyCheckSiteSchedule.CheckMan;
                }
                return dutyDetail;
            }
        }

        public DutyDetail GetDutyDetailByFieldCheck(Guid sentinelId, DateTime time, Guid? staffId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var Sentinel = db.IPDeviceInfo
                            .FirstOrDefault(p => p.IPDeviceInfoId.Equals(sentinelId));
                if (Sentinel == null)
                {
                    return null;
                }
                //取当前记录
                var logs = db.DutyCheckLog
                                 .Include(t => t.DutyCheckSiteSchedule).ThenInclude(t => t.CheckMan).ThenInclude(t => t.Organization)
                                 .Include(t => t.DutyCheckSiteSchedule).ThenInclude(t => t.CheckMan).ThenInclude(t => t.PositionType)
                                 .Include(t => t.DayPeriod).ThenInclude(t => t.TimePeriods)
                                 .Where(p => p.DutyCheckSiteSchedule != null
                                 && new DateTime(p.PlanDate.Value.Year, p.PlanDate.Value.Month, p.PlanDate.Value.Day, p.TimePeriod.StartTime.Hour, p.TimePeriod.StartTime.Minute, p.TimePeriod.StartTime.Second) <= time
                                 && new DateTime(p.PlanDate.Value.Year, p.PlanDate.Value.Month, p.PlanDate.Value.Day, p.TimePeriod.EndTime.Hour, p.TimePeriod.EndTime.Minute, p.TimePeriod.EndTime.Second) >= time);
                if (logs == null)
                    return null;
                DutyCheckLog log = null;
                foreach (DutyCheckLog dcl in logs) //由于DutyCheckSiteScheduleId也可能为空，暂这样处理
                {
                    if (dcl.DutyCheckSiteSchedule != null && dcl.DutyCheckSiteSchedule.SiteOrganizationId.Equals(Sentinel.OrganizationId))
                    {
                        log = dcl;
                        break;
                    }
                }
                if (log == null)
                    return null;
                //取 查哨人员
                Staff checkStaff = null;
                if (staffId != null)
                {
                    checkStaff = db.Staff.FirstOrDefault(p => p.StaffId.Equals(staffId));
                }
                //
                DutyDetail dutyDetail = new DutyDetail();
                dutyDetail.Sentinelid = sentinelId;
                dutyDetail.OnDutyStaff = log.DutyCheckSiteSchedule.CheckMan;
                if (checkStaff != null)
                {
                    dutyDetail.OffDutyStaff = checkStaff;
                }
                return dutyDetail;
            }
        }


        /// <summary>
        /// 检查人员是否正常换岗
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Paps/DutyCheck/fingerPrintNumber={fingerPrintNumber}&sentinelId={sentinelId}")]
        public IActionResult GetCheckDuty(int fingerPrintNumber, Guid sentinelId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var fingerprint = db.Set<Fingerprint>().FirstOrDefault(p => p.FingerprintNo.Equals(fingerPrintNumber));
                    if (fingerprint == null)
                    {
                        return new ObjectResult(new FingerprintOnDuty { FingerprintNumber = fingerPrintNumber, Result = false });
                    }
                    //查找对应人员
                    Staff staff = db.Staff
                                  .FirstOrDefault(p => p.StaffId.Equals(fingerprint.StaffId));
                    if (staff == null)
                    {
                        return new ObjectResult(new FingerprintOnDuty { FingerprintNumber = fingerPrintNumber, Result = false });
                    }
                    //查找哨位台
                    var sentry = db.IPDeviceInfo
                               .FirstOrDefault(p => p.IPDeviceInfoId.Equals(sentinelId));
                    if (sentry == null)
                    {
                        return new ObjectResult(new FingerprintOnDuty { FingerprintNumber = fingerPrintNumber, Result = false });
                    }
                    //查找排班情况
                    var logs = db.DutyCheckLog
                               .Include(t => t.DutyCheckSiteSchedule)
                               .Where(p => p.DutyCheckSiteSchedule!=null
                               //
                               && new DateTime(p.PlanDate.Value.Year, p.PlanDate.Value.Month, p.PlanDate.Value.Day, p.TimePeriod.StartTime.Hour, p.TimePeriod.StartTime.Minute, p.TimePeriod.StartTime.Second) <= DateTime.Now
                               && new DateTime(p.PlanDate.Value.Year, p.PlanDate.Value.Month, p.PlanDate.Value.Day, p.TimePeriod.EndTime.Hour, p.TimePeriod.EndTime.Minute, p.TimePeriod.EndTime.Second) >= DateTime.Now);
                    DutyCheckLog log = null;
                    foreach (DutyCheckLog dcl in logs) //由于DutyCheckSiteScheduleId也可能为空，暂这样处理
                    {
                        if (dcl.DutyCheckSiteSchedule != null && dcl.DutyCheckSiteSchedule.SiteOrganizationId.Equals(sentry.OrganizationId)
                            && dcl.DutyCheckSiteSchedule.CheckManId.Equals(staff.StaffId))
                        {
                            log = dcl;
                            break;
                        }
                    }
                    if (log == null)
                    {
                        return new ObjectResult(new FingerprintOnDuty { FingerprintNumber = fingerPrintNumber, Result = false });
                    }
                    else
                    {
                        return new ObjectResult(new FingerprintOnDuty { FingerprintNumber = fingerPrintNumber, Result = true });
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.LogError("检查人员是否正常换岗：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }


        /// <summary>
        /// 根据组织机构ID获取查勤进度状态
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Paps/DutyCheck/GetDutyCheckPackageProcess/organizationId={organizationId}")]
        public IActionResult GetDutyCheckPackageProcess(Guid organizationId)
        {
            try
            {
                if (organizationId == null || organizationId == new Guid())
                    return NoContent();

                using (var db = new AllInOneContext.AllInOneContext())
                {
                    DutyCheckPackage package = db.DutyCheckPackage
                                .Include(t => t.DutyCheckPackLogs).ThenInclude(t => t.DutyCheckLog).ThenInclude(t => t.Status)
                                .FirstOrDefault(p => p.StartTime <= DateTime.Now && p.EndTime >= DateTime.Now);
                    if (package == null)
                        return NoContent();

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

                    return new ObjectResult(process);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("根据组织机构ID获取查勤进度状态：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }

        }


        /// <summary>
        /// 根据组织机构ID获取下一个查勤数据
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Paps/DutyCheck/getnextdutychecklog/organizationId={organizationId}")]
        public IActionResult GetNextDutyCheckLog(Guid organizationId)
        {
            try
            {
                if (organizationId == null || organizationId == new Guid())
                    return NoContent();
                //调整方法，原来记录生成的只是组织机构，需匹配生成查勤点
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    DutyCheckPackage package = db.DutyCheckPackage
                                .Include(t => t.DutyCheckPackLogs).ThenInclude(t => t.DutyCheckLog).ThenInclude(t => t.Status)
                                .FirstOrDefault(p => p.StartTime <= DateTime.Now && p.EndTime >= DateTime.Now && p.PackageStatusId!=new Guid("124A8562-EAC8-4C09-8758-A6E312974552"));
                    if (package == null)
                        return NoContent();

                    //处理已获取，未完成的记录
                    var getLogs = package.DutyCheckPackLogs.Where(p => p.DutyCheckLog.Status.SystemOptionCode.Equals("16000002"));

                    if (getLogs.Count() > 0)
                    {
                        DutyCheckLog log = db.DutyCheckLog
                                        .Include(t => t.Apprises).ThenInclude(t => t.DutyCheckAppraise).ThenInclude(t => t.AppraiseICO)
                                        .Include(t => t.Apprises).ThenInclude(t => t.DutyCheckAppraise).ThenInclude(t => t.AppraiseType)
                                        .Include(t => t.DutyCheckOperation).ThenInclude(t => t.Attachments).ThenInclude(t => t.Attachment)
                                        .Include(t => t.DutyCheckOperation).ThenInclude(t => t.Attachments).ThenInclude(t => t.AttachmentType)
                                            .Include(t => t.DutyCheckSiteSchedule).ThenInclude(t => t.SiteOrganization)
                                        .Include(t => t.DutyCheckSiteSchedule).ThenInclude(t => t.CheckMan)
                                        .Include(t => t.DutyCheckStaff)
                                        .Include(t => t.Organization)
                                        .Include(t => t.RecordType)
                                        .Include(t => t.Status)
                                        .Include(t => t.CircularTypes).ThenInclude(t => t.Dispose)
                                        .Include(t => t.MainApprise)
                                        .FirstOrDefault(p => p.DutyCheckLogId.Equals(getLogs.ToList()[0].DutyCheckLogId));
                        //
                        CameraView cameraView = GetCameraView(log.DutyCheckSiteSchedule.SiteOrganizationId, db, log);

                        DutyCheckPointView View = new DutyCheckPointView
                        {
                            DutyCheckLog=log,
                            CameraView= cameraView
                        };

                        return new ObjectResult(View);
                    }
                    //从未获取的记录中，返回一个
                    var noLogs = package.DutyCheckPackLogs.Where(p => p.DutyCheckLog.Status.SystemOptionCode.Equals("16000001"));
                    if (noLogs.Count() > 0)
                    {
                        DutyCheckLog log = db.DutyCheckLog
                                            .Include(t => t.Apprises).ThenInclude(t => t.DutyCheckAppraise).ThenInclude(t => t.AppraiseICO)
                                            .Include(t => t.Apprises).ThenInclude(t => t.DutyCheckAppraise).ThenInclude(t => t.AppraiseType)
                                            .Include(t => t.DutyCheckOperation).ThenInclude(t => t.Attachments).ThenInclude(t => t.Attachment)
                                            .Include(t => t.DutyCheckOperation).ThenInclude(t => t.Attachments).ThenInclude(t => t.AttachmentType)
                                            .Include(t => t.DutyCheckSiteSchedule).ThenInclude(t=>t.SiteOrganization)
                                            .Include(t => t.DutyCheckSiteSchedule).ThenInclude(t => t.CheckMan)
                                            .Include(t => t.DutyCheckStaff)
                                            .Include(t => t.Organization)
                                            .Include(t => t.RecordType)
                                            .Include(t => t.Status)
                                            .Include(t => t.CircularTypes).ThenInclude(t => t.Dispose)
                                            .Include(t => t.MainApprise)
                                            .FirstOrDefault(p => p.DutyCheckLogId.Equals(noLogs.ToList()[0].DutyCheckLogId));
                        //
                        CameraView cameraView = GetCameraView(log.DutyCheckSiteSchedule.SiteOrganizationId, db, log);
                        //绑定查勤视频点与查勤记录的关系
                        if (cameraView != null)
                        {
                            log.DutycheckSiteId = cameraView.CameraId;
                            log.DutycheckSiteName = log.DutyCheckSiteSchedule.SiteOrganization.OrganizationShortName+"-"+cameraView.CameraName;
                            SystemOption so = db.SystemOption.FirstOrDefault(p => p.SystemOptionCode.Equals("16000002"));
                            log.StatusId = so.SystemOptionId;
                            log.Status = null;
                            db.DutyCheckLog.Update(log);
                            db.SaveChanges();
                        }
                        //
                        DutyCheckPointView View = new DutyCheckPointView
                        {
                            DutyCheckLog = log,
                            CameraView = cameraView
                        };

                        return new ObjectResult(View);
                    }
                    //当前时段已完成,更新查勤包的状态
                    else
                    {
                        package.PackageStatusId = new Guid("24AC9875-C463-47B6-8147-5845874C3CAF");
                        db.DutyCheckPackage.Update(package);
                        //
                        return NoContent();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("根据组织机构ID获取下一个查勤数据：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }


        /// <summary>
        ///随机获取该组织机构下的一个监控点
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>

        private CameraView GetCameraView(Guid? organizationId, AllInOneContext.AllInOneContext db, DutyCheckLog log)
        {
            CameraView monitorySite = null;
            if (organizationId == null)
                return monitorySite;
            //获取组织机构下所有的监控点View
            Organization org = db.Organization
                .Include(t => t.Center)
                .FirstOrDefault(p => p.OrganizationId.Equals(organizationId));
            if (org == null || org.Center == null || org.Center.EndPoints == null || org.Center.EndPoints.Count == 0)
                return monitorySite;
            //
            List<DutyCheckLog> allDutyCheckLogs = GetAllDutyCheckLog(db, org);
            //判断是否已绑定监控点数据
            if (log.DutycheckSiteId != null)
            {
                string murl = string.Format("http://{0}:{1}/Resources/MonitorySite/cameraId/{2}", org.Center.EndPoints[0].IPAddress, org.Center.EndPoints[0].Port, log.DutycheckSiteId);
                MonitorySite ms = HttpClientHelper.GetOne<MonitorySite>(murl);
                return ms.ToCameraView();
            }
            string url = string.Format("http://{0}:{1}/Resources/MonitorySite", org.Center.EndPoints[0].IPAddress, org.Center.EndPoints[0].Port);
            IEnumerable<MonitorySite> monitorySiteT = HttpClientHelper.Get<MonitorySite>(url);
            //过滤出有效的监控点
            monitorySiteT = monitorySiteT.Where(p => p.IsDutycheckSite);
            //确保所有监控点能覆盖
            CameraView randomCameraView = null;
            if (monitorySiteT == null || monitorySiteT.Count() == 0)
                return randomCameraView;
            foreach (MonitorySite cam in monitorySiteT)
            {
                var competeLog = allDutyCheckLogs.FirstOrDefault(p => p.DutycheckSiteId.Equals(cam.CameraId));
                if (competeLog != null)
                {
                    continue;
                }
                else
                {
                    randomCameraView = cam.ToCameraView();
                    break;
                }
            }
            //
            if (randomCameraView == null)
            {
                Random r = new Random(monitorySiteT.Count());

                randomCameraView = monitorySiteT.ToArray()[r.Next()].ToCameraView();
            }

            return randomCameraView;
        }

        /// <summary>
        /// 获取查勤包的情况（总队为一周，支队/中队为一天）
        /// </summary>
        /// <param name="db"></param>
        /// <param name="org"></param>
        /// <returns></returns>
        private static List<DutyCheckLog> GetAllDutyCheckLog(AllInOneContext.AllInOneContext db, Organization org)
        {
            List<DutyCheckLog> allDutyCheckLogs = new List<DutyCheckLog>();
            if (!org.OrganizationFullName.Contains(".") && org.OrganizationFullName.Contains("总队"))
            {
                DateTime dt = DateTime.Now; //当前时间

                DateTime startWeek = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d"))); //本周周一

                DateTime endWeek = startWeek.AddDays(6); //本周周日

                var packages = db.DutyCheckPackage
                          .Include(t => t.DutyCheckPackLogs).ThenInclude(t => t.DutyCheckLog).ThenInclude(t => t.Status)
                          .Where(p => p.StartTime.Date <= startWeek.Date && p.EndTime.Date >= endWeek.Date);

                foreach (DutyCheckPackage package in packages)
                {
                    foreach (DutyCheckPackageLog plog in package.DutyCheckPackLogs)
                    {
                        allDutyCheckLogs.Add(plog.DutyCheckLog);
                    }
                }
            }
            else
            {
                var packages = db.DutyCheckPackage
                                          .Include(t => t.DutyCheckPackLogs).ThenInclude(t => t.DutyCheckLog).ThenInclude(t => t.Status)
                                          .Where(p => p.StartTime.Date == DateTime.Now.Date && p.EndTime.Date == DateTime.Now.Date);

                foreach (DutyCheckPackage package in packages)
                {
                    foreach (DutyCheckPackageLog plog in package.DutyCheckPackLogs)
                    {
                        allDutyCheckLogs.Add(plog.DutyCheckLog);
                    }
                }
            }

            return allDutyCheckLogs;
        }

        [HttpPost]
        [Route("~/Paps/DutyCheck/SynchronousSensing")]
        public IActionResult PushSynchronousSensing([FromBody]SynchronousSensing model)
        {
            if (model == null)
                return NoContent();
            //
            using (var db = new AllInOneContext.AllInOneContext())
            {
                ////查找监控点所在的组织机构
                //Organization org = db.Organization
                //    .Include(t => t.Center)
                //    .FirstOrDefault(p => p.OrganizationId.Equals(model.MonitorySite.Organization.ParentOrganizationId));
                //if (org == null || org.Center == null || org.Center.EndPoints == null || org.Center.EndPoints.Count == 0)
                //    return NoContent();


                //string url = string.Format("http://{0}:{1}/paps/dutycheck/synchronoussensing/notify", org.Center.EndPoints[0].IPAddress, org.Center.EndPoints[0].Port);

                //bool ret = HttpClientHelper.Post(model, url);

                //return new ObjectResult("Ok");
                //查找受检单位
                Organization checkedOrg = db.Organization.Include(t=>t.Center).FirstOrDefault(t => t.OrganizationId.Equals(model.Organization.OrganizationId));
                Organization checkingOrg = db.Organization.FirstOrDefault(t => t.OrganizationId.Equals(model.CheckOrganization.OrganizationId));

                if (checkedOrg == null)
                {
                    return BadRequest(new ApplicationException() { ErrorCode = "参数错误", ErrorMessage = "受检单位不能为空" });
                }
                if (checkedOrg.Center.EndPoints == null || checkedOrg.Center.EndPoints.Count == 0)
                {
                    return BadRequest(new ApplicationException() { ErrorCode = "参数错误", ErrorMessage = "未配置受检单位应用服务器信息" });
                }
                model.CheckOrganization = checkingOrg;
                string url = string.Format("http://{0}:{1}/Paps/DutyCheck/SynchronousSensing/Notify", checkedOrg.Center.EndPoints[0].IPAddress, checkedOrg.Center.EndPoints[0].Port);
                _logger.LogInformation("同步感知 Begin,Url:{0}", url);
               var ret = HttpClientHelper.Post(model, url);
                _logger.LogInformation("同步感知 End.Result:{0}", ret);
                return new ObjectResult("Ok");
            }
        }

        /// <summary>
        /// 通知感知通知，用于上级向下级发送
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/Paps/DutyCheck/SynchronousSensing/Notify")]
        public IActionResult NotifySynchronousSensing([FromBody]SynchronousSensing model)
        {
            //广播消息
            MQPulish.PublishMessage("SynchronousSensing", model);
            //并转发消息到下级
            return new ObjectResult("Ok");
        }


        /// <summary>
        /// 获取实地查勤记录
        /// </summary>
        /// <param name="data"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Paps/DutyCheck/Record/FieldDutyCheck")]
        public IActionResult QueryFieldDutyCheckView(DateTime data, Guid statusId, int currentPage,
            int pageSize)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {

                //测试数据
                List<FieldDutyCheckLog> list = new List<FieldDutyCheckLog>();
                list.Add(new FieldDutyCheckLog
                {
                    Appriase = "好",
                    AttachmentPath = "",
                    DutyCheckStaffName = "张三",
                    DutyCheckTime = "2017-01-11 10:32:01",
                    OrganizationName = "xx中队",
                    PlanDutyCheckStaffName = "张三",
                    PlanDutyCheckTime = "2017-01-11 08:00-12:00",
                    SentinelName = "一号哨",
                    Status = "正常"
                });
                list.Add(new FieldDutyCheckLog
                {
                    Appriase = "好",
                    AttachmentPath = "",
                    DutyCheckStaffName = "张三",
                    DutyCheckTime = "2017-01-11 11::21",
                    OrganizationName = "xx中队",
                    PlanDutyCheckStaffName = "张三",
                    PlanDutyCheckTime = "2017-01-11 08:00-12:00",
                    SentinelName = "二号哨",
                    Status = "正常"
                });
                list.Add(new FieldDutyCheckLog
                {
                    Appriase = "好",
                    AttachmentPath = "",
                    DutyCheckStaffName = "张三",
                    DutyCheckTime = "2017-01-11 11::42",
                    OrganizationName = "xx中队",
                    PlanDutyCheckStaffName = "张三",
                    PlanDutyCheckTime = "2017-01-11 08:00-12:00",
                    SentinelName = "三号哨",
                    Status = "正常"
                });
                //
                list.Add(new FieldDutyCheckLog
                {
                    Appriase = "好",
                    AttachmentPath = "",
                    DutyCheckStaffName = "李四",
                    DutyCheckTime = "2017-01-11 12:12",
                    OrganizationName = "xx中队",
                    PlanDutyCheckStaffName = "李四",
                    PlanDutyCheckTime = "2017-01-11 12:00-18:00",
                    SentinelName = "一号哨",
                    Status = "正常"
                });
                list.Add(new FieldDutyCheckLog
                {
                    Appriase = "好",
                    AttachmentPath = "",
                    DutyCheckStaffName = "李四",
                    DutyCheckTime = "2017-01-11 14:05",
                    OrganizationName = "xx中队",
                    PlanDutyCheckStaffName = "李四",
                    PlanDutyCheckTime = "2017-01-11 12:00-18:00",
                    SentinelName = "二号哨",
                    Status = "正常"
                });
                list.Add(new FieldDutyCheckLog
                {
                    Appriase = "好",
                    AttachmentPath = "",
                    DutyCheckStaffName = "李四",
                    DutyCheckTime = "2017-01-11 17:35",
                    OrganizationName = "xx中队",
                    PlanDutyCheckStaffName = "李四",
                    PlanDutyCheckTime = "2017-01-11 12:00-18:00",
                    SentinelName = "三号哨",
                    Status = "正常"
                });
                //
                list.Add(new FieldDutyCheckLog
                {
                    Appriase = "中",
                    AttachmentPath = "",
                    DutyCheckStaffName = "王五",
                    DutyCheckTime = "2017-01-11 19:55",
                    OrganizationName = "xx中队",
                    PlanDutyCheckStaffName = "王五",
                    PlanDutyCheckTime = "2017-01-11 18:00-00:00",
                    SentinelName = "一号哨",
                    Status = "正常"
                });
                list.Add(new FieldDutyCheckLog
                {
                    Appriase = "好",
                    AttachmentPath = "",
                    DutyCheckStaffName = "王五",
                    DutyCheckTime = "2017-01-11 21:55",
                    OrganizationName = "xx中队",
                    PlanDutyCheckStaffName = "王五",
                    PlanDutyCheckTime = "2017-01-11 18:00-00:00",
                    SentinelName = "二号哨",
                    Status = "正常"
                });
                list.Add(new FieldDutyCheckLog
                {
                    Appriase = "好",
                    AttachmentPath = "",
                    DutyCheckStaffName = "王五",
                    DutyCheckTime = "2017-01-12 1:55",
                    OrganizationName = "xx中队",
                    PlanDutyCheckStaffName = "王五",
                    PlanDutyCheckTime = "2017-01-11 18:00-00:00",
                    SentinelName = "三号哨",
                    Status = "异常"
                });

                QueryPagingRecord queryPagingRecord = new QueryPagingRecord
                {
                    SumRecordCount=9,
                    Record= list
                };

                return new ObjectResult(queryPagingRecord);
            }
        }
    }
}
