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
    /// 反馈控制类
    /// </summary>
    public class FeedbackController : Controller
    {


        /// <summary>
        /// 根据反馈ID获取反馈
        /// </summary>
        /// <param name="id">反馈ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    Feedback data = db.Feedback
                                    .Include(t => t.Circular)
                                    .Include(t=>t.FeedbackOptions)
                                    .Include(t=>t.FeedbackStaff)
                                    .Include(t=>t.Fault)
                                    .FirstOrDefault(p => p.FeedbackId.Equals(id));
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
        /// 新增反馈
        /// </summary>
        /// <param name="model">反馈实体</param>
        /// <returns>返回值</returns>
        [HttpPost]
        public IActionResult Add([FromBody]Feedback model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.Feedback.Add(model);
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
        /// 修改反馈
        /// </summary>
        /// <param name="model">反馈实体</param>
        /// <returns>返回值</returns>
        [HttpPut]
        public IActionResult UpdateFeedback([FromBody]Feedback model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.Feedback.Update(model);
                    db.SaveChanges();
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
        ///  根据反馈ID删除反馈
        /// </summary>
        /// <param name="id">反馈ID</param>
        /// <returns>返回值</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    Feedback data = db.Feedback.FirstOrDefault(p => p.FeedbackId == id);
                    if (data == null)
                    {
                        return NoContent();
                    }
                    db.Feedback.Remove(data);
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
        /// 根据参数获取反馈记录
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="currentPage">当前页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="circularId">通报id</param>
        /// <param name="feedbackStaffId">反馈人员id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Paps/Feedback/Query")]
        public IActionResult GetFeedbackByParameter(DateTime startTime, DateTime endTime, int currentPage,
            int pageSize, Guid?[] circularId, Guid?[] feedbackStaffId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var Query = from p in db.Feedback
                                .Include(t => t.Circular)
                                .Include(t => t.FeedbackOptions)
                                .Include(t => t.FeedbackStaff)
                                .Include(t => t.Fault)
                                orderby p.FeedbackTime descending
                                where p.FeedbackTime >= startTime && p.FeedbackTime <= endTime
                                && ((circularId == null || circularId.Length == 0) || circularId.Contains(p.Circular.CircularId))
                                && ((feedbackStaffId == null || feedbackStaffId.Length == 0) || feedbackStaffId.Contains(p.FeedbackStaff.StaffId))
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
        /// 广播反馈信息
        /// </summary>
        /// <param name="fault"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/Paps/Feedback/Broadcast")]
        public IActionResult BroadcastFeedback([FromBody]Feedback Feedback)
        {
            if (Feedback == null)
                return BadRequest("Feedback object can not be null!");

            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    MQPulish.PublishMessage("Feedback", Feedback);
                    return CreatedAtAction("", Feedback);
                }
                catch (Exception ex)
                {
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
        }



        /// <summary>
        /// 推送反馈信息至上级
        /// </summary>
        /// <param name="Feedback"></param>
        public bool PushFeedback(Fault model)
        {
            if (model == null)
                return false;
            using (var db = new AllInOneContext.AllInOneContext())
            {
                //查找监控点所在的组织机构
                Organization org = db.Organization
                    .Include(t => t.Center)
                    .FirstOrDefault(p => p.OrganizationId.Equals(model.CheckDutySite.Organization.ParentOrganizationId));
                if (org == null || org.Center == null || org.Center.EndPoints == null || org.Center.EndPoints.Count == 0)
                    return false;


                string url = string.Format("http://{0}:{1}/Paps/Feedback/Broadcast", org.Center.EndPoints[0].IPAddress, org.Center.EndPoints[0].Port);

                var ret = HttpClientHelper.Post(model, url);

                return ret.Success;            }

        }

    }
}
