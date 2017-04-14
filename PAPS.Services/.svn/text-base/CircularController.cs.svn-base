using AllInOneContext;
using HttpClientEx;
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
    /// 通报控制类
    /// </summary>
    public class CircularController : Controller
    {

        /// <summary>
        /// 根据通报ID获取通报
        /// </summary>
        /// <param name="id">通报ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    Circular data = db.Circular
                                .Include(t => t.CircularStaff).ThenInclude(t => t.Photo)
                                .Include(t => t.CircularStaff).ThenInclude(t => t.PositionType)
                                .Include(t => t.CircularStaff).ThenInclude(t => t.RankType)
                                .Include(t => t.CircularStaff).ThenInclude(t => t.Sex)
                                .Include(t => t.DutyCheckLog).ThenInclude(t => t.Apprises).ThenInclude(t => t.DutyCheckAppraise).ThenInclude(t => t.AppraiseType)
                                .Include(t => t.DutyCheckLog).ThenInclude(t => t.Apprises).ThenInclude(t => t.DutyCheckAppraise).ThenInclude(t => t.AppraiseICO)
                                .Include(t => t.DutyCheckLog).ThenInclude(t => t.Apprises).ThenInclude(t => t.DutyCheckAppraise).ThenInclude(t => t.AppraiseType)
                                .Include(t => t.DutyCheckLog).ThenInclude(t => t.DutyCheckOperation).ThenInclude(t => t.Attachments).ThenInclude(t => t.Attachment)
                                .Include(t => t.DutyCheckLog).ThenInclude(t => t.DutyCheckOperation).ThenInclude(t => t.Attachments).ThenInclude(t => t.AttachmentType)
                                .Include(t => t.DutyCheckLog).ThenInclude(t => t.DutyCheckSiteSchedule)/*.ThenInclude(t => t.CheckDutySite).ThenInclude(t => t.Organization)*/
                                .Include(t => t.DutyCheckLog).ThenInclude(t => t.DutyCheckSiteSchedule).ThenInclude(t => t.CheckMan)
                                .Include(t => t.DutyCheckLog).ThenInclude(t => t.DutyCheckStaff)
                                .Include(t => t.DutyCheckLog).ThenInclude(t => t.Organization)
                                .Include(t => t.DutyCheckLog).ThenInclude(t => t.RecordType)
                                .Include(t => t.DutyCheckLog).ThenInclude(t => t.Status)
                                .Include(t => t.DutyCheckLog).ThenInclude(t => t.CircularTypes).ThenInclude(t => t.Dispose)
                                .Include(t => t.DutyCheckLog).ThenInclude(t => t.MainApprise)
                                .FirstOrDefault(p => p.CircularId.Equals(id));
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
        /// 新增通报
        /// </summary>
        /// <param name="model">通报实体</param>
        /// <returns>返回值</returns>
        [HttpPost]
        public IActionResult Add([FromBody]Circular model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.Circular.Add(model);
                    db.SaveChanges();
                    // 下发通报至下级
                    PushCircular(model);
                    //
                    return Created("", "OK");
                }
            }
            catch (DbUpdateException dbEx)
            {
                return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        ///// <summary>
        ///// 修改通报
        ///// </summary>
        ///// <param name="model">通报实体</param>
        ///// <returns>返回值</returns>
        //[HttpPut]
        //public IActionResult Update([FromBody]Circular model)
        //{
        //    try
        //    {
        //        if (model == null)
        //        {
        //            return BadRequest();
        //        }
        //        using (var db = new AllInOneContext.AllInOneContext())
        //        {
        //            db.Circular.Update(model);
        //            db.SaveChanges();
        //            return new NoContentResult();
        //        }
        //    }
        //    catch (DbUpdateException dbEx)
        //    {
        //        return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
        //    }
        //}

        ///// <summary>
        /////  根据通报ID删除通报
        ///// </summary>
        ///// <param name="id">通报ID</param>
        ///// <returns>返回值</returns>
        //[HttpDelete("{id}")]
        //public IActionResult Delete(Guid id)
        //{
        //    try
        //    {
        //        using (var db = new AllInOneContext.AllInOneContext())
        //        {
        //            Circular data = db.Circular.FirstOrDefault(p => p.CircularId == id);
        //            if (data == null)
        //            {
        //                return NoContent();
        //            }
        //            db.Circular.Remove(data);
        //            db.SaveChanges();
        //            return new NoContentResult();
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
        //    }
        //}


        /// <summary>
        /// 根据参数获取通报记录
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="currentPage">当前页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="circularStaffId">通报人id</param>
        /// <param name="mainAppriseId">总体评价id</param>
        /// <param name="monitorySiteId">监控点id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Paps/Circular/Query")]
        public IActionResult GetCircularByParameter(DateTime startTime, DateTime endTime, int currentPage,
            int pageSize, Guid?[] circularStaffId, Guid? mainAppriseId, Guid?[] monitorySiteId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var Query = from p in db.Circular
                                .Include(t => t.CircularStaff).ThenInclude(t => t.Photo)
                                .Include(t => t.CircularStaff).ThenInclude(t => t.PositionType)
                                .Include(t => t.CircularStaff).ThenInclude(t => t.RankType)
                                .Include(t => t.CircularStaff).ThenInclude(t => t.Sex)
                                .Include(t => t.DutyCheckLog).ThenInclude(t => t.Apprises).ThenInclude(t => t.DutyCheckAppraise).ThenInclude(t => t.AppraiseType)
                                .Include(t => t.DutyCheckLog).ThenInclude(t => t.Apprises).ThenInclude(t => t.DutyCheckAppraise).ThenInclude(t => t.AppraiseICO)
                                .Include(t => t.DutyCheckLog).ThenInclude(t => t.Apprises).ThenInclude(t => t.DutyCheckAppraise).ThenInclude(t => t.AppraiseType)
                                .Include(t => t.DutyCheckLog).ThenInclude(t => t.DutyCheckOperation).ThenInclude(t => t.Attachments).ThenInclude(t => t.Attachment)
                                .Include(t => t.DutyCheckLog).ThenInclude(t => t.DutyCheckOperation).ThenInclude(t => t.Attachments).ThenInclude(t => t.AttachmentType)
                                .Include(t => t.DutyCheckLog).ThenInclude(t => t.DutyCheckSiteSchedule)/*.ThenInclude(t => t.CheckDutySite).ThenInclude(t => t.Organization)*/
                                .Include(t => t.DutyCheckLog).ThenInclude(t => t.DutyCheckSiteSchedule).ThenInclude(t => t.CheckMan)
                                .Include(t => t.DutyCheckLog).ThenInclude(t => t.DutyCheckStaff)
                                .Include(t => t.DutyCheckLog).ThenInclude(t => t.Organization)
                                .Include(t => t.DutyCheckLog).ThenInclude(t => t.RecordType)
                                .Include(t => t.DutyCheckLog).ThenInclude(t => t.Status)
                                .Include(t => t.DutyCheckLog).ThenInclude(t => t.CircularTypes).ThenInclude(t => t.Dispose)
                                .Include(t => t.DutyCheckLog).ThenInclude(t => t.MainApprise)
                                orderby p.CircularTime descending
                                where p.CircularTime >= startTime && p.CircularTime <= endTime
                                && ((circularStaffId == null || circularStaffId.Length == 0) || circularStaffId.Contains(p.CircularStaff.StaffId))
                                && ((mainAppriseId == null) || p.DutyCheckLog.MainAppriseId.Equals(mainAppriseId))
                                && ((monitorySiteId == null || monitorySiteId.Length == 0) 
                                || monitorySiteId.Contains(p.DutyCheckLog.DutyCheckSiteSchedule.CheckDutySiteId)
                                /*|| monitorySiteId.Contains(p.DutyCheckLog.DutyCheckSiteSchedule.CheckDutySite.MonitorySiteId)*/)
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
        /// 广播通告
        /// </summary>
        /// <param name="Circular"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/Paps/Circular/Broadcast")]
        public IActionResult BroadcastCircular([FromBody]Circular Circular)
        {
            if (Circular == null)
                return BadRequest("Circular object can not be null!");

            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    MQPulish.PublishMessage("Circular", Circular);
                    return CreatedAtAction("", Circular);
                }
                catch (Exception ex)
                {
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
        }

        /// <summary>
        /// 推送反馈至下级
        /// </summary>
        /// <param name="Circular"></param>
        public bool PushCircular(Circular model)
        {
            if (model == null)
                return false;
            using (var db = new AllInOneContext.AllInOneContext())
            {
                Circular Circular = db.Circular
                                    .Include(t => t.CircularStaff).ThenInclude(t => t.Photo)
                                    .Include(t => t.CircularStaff).ThenInclude(t => t.PositionType)
                                    .Include(t => t.CircularStaff).ThenInclude(t => t.RankType)
                                    .Include(t => t.CircularStaff).ThenInclude(t => t.Sex)
                                    .Include(t => t.DutyCheckLog).ThenInclude(t => t.Apprises).ThenInclude(t => t.DutyCheckAppraise).ThenInclude(t => t.AppraiseType)
                                    .Include(t => t.DutyCheckLog).ThenInclude(t => t.Apprises).ThenInclude(t => t.DutyCheckAppraise).ThenInclude(t => t.AppraiseICO)
                                    .Include(t => t.DutyCheckLog).ThenInclude(t => t.Apprises).ThenInclude(t => t.DutyCheckAppraise).ThenInclude(t => t.AppraiseType)
                                    .Include(t => t.DutyCheckLog).ThenInclude(t => t.DutyCheckOperation).ThenInclude(t => t.Attachments).ThenInclude(t => t.Attachment)
                                    .Include(t => t.DutyCheckLog).ThenInclude(t => t.DutyCheckOperation).ThenInclude(t => t.Attachments).ThenInclude(t => t.AttachmentType)
                                    .Include(t => t.DutyCheckLog).ThenInclude(t => t.DutyCheckSiteSchedule)/*.ThenInclude(t => t.CheckDutySite).ThenInclude(t=>t.Organization)*/
                                    .Include(t => t.DutyCheckLog).ThenInclude(t => t.DutyCheckSiteSchedule).ThenInclude(t => t.CheckMan)
                                    .Include(t => t.DutyCheckLog).ThenInclude(t => t.DutyCheckStaff)
                                    .Include(t => t.DutyCheckLog).ThenInclude(t => t.Organization)
                                    .Include(t => t.DutyCheckLog).ThenInclude(t => t.RecordType)
                                    .Include(t => t.DutyCheckLog).ThenInclude(t => t.Status)
                                    .Include(t => t.DutyCheckLog).ThenInclude(t => t.CircularTypes).ThenInclude(t => t.Dispose)
                                    .Include(t => t.DutyCheckLog).ThenInclude(t => t.MainApprise)
                                    .FirstOrDefault(p => p.CircularId.Equals(model.CircularId));
                if (Circular == null)
                    return false;
                //查找监控点所在的组织机构
                Organization org = db.Organization
                    .Include(t => t.Center)
                    .FirstOrDefault(p => p.OrganizationId.Equals(Circular.DutyCheckLog.DutyCheckSiteSchedule.CheckDutySite.Organization.ParentOrganizationId));
                if (org == null || org.Center == null || org.Center.EndPoints == null || org.Center.EndPoints.Count == 0)
                    return false;


                string url = string.Format("http://{0}:{1}/Paps/Circular/Broadcast", org.Center.EndPoints[0].IPAddress, org.Center.EndPoints[0].Port);

                var ret = HttpClientHelper.Post(Circular, url);

                return ret.Success;
            }

        }
    }
}
