using Infrastructure.Model;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resources.Data;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Resources.Services
{
    [Route("Resources/[controller]")]
    public class ServiceController:Controller
    {
        private readonly ILogger<ServiceController> _logger;
        public ServiceController(ILogger<ServiceController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Add([FromBody]ServiceInfo Service)
        {
            if (Service == null)
                return BadRequest("Service object can not be null!");
            if (ExistsName(Service.ServiceName, Service.ServiceInfoId))
                return BadRequest("Service name has been used!");
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    Service.Modified = DateTime.Now;
                    db.ServiceInfo.Add(Service);
                    db.SaveChanges();
                    return CreatedAtAction("", Service);
                }
                catch (Exception ex)
                {
                    _logger.LogError("添加服务器异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody]ServiceInfo Service)
        {
            if (Service == null)
                return BadRequest("Service object can not be null!");
            if (ExistsName(Service.ServiceName, Service.ServiceInfoId))
                return BadRequest("Service name has been used!");

            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    Service.Modified = DateTime.Now;
                    db.ServiceInfo.Update(Service);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    _logger.LogError("更新服务器异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    ServiceInfo delObj = db.ServiceInfo.FirstOrDefault(s => s.ServiceInfoId.Equals(id));
                    if (delObj == null)
                    {
                        return NotFound();
                    }
                    db.ServiceInfo.Remove(delObj);
                    db.SaveChanges();
                    return NoContent();
                }
                catch (Exception ex)
                {
                    _logger.LogError("删除服务器异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
        }

        //[HttpGet("{id}",Name ="GetById")]
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            _logger.LogInformation("Get Service info by id {0}", id);
            using (var db = new AllInOneContext.AllInOneContext())
            {
                ServiceInfo si = db.ServiceInfo.FirstOrDefault(t => t.ServiceInfoId.Equals(id));
                if (si == null)
                {
                    return NotFound();
                }
                return new ObjectResult(si);
            }
        }

        private IQueryable<ServiceInfo> GetDbQuery(AllInOneContext.AllInOneContext dbContext)
        {
            return  dbContext.ServiceInfo.Include(t => t.ServerInfo).ThenInclude(t => t.Organization).Include(t => t.ServiceType);
        }

        [HttpGet]
        [Route("~/Resources/Service/organizationId={organizationId}")]
        //http://127.0.0.1:5001/Resources/Service/organizationId=b31f22c1-bcd8-4b5a-ad5b-70760a1a9d74?ServiceTypeId=a0002016-e009-b019-e001-abcd11300204
        public IEnumerable<ServiceInfo> GetByOrganization(Guid organizationId, Guid ServiceTypeId, bool includeLower = false)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var query = GetDbQuery(db);
                if (includeLower)
                {
                    var org = db.Organization.FirstOrDefault(t => t.OrganizationId.Equals(organizationId));
                    if (org != null)
                        return query.Where(s => (ServiceTypeId.Equals(Guid.Empty) || s.ServiceTypeId.Equals(ServiceTypeId)) &&
                            s.ServerInfo.Organization.OrganizationFullName.Contains(org.OrganizationFullName)).ToList();
                    return null;
                }
                else
                {
                    return query.Where(s => s.ServerInfo.OrganizationId.Equals(organizationId)
                           && (ServiceTypeId.Equals(Guid.Empty) || s.ServiceTypeId.Equals(ServiceTypeId))).ToList();
                }
            }
        }

        [HttpGet]
        [Route("~/Resources/Service/serviceTypeId={serviceTypeId}")]
        //http://127.0.0.1:5001/Resources/Service/ServiceTypeId=a0002016-e009-b019-e001-abcd11300204
        public IEnumerable<ServiceInfo> GetByServiceType(Guid serviceTypeId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var query = GetDbQuery(db);

                return query.Where(s => s.ServiceTypeId.Equals(serviceTypeId)).ToList();
            }
        }

        [HttpGet]
        public IEnumerable<ServiceInfo> GetAll()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return GetDbQuery(db).ToList();
            }
        }

        /// <summary>
        /// 获取使用该服务器的所有服务信息
        /// </summary>
        /// <param name="serverInfoId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Resources/Service/serverInfoId={serverInfoId}")]
        public IEnumerable<ServiceInfo> GetByServer(Guid serverInfoId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return db.ServiceInfo.Where(t => t.ServerInfoId.Equals(serverInfoId)).ToList();
            }
        }

        /// <summary>
        /// 判断组织机构下的服务器名称是否已存在
        /// </summary>
        /// <param name="name"></param>
        /// <param name="organizationId"></param>
        /// <param name="Serviceid">设备id,更新需考虑</param>
        /// <returns></returns>
        public bool ExistsName(string name, Guid Serviceid)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return db.ServiceInfo.Where(t => t.ServiceName.Equals(name)
                    && !Serviceid.Equals(t.ServiceInfoId)).Count() > 0;
            }
        }

        /// <summary>
        /// 获取服务器视图数据
        /// </summary>
        /// <param name="serviceTypeId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Resources/Service/View/serviceTypeId={serviceTypeId}")]
        //http://127.0.0.1:5001/Resources/Service/View/ServiceTypeId=a0002016-e009-b019-e001-abcd11300204
        public IEnumerable<ServiceInfoView> GetServiceInfoViewByServiceType(Guid serviceTypeId)
        {
            List<ServiceInfoView> services = new List<ServiceInfoView>();
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var list = GetDbQuery(db).Where(s => s.ServiceTypeId.Equals(serviceTypeId)).ToList();
                list.ForEach(t=>{
                    ServiceInfoView sv = new ServiceInfoView() {
                        ServiceInfoId = t.ServiceInfoId,
                        ServiceName = t.ServiceName,
                        ServiceTypeId = t.ServiceTypeId,
                        EndPoints = t.EndPoints,
                        Password = t.Password,
                        Username = t.Username
                    };
                    services.Add(sv);
                });
            }
            return services;
        }

        /// <summary>
        /// 获取哨位服务信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Resources/Service/sentinel")]
        public ServiceInfo GetSentinelServerInfo()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return db.ServiceInfo.Include(t=>t.ServiceType).FirstOrDefault(t => t.ServiceType.SystemOptionCode.Equals("11300206"));
            }
        }
    }
}
