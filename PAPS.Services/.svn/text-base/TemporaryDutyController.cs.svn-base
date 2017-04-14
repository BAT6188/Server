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
    /// 临时勤务控制类
    /// </summary>
    public class TemporaryDutyController : Controller
    {



        /// <summary>
        /// 根据临时勤务ID获取临时勤务
        /// </summary>
        /// <param name="id">临时勤务ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    TemporaryDuty data = db.TemporaryDuty
                                        .Include(t=>t.Commander)
                                        .Include(t=>t.DutyProgrammePicture)
                                        .Include(t=>t.DutyType)
                                        .Include(t=>t.Equipments)
                                        .Include(t=>t.Organization)
                                        .Include(t=>t.VehicleType)
                                        .FirstOrDefault(p => p.TemporaryDutyId.Equals(id));
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
        /// 新增临时勤务
        /// </summary>
        /// <param name="model">临时勤务实体</param>
        /// <returns>返回值</returns>
        [HttpPost]
        public IActionResult Add([FromBody]TemporaryDuty model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.TemporaryDuty.Add(model);
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
        /// 修改临时勤务
        /// </summary>
        /// <param name="model">临时勤务实体</param>
        /// <returns>返回值</returns>
        [HttpPut]
        public IActionResult Update([FromBody]TemporaryDuty model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.TemporaryDuty.Update(model);
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
        ///  根据临时勤务ID删除临时勤务
        /// </summary>
        /// <param name="id">临时勤务ID</param>
        /// <returns>返回值</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    TemporaryDuty data = db.TemporaryDuty.FirstOrDefault(p => p.TemporaryDutyId == id);
                    if (data == null)
                    {
                        return NoContent();
                    }
                    db.TemporaryDuty.Remove(data);
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
        /// 根据参数获取临时勤务记录
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="currentPage">当前页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="organizationId">组织机构ID</param>
        /// <param name="taskName">任务名称</param>
        /// <param name="dutyTypeId">勤务类型ID</param>
        /// <param name="vehicleTypeId">交通工具ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Paps/TemporaryDuty/Query")]
        public IActionResult GetTemporaryDutyByParameter(DateTime startTime, DateTime endTime, int currentPage,
            int pageSize, string organizationId, string taskName, Guid? dutyTypeId, Guid? vehicleTypeId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var Query = from p in db.TemporaryDuty
                                .Include(t => t.Commander)
                                .Include(t => t.DutyProgrammePicture)
                                .Include(t => t.DutyType)
                                .Include(t => t.Equipments)
                                .Include(t => t.Organization)
                                .Include(t => t.VehicleType)
                                orderby p.StartTime descending
                                where p.StartTime >= startTime && p.StartTime <= endTime
                                && (organizationId != null && p.Organization.OrganizationId.Equals(organizationId))
                                && (taskName != null && p.TaskName.Equals(taskName))
                                && ((dutyTypeId ==null) ||p.DutyType.SystemOptionId.Equals(dutyTypeId))
                                && ((vehicleTypeId == null) || p.VehicleTypeId.Equals(vehicleTypeId))
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
