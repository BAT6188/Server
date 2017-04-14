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
    /// <summary>
    /// 故障控制类
    /// </summary>
    [Route("Paps/[controller]")]
    public class FaultController : Controller
    {
        /// <summary>
        /// 根据故障ID获取故障信息
        /// </summary>
        /// <param name="id">故障ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            if (id == null || id == new Guid())
                return NoContent();

            using (var db = new AllInOneContext.AllInOneContext())
            {
                Fault Fault = db.Set<Fault>()
                            .Include(t => t.CheckDutySite)
                            .Include(t => t.CheckMan)
                            .Include(t => t.DutyCheckOperation).ThenInclude(t => t.Attachments)
                            .Include(t => t.DutyOrganization)
                            .Include(t => t.FaultType)
                            .FirstOrDefault(p => p.FaultId.Equals(id));

                if (Fault == null)
                    return NoContent();

                return new ObjectResult(Fault);
            }

        }

        [HttpPost]
        public IActionResult Add([FromBody]Fault model)
        {
            try
            {
                if (model == null)
                    return NoContent();

                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.Set<Fault>().Add(model);
                    db.SaveChanges();
                    //
                    PushFault(model);
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


        /// <summary>
        /// 根据参数查询故障信息
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="currentPage">当前页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="faultTypeId">故障类型Id</param>
        /// <param name="checkDutySiteId">检查点ID</param>
        /// <param name="dutyOrganizationId">组织机构ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Paps/Fault/Query")]
        public IActionResult GetFaultByParameter(DateTime startTime, DateTime endTime, int currentPage,
            int pageSize,Guid?[] faultTypeId,Guid?[] checkDutySiteId,Guid? dutyOrganizationId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var Query = from p in db.Set<Fault>()
                                .Include(t => t.CheckDutySite)
                                .Include(t => t.CheckMan)
                                .Include(t => t.DutyCheckOperation).ThenInclude(t => t.Attachments)
                                .Include(t => t.DutyOrganization)
                                .Include(t => t.FaultType)
                                orderby p.CircularTime descending
                                where p.CircularTime >= startTime && p.CircularTime <= endTime
                                && ((faultTypeId == null || faultTypeId.Length == 0) || faultTypeId.Contains(p.FaultTypeId))
                                && ((checkDutySiteId == null || checkDutySiteId.Length == 0) || checkDutySiteId.Contains(p.CheckDutySiteId))
                                && ((dutyOrganizationId == null) || p.DutyOrganizationId.Equals(dutyOrganizationId))
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
        /// 广播故障信息
        /// </summary>
        /// <param name="Fault"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/Paps/Fault/Broadcast")]
        public IActionResult BroadcastFault([FromBody]Fault Fault)
        {
            if (Fault == null)
                return BadRequest("Fault object can not be null!");

            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    MQPulish.PublishMessage("Fault", Fault);
                    return CreatedAtAction("", Fault);
                }
                catch (Exception ex)
                {
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
        }

        /// <summary>
        /// 推送故障至下级
        /// </summary>
        /// <param name="Fault"></param>
        public bool PushFault(Fault model)
        {
            if (model == null)
                return false;
            using (var db = new AllInOneContext.AllInOneContext())
            {
                Fault Fault = db.Set<Fault>()
                                .Include(t => t.CheckDutySite)
                                .Include(t => t.CheckMan)
                                .Include(t => t.DutyCheckOperation).ThenInclude(t => t.Attachments)
                                .Include(t => t.DutyOrganization)
                                .Include(t => t.FaultType)
                                .FirstOrDefault(p => p.FaultId.Equals(model.FaultId));
                if (Fault == null)
                    return false;
                //查找监控点所在的组织机构
                Organization org = db.Organization
                    .Include(t => t.Center)
                    .FirstOrDefault(p => p.OrganizationId.Equals(Fault.CheckDutySite.Organization.ParentOrganizationId));
                if (org == null || org.Center == null || org.Center.EndPoints == null || org.Center.EndPoints.Count == 0)
                    return false;


                string url = string.Format("http://{0}:{1}/Paps/Fault/Broadcast", org.Center.EndPoints[0].IPAddress, org.Center.EndPoints[0].Port);

                var ret = HttpClientHelper.Post(Fault, url);

                return ret.Success;
            }

        }

    }
}
