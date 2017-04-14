using PAPS.Model;
using AllInOneContext;
using Infrastructure.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Utility;

namespace PAPS.Services
{
    [Route("Paps/[controller]")]
    /// <summary>
    /// 查勤编组控制类
    /// </summary>
    public class DutyCheckGroupController : Controller
    {

        /// <summary>
        /// 获取所有查勤编组
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<DutyCheckGroup> GetAll()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var data = db.DutyCheckGroup
                    .Include(t => t.CheckDutySiteSchedules)/*.ThenInclude(t => t.CheckDutySite)*/
                    .Include(t => t.CheckDutySiteSchedules).ThenInclude(t => t.CheckMan)
                    ;
                return data.ToList();
            }
        }


        /// <summary>
        /// 根据查勤编组ID获取查勤编组
        /// </summary>
        /// <param name="id">查勤编组ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                DutyCheckGroup data = db.DutyCheckGroup
                    .Include(t => t.CheckDutySiteSchedules)/*.ThenInclude(t => t.CheckDutySite)*/
                    .Include(t => t.CheckDutySiteSchedules).ThenInclude(t => t.CheckMan)
                    .FirstOrDefault(p => p.DutyCheckGroupId.Equals(id));
                if (data == null)
                {
                    return NoContent();
                }

                return new ObjectResult(data);
            }
        }


        /// <summary>
        /// 新增查勤编组
        /// </summary>
        /// <param name="model">查勤编组实体</param>
        /// <returns>返回值</returns>
        [HttpPost]
        public IActionResult Add([FromBody]DutyCheckGroup model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.DutyCheckGroup.Add(model);
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
        /// 修改查勤编组
        /// </summary>
        /// <param name="model">查勤编组实体</param>
        /// <returns>返回值</returns>
        [HttpPut]
        public IActionResult Update([FromBody]DutyCheckGroup model)
        {

            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        DutyCheckGroup group = db.DutyCheckGroup
                                            .Include(t => t.CheckDutySiteSchedules)/*.ThenInclude(t => t.CheckDutySite)*/
                                            .Include(t => t.CheckDutySiteSchedules).ThenInclude(t => t.CheckMan)
                                            .FirstOrDefault(p => p.DutyCheckGroupId.Equals(model.DutyCheckGroupId));
                        if (group == null)
                            return BadRequest();
                        //转换普通数据
                        group.DutyGroupName = model.DutyGroupName;
                        //
                        RemoveLinkage(db, group);
                        //
                        group.CheckDutySiteSchedules = model.CheckDutySiteSchedules;
                        db.DutyCheckGroup.Update(group);
                        db.SaveChanges();

                        tran.Commit();
                        return new NoContentResult();
                    }
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

        private static void RemoveLinkage(AllInOneContext.AllInOneContext db, DutyCheckGroup group)
        {
            //手动移除级联属性
            if (group.CheckDutySiteSchedules != null)
            {
                List<DutyCheckSiteSchedule> delList = new List<DutyCheckSiteSchedule>();
                foreach (DutyCheckSiteSchedule rp in group.CheckDutySiteSchedules)
                {
                    DutyCheckSiteSchedule del = db.Set<DutyCheckSiteSchedule>()
                        .FirstOrDefault(p => p.CheckDutySiteId.Equals(rp.CheckDutySiteId) && p.CheckManId.Equals(rp.CheckManId));
                    if (del != null)
                    {
                        delList.Add(del);
                    }
                }
                db.Set<DutyCheckSiteSchedule>().RemoveRange(delList);
                db.SaveChanges();
            }
        }

        /// <summary>
        ///  根据查勤编组ID删除查勤编组
        /// </summary>
        /// <param name="id">查勤编组ID</param>
        /// <returns>返回值</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        DutyCheckGroup group = db.DutyCheckGroup
                                            .Include(t => t.CheckDutySiteSchedules)/*.ThenInclude(t => t.CheckDutySite)*/
                                            .Include(t => t.CheckDutySiteSchedules).ThenInclude(t => t.CheckMan)
                                            .FirstOrDefault(p => p.DutyCheckGroupId.Equals(id));
                        if (group == null)
                        {
                            return NoContent();
                        }
                        //
                        RemoveLinkage(db, group);
                        //
                        db.DutyCheckGroup.Remove(group);
                        db.SaveChanges();

                        tran.Commit();
                        return new NoContentResult();
                    }
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }
    }
}
