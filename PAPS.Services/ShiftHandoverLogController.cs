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
    /// 交班记录控制类
    /// </summary>
    public class ShiftHandoverLogController : Controller
    {


        /// <summary>
        /// 根据交班记录ID获取交班记录
        /// </summary>
        /// <param name="id">交班记录ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    ShiftHandoverLog data = db.ShiftHandoverLog
                                            .Include(t=>t.OffGoing)
                                            .Include(t=>t.OnComing)
                                            .Include(t=>t.Organization)
                                            .Include(t=>t.Status)
                                            .FirstOrDefault(p => p.ShiftHandoverLogId.Equals(id));
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
        /// 新增交班记录
        /// </summary>
        /// <param name="model">交班记录实体</param>
        /// <returns>返回值</returns>
        [HttpPost]
        public IActionResult Add([FromBody]ShiftHandoverLog model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.ShiftHandoverLog.Add(model);
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
        /// 修改交班记录
        /// </summary>
        /// <param name="model">交班记录实体</param>
        /// <returns>返回值</returns>
        [HttpPut]
        public IActionResult Update([FromBody]ShiftHandoverLog model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.ShiftHandoverLog.Update(model);
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
        ///  根据交班记录ID删除交班记录
        /// </summary>
        /// <param name="id">交班记录ID</param>
        /// <returns>返回值</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    ShiftHandoverLog data = db.ShiftHandoverLog.FirstOrDefault(p => p.ShiftHandoverLogId == id);
                    if (data == null)
                    {
                        return NoContent();
                    }
                    db.ShiftHandoverLog.Remove(data);
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
        /// 根据参数获取交班记录记录
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="currentPage">当前页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="organizationId">组织机构id</param>
        /// <param name="statusId">状态id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Paps/ShiftHandoverLog/Query")]
        public IActionResult GetShiftHandoverLogByParameter(DateTime startTime, DateTime endTime, int currentPage,
            int pageSize, Guid organizationId, Guid? statusId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var query = from p in db.ShiftHandoverLog
                                .Include(t => t.OffGoing)
                                .Include(t => t.OnComing)
                                .Include(t => t.Organization)
                                .Include(t => t.Status)
                                orderby p.HandoverDate descending
                                where p.HandoverDate >= startTime && p.HandoverDate <= endTime
                                && p.Organization.OrganizationId.Equals(organizationId)
                                && ((statusId==null) || p.Status.SystemOptionId.Equals(statusId))
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

    }
}
