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
    /// 值班日报控制类
    /// </summary>
    public class DailyOnDutyController : Controller
    {

        /// <summary>
        /// 根据值班日报ID获取值班日报
        /// </summary>
        /// <param name="id">值班日报ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                DailyOnDuty data = db.DailyOnDuty
                    .Include(t=>t.DutyOfficerToday).Include(t=>t.Organization)
                    .Include(t=>t.Status).Include(t=>t.TomorrowAttendant)
                    .FirstOrDefault(p=>p.DailyOnDutyId.Equals(id));
                if (data == null)
                {
                    return NoContent();
                }

                return new ObjectResult(data);
            }
        }


        /// <summary>
        /// 新增值班日报
        /// </summary>
        /// <param name="model">值班日报实体</param>
        /// <returns>返回值</returns>
        [HttpPost]
        public IActionResult Add([FromBody]DailyOnDuty model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.DailyOnDuty.Add(model);
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
        /// 修改值班日报
        /// </summary>
        /// <param name="model">值班日报实体</param>
        /// <returns>返回值</returns>
        [HttpPut]
        public IActionResult Update([FromBody]DailyOnDuty model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.DailyOnDuty.Update(model);
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
        ///  根据值班日报ID删除值班日报
        /// </summary>
        /// <param name="id">值班日报ID</param>
        /// <returns>返回值</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    DailyOnDuty data = db.DailyOnDuty.FirstOrDefault(p => p.DailyOnDutyId.Equals(id));
                    if (data == null)
                    {
                        return NoContent();
                    }
                    db.DailyOnDuty.Remove(data);
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
        /// 根据参数获取值班日报记录
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="currentPage">当前页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="organizationId">组织机构ID</param>
        /// <param name="statusId">状态ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Paps/DailyonDuty/Query")]
        public IActionResult GetDailyOnDutyByParameter(DateTime startTime, DateTime endTime, int currentPage,
            int pageSize, Guid? organizationId,Guid? statusId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var Query = from p in db.DailyOnDuty.Include(t => t.DutyOfficerToday).Include(t => t.Organization)
                    .Include(t => t.Status).Include(t => t.TomorrowAttendant)
                                orderby p.DutyDate descending
                                where p.DutyDate >= startTime && p.DutyDate <= endTime
                                && ((organizationId == null) || p.Organization.OrganizationId.Equals(organizationId))
                                && ((statusId==null) || p.Status.SystemOptionId.Equals(statusId))
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
    }
}
