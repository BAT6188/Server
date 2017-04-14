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
    /// 查勤问题选项控制类
    /// </summary>
    public class DutyCheckMatterController : Controller
    {

        /// <summary>
        /// 获取所有查勤问题选项
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<DutyCheckMatter> GetAll()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var data = db.DutyCheckMatter
                        .Include(t=>t.MatterICO)
                        .Include(t=>t.Organization)
                        .Include(t=>t.VoiceFile);
                return data.ToList();
            }
        }

        /// <summary>
        /// 根据组织机构ID获取查勤问题选项
        /// </summary>
        /// <param name="organizationId">组织机构ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Paps/dutyCheckMatter/organizationId={organizationId}")]
        public IActionResult GetDutyCheckMatterByOrganizationID(Guid organizationId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var data = db.DutyCheckMatter
                            .Include(t=>t.MatterICO)
                            .Include(t=>t.Organization)
                            .Include(t=>t.VoiceFile)
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
        /// 根据查勤问题选项ID获取查勤问题选项
        /// </summary>
        /// <param name="id">查勤问题选项ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    DutyCheckMatter data = db.DutyCheckMatter
                        .Include(t => t.MatterICO)
                        .Include(t => t.Organization)
                        .Include(t => t.VoiceFile)
                        .FirstOrDefault(p => p.DutyCheckMatterId.Equals(id));
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
        /// 新增查勤问题选项
        /// </summary>
        /// <param name="model">查勤问题选项实体</param>
        /// <returns>返回值</returns>
        [HttpPost]
        public IActionResult Add([FromBody]DutyCheckMatter model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.DutyCheckMatter.Add(model);
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
        /// 修改查勤问题选项
        /// </summary>
        /// <param name="model">查勤问题选项实体</param>
        /// <returns>返回值</returns>
        [HttpPut]
        public IActionResult Update([FromBody]DutyCheckMatter model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.DutyCheckMatter.Update(model);
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
        ///  根据查勤问题选项ID删除查勤问题选项
        /// </summary>
        /// <param name="id">查勤问题选项ID</param>
        /// <returns>返回值</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    DutyCheckMatter data = db.DutyCheckMatter.FirstOrDefault(p => p.DutyCheckMatterId == id);
                    if (data == null)
                    {
                        return NoContent();
                    }
                    db.DutyCheckMatter.Remove(data);
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
