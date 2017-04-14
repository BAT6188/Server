using AllInOneContext;
using Infrastructure.Model;
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
    /// 查勤包控制类
    /// </summary>
    public class DutyCheckPackageController : Controller
    {


        /// <summary>
        /// 根据查勤包ID获取查勤包
        /// </summary>
        /// <param name="id">查勤包ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetDutyCheckPackageByID(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                DutyCheckPackage data = db.DutyCheckPackage
                                        .Include(t => t.DutyCheckPackLogs).ThenInclude(t=>t.DutyCheckLog).ThenInclude(t=>t.Apprises)
                                        .Include(t => t.DutyCheckPackLogs).ThenInclude(t => t.DutyCheckLog).ThenInclude(t => t.DutyCheckOperation)
                                        .Include(t => t.DutyCheckPackLogs).ThenInclude(t => t.DutyCheckLog).ThenInclude(t => t.DutyCheckSiteSchedule)/*.ThenInclude(t=>t.CheckDutySite)*/
                                        .Include(t => t.DutyCheckPackLogs).ThenInclude(t => t.DutyCheckLog).ThenInclude(t => t.DutyCheckSiteSchedule).ThenInclude(t => t.CheckMan)
                                        .Include(t => t.DutyCheckPackLogs).ThenInclude(t => t.DutyCheckLog).ThenInclude(t => t.DutyCheckStaff)
                                        .Include(t => t.DutyCheckPackLogs).ThenInclude(t => t.DutyCheckLog).ThenInclude(t=>t.DayPeriod)
                                        .Include(t => t.Organization)
                                        .Include(t=>t.PackageStatus)
                                        .FirstOrDefault(p => p.DutyCheckPackageId.Equals(id));
                if (data == null)
                {
                    return NoContent();
                }

                return new ObjectResult(data);
            }
        }


        ///// <summary>
        ///// 新增查勤包
        ///// </summary>
        ///// <param name="model">查勤包实体</param>
        ///// <returns>返回值</returns>
        //[HttpPost]
        //public IActionResult Add([FromBody]DutyCheckPackage model)
        //{
        //    try
        //    {
        //        if (model == null)
        //        {
        //            return BadRequest();
        //        }
        //        using (var db = new AllInOneContext.AllInOneContext())
        //        {
        //            db.DutyCheckPackage.Add(model);
        //            db.SaveChanges();
        //            return Created("", "OK");
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
        ///// 修改查勤包
        ///// </summary>
        ///// <param name="model">查勤包实体</param>
        ///// <returns>返回值</returns>
        //[HttpPut]
        //public IActionResult Update([FromBody]DutyCheckPackage model)
        //{
        //    try
        //    {
        //        if (model == null)
        //        {
        //            return BadRequest();
        //        }
        //        using (var db = new AllInOneContext.AllInOneContext())
        //        {
        //            using (var tran = db.Database.BeginTransaction())
        //            {
        //                db.DutyCheckPackage.Update(model);
        //                db.SaveChanges();
        //                return new NoContentResult();
        //            }
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
        /////  根据查勤包ID删除查勤包
        ///// </summary>
        ///// <param name="id">查勤包ID</param>
        ///// <returns>返回值</returns>
        //[HttpDelete("{id}")]
        //public IActionResult Delete(Guid id)
        //{
        //    try
        //    {
        //        using (var db = new AllInOneContext.AllInOneContext())
        //        {
        //            DutyCheckPackage data = db.DutyCheckPackage.FirstOrDefault(p => p.DutyCheckPackageId.Equals(id));
        //            if (data == null)
        //            {
        //                return NoContent();
        //            }
        //            db.DutyCheckPackage.Remove(data);
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
        /// 获取查勤包中的一个数据
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Paps/dutyCheckPackage/organizationId={organizationId}")]
        public IActionResult GetNextPackage(Guid organizationId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var getPackages = db.DutyCheckPackage
                                        .Include(t => t.DutyCheckPackLogs).ThenInclude(t => t.DutyCheckLog).ThenInclude(t => t.Apprises)
                                        .Include(t => t.DutyCheckPackLogs).ThenInclude(t => t.DutyCheckLog).ThenInclude(t => t.DutyCheckOperation)
                                        .Include(t => t.DutyCheckPackLogs).ThenInclude(t => t.DutyCheckLog).ThenInclude(t => t.DutyCheckSiteSchedule)/*.ThenInclude(t => t.CheckDutySite)*/
                                        .Include(t => t.DutyCheckPackLogs).ThenInclude(t => t.DutyCheckLog).ThenInclude(t => t.DutyCheckSiteSchedule).ThenInclude(t => t.CheckMan)
                                        .Include(t => t.DutyCheckPackLogs).ThenInclude(t => t.DutyCheckLog).ThenInclude(t => t.DutyCheckStaff)
                                        .Include(t => t.DutyCheckPackLogs).ThenInclude(t => t.DutyCheckLog).ThenInclude(t => t.DayPeriod)
                                        .Include(t => t.Organization)
                                        .Include(t => t.PackageStatus)
                                        .Where(p => p.StartTime<=DateTime.Now && p.EndTime>=DateTime.Now
                                         && p.Organization.OrganizationId.Equals(organizationId)
                                         && p.PackageStatusId.Equals(new Guid("E07A7F37-031A-4834-BAE3-8276E162DA51")) //已获取
                                         );

                if (getPackages != null)
                {
                    return new ObjectResult(getPackages);
                }
                //
                var noGetPackages= db.DutyCheckPackage
                                        .Include(t => t.DutyCheckPackLogs).ThenInclude(t => t.DutyCheckLog).ThenInclude(t => t.Apprises)
                                        .Include(t => t.DutyCheckPackLogs).ThenInclude(t => t.DutyCheckLog).ThenInclude(t => t.DutyCheckOperation)
                                        .Include(t => t.DutyCheckPackLogs).ThenInclude(t => t.DutyCheckLog).ThenInclude(t => t.DutyCheckSiteSchedule)/*.ThenInclude(t => t.CheckDutySite)*/
                                        .Include(t => t.DutyCheckPackLogs).ThenInclude(t => t.DutyCheckLog).ThenInclude(t => t.DutyCheckSiteSchedule).ThenInclude(t => t.CheckMan)
                                        .Include(t => t.DutyCheckPackLogs).ThenInclude(t => t.DutyCheckLog).ThenInclude(t => t.DutyCheckStaff)
                                        .Include(t => t.DutyCheckPackLogs).ThenInclude(t => t.DutyCheckLog).ThenInclude(t => t.DayPeriod)
                                        .Include(t => t.Organization)
                                        .Include(t => t.PackageStatus)
                                        .Where(p => p.StartTime <= DateTime.Now && p.EndTime >= DateTime.Now
                                         && p.Organization.OrganizationId.Equals(organizationId)
                                         && p.PackageStatusId.Equals(new Guid("361ADFE9-E58A-4C88-B191-B742CC212443")) //未开始
                                         );
                if (noGetPackages != null)
                {
                    return new ObjectResult(noGetPackages);
                }
                else
                {
                    return NoContent();
                }
            }
        }
    }
}
