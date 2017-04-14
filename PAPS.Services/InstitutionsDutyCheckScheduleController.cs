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
    /// 查勤检查安排控制类
    /// </summary>
    public class InstitutionsDutyCheckScheduleController : Controller
    {



        /// <summary>
        /// 根据查勤检查安排ID获取查勤检查安排
        /// </summary>
        /// <param name="id">查勤检查安排ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    InstitutionsDutyCheckSchedule data = db.InstitutionsDutyCheckSchedule
                                                        //.Include(t => t.Entourages)
                                                        .Include(t=>t.InspectedOrganization)
                                                        //.Include(t=>t.InspectionTarget)
                                                        .Include(t=>t.Lead)
                                                        .FirstOrDefault(p => p.InstitutionsDutyCheckScheduleId.Equals(id));
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
        /// 新增查勤检查安排
        /// </summary>
        /// <param name="model">查勤检查安排实体</param>
        /// <returns>返回值</returns>
        [HttpPost]
        public IActionResult Add([FromBody]InstitutionsDutyCheckSchedule model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.InstitutionsDutyCheckSchedule.Add(model);
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
        /// 修改查勤检查安排
        /// </summary>
        /// <param name="model">查勤检查安排实体</param>
        /// <returns>返回值</returns>
        [HttpPut]
        public IActionResult Update([FromBody]InstitutionsDutyCheckSchedule model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.InstitutionsDutyCheckSchedule.Update(model);
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
        ///  根据查勤检查安排ID删除查勤检查安排
        /// </summary>
        /// <param name="id">查勤检查安排ID</param>
        /// <returns>返回值</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    InstitutionsDutyCheckSchedule data = db.InstitutionsDutyCheckSchedule.FirstOrDefault(p => p.InstitutionsDutyCheckScheduleId == id);
                    if (data == null)
                    {
                        return NoContent();
                    }
                    db.InstitutionsDutyCheckSchedule.Remove(data);
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
        /// 根据参数获取查勤检查安排记录
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="currentPage">当前页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="inspectedOrganizationId">受检组织ID</param>
        /// <param name="leadId">负责人ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Paps/InstitutionsDutyCheckSchedule/Query")]
        public IActionResult GetInstitutionsDutyCheckScheduleByParameter(DateTime startTime, DateTime endTime, int currentPage,
            int pageSize, Guid? inspectedOrganizationId, Guid? leadId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var Query = from p in db.InstitutionsDutyCheckSchedule
                                //.Include(t => t.Entourages)
                                .Include(t => t.InspectedOrganization)
                                //.Include(t => t.InspectionTarget)
                                .Include(t => t.Lead)
                                orderby p.StartTime descending
                                where p.StartTime >= startTime && p.EndTime <= endTime
                                && ((inspectedOrganizationId == null) || p.InspectedOrganization.OrganizationId.Equals(inspectedOrganizationId))
                                && ((leadId == null) ||  p.Lead.StaffId.Equals(leadId))
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
