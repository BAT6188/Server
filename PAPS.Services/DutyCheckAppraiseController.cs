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
    /// 查勤评价控制类
    /// </summary>
    public class DutyCheckAppraiseController : Controller
    {

        /// <summary>
        /// 获取所有查勤评价
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<DutyCheckAppraise> GetAll()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var data = db.DutyCheckAppraise.Include(t=>t.AppraiseICO).Include(t=>t.AppraiseType).Include(t=>t.Organization).OrderBy(p=>p.OrderNo);
                return data.ToList();
            }
        }

        /// <summary>
        /// 根据评价类型ID获取查勤评价
        /// </summary>
        /// <param name="appraiseType">评价类型ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Paps/dutyCheckAppraise/appraiseType={appraiseType}")]
        public IActionResult GetDutyCheckAppraiseByAppraiseType(Guid appraiseType)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var data = db.DutyCheckAppraise.Include(t => t.AppraiseICO).Include(t => t.AppraiseType).Include(t => t.Organization)
                        .Where(p => p.AppraiseType.SystemOptionId.Equals(appraiseType));
                    if (data == null)
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
        /// 根据组织机构ID获取查勤评价
        /// </summary>
        /// <param name="organizationId">组织机构ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Paps/dutyCheckAppraise/organizationId={organizationId}")]
        public IActionResult GetDutyCheckAppraiseByOrganizationID(Guid organizationId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var data = db.DutyCheckAppraise.Include(t => t.AppraiseICO).Include(t => t.AppraiseType).Include(t => t.Organization)
                        .Where(p => p.Organization.OrganizationId.Equals(organizationId));
                    if (data == null)
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
        /// 根据查勤评价ID获取查勤评价
        /// </summary>
        /// <param name="id">查勤评价ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                DutyCheckAppraise data = db.DutyCheckAppraise.Include(t => t.AppraiseICO).Include(t => t.AppraiseType).Include(t => t.Organization)
                    .FirstOrDefault(p => p.DutyCheckAppraiseId.Equals(id));
                if (data == null)
                {
                    return NoContent();
                }

                return new ObjectResult(data);
            }
        }


        /// <summary>
        /// 新增查勤评价
        /// </summary>
        /// <param name="model">查勤评价实体</param>
        /// <returns>返回值</returns>
        [HttpPost]
        public IActionResult Add([FromBody]DutyCheckAppraise model)
        {

            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.DutyCheckAppraise.Add(model);
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
        /// 修改查勤评价
        /// </summary>
        /// <param name="model">查勤评价实体</param>
        /// <returns>返回值</returns>
        [HttpPut]
        public IActionResult Update([FromBody]DutyCheckAppraise model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.DutyCheckAppraise.Update(model);
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
        ///  根据查勤评价ID删除查勤评价
        /// </summary>
        /// <param name="id">查勤评价ID</param>
        /// <returns>返回值</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    DutyCheckAppraise data = db.DutyCheckAppraise.FirstOrDefault(p => p.DutyCheckAppraiseId == id);
                    if (data == null)
                    {
                        return NoContent();
                    }
                    db.DutyCheckAppraise.Remove(data);
                    db.SaveChanges();
                    return new NoContentResult();
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }
    }
}
