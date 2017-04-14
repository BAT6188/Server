using Infrastructure.Model;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Resources.Data;
using Resources.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Resources.Services
{
    [Route("Resources/[controller]")]
    public class ServerController : Controller
    {
        private readonly ILogger<ServerController> _logger;
        public ServerController(ILogger<ServerController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Add([FromBody]ServerInfo server)
        {
            if (server == null)
                return BadRequest("Server object can not be null!");
            if (ExistsName(server.ServerName, server.OrganizationId, server.ServerInfoId))
                return BadRequest("Server name has been used!");
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    server.Modified = DateTime.Now;
                    db.ServerInfo.Add(server);
                    if (server.Services != null && server.Services.Count > 0)
                    {
                        server.Services.ForEach(t => t.ServerInfoId = server.ServerInfoId);
                        db.ServiceInfo.AddRange(server.Services);
                    }
                    db.SaveChanges();
                    return CreatedAtAction("", server);
                }
                catch (Exception ex)
                {
                    _logger.LogError("添加物理服务器异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody]ServerInfo server)
        {
            if (server == null)
                return BadRequest("Server object can not be null!");
            if (ExistsName(server.ServerName, server.OrganizationId, server.ServerInfoId))
                return BadRequest("Server name has been used!");

            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    server.Modified = DateTime.Now;
                    db.ServerInfo.Update(server);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    _logger.LogError("更新物理服务器异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
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
                    ServerInfo delObj = db.ServerInfo.FirstOrDefault(s => s.ServerInfoId.Equals(id));
                    if (delObj == null)
                    {
                        return NotFound();
                    }
                    db.ServerInfo.Remove(delObj);
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
            _logger.LogInformation("Get server info by id {0}", id);
            using (var db = new AllInOneContext.AllInOneContext())
            {
                ServerInfo si = db.ServerInfo.FirstOrDefault(t => t.ServerInfoId.Equals(id));
                if (si == null)
                {
                    return NotFound();
                }
                return new ObjectResult(si);
            }
        }
      

        [HttpGet]
        [Route("~/Resources/Server/organizationId={organizationId}")]
        //http://127.0.0.1:5001/Resources/Server/organizationId=b31f22c1-bcd8-4b5a-ad5b-70760a1a9d74
        public IEnumerable<ServerInfo> GetByOrganization(Guid organizationId, bool includeLower=false)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var query = db.ServerInfo.Include(t => t.Organization);
                if (includeLower)
                {
                    var org = db.Organization.FirstOrDefault(t => t.OrganizationId.Equals(organizationId));
                    if (org != null)
                        return query.Where(s => s.Organization.OrganizationFullName.Contains(org.OrganizationFullName)).ToList();
                    return null;
                }
                else
                {
                    return query.Where(s => s.OrganizationId.Equals(organizationId)).ToList();
                }
            }
        }

        [HttpGet]
        public IEnumerable<ServerInfo> GetAll()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return db.ServerInfo.Include(t=>t.Organization).ToList();
            }
        }

        /// <summary>
        /// 判断组织机构下的服务器名称是否已存在
        /// </summary>
        /// <param name="name"></param>
        /// <param name="organizationId"></param>
        /// <param name="serverid">设备id,更新需考虑</param>
        /// <returns></returns>
        public bool ExistsName(string name, Guid organizationId, Guid serverid)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return db.ServerInfo.Where(t => t.ServerName.Equals(name)
                    && t.OrganizationId.Equals(organizationId)
                    && !serverid.Equals(t.ServerInfoId)).Count() > 0;
            }
        }

        static UdpClient m_UdpClient = null;

        //static bool SearchingServer = false;

        static Task RecvTask = null;

        static Hashtable ServerSet = new Hashtable();

        /// <summary>
        /// 搜索服务器，广播搜索指令后等待5s回复
        /// </summary>
        /// <returns></returns>
        //http://192.168.18.71:5001/Resources/Server/search/sn=afa6cab1-b805-42ca-83fc-98d33ae1c660
        [HttpGet]
        [Route("~/Resources/Server/Search/sn={sn}")]
        public async Task<IEnumerable<ServerInfo>> SearchServer(Guid sn)
        {
            if (m_UdpClient == null)
                m_UdpClient = new UdpClient(new IPEndPoint(IPAddress.Any, 10022));
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Broadcast, 10021);
            var cmd = new CmdRequest()
            {
                SN = sn,
                Cmd = "reqNetwork"
            };
            if (RecvTask == null)
            {
                RecvTask = Task.Factory.StartNew(() => Recv());
            }

            byte[] buf = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(cmd));
            int i = await m_UdpClient.SendAsync(buf, buf.Length, endpoint);
            await Task.Delay(GlobalSetting.SearchServerTimeout * 1000);
         //   return GetTestData();
            var servers = ServerSet[sn] as List<ServerInfo>;
            if (servers != null)
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    servers.ForEach(t =>
                    {
                        if (t.Services != null)
                        {
                            t.Services.ForEach(f =>
                            {
                                var option = db.SystemOption.FirstOrDefault(k => k.SystemOptionId.Equals(f.ServiceTypeId));
                                f.ServiceType = option;
                            });
                        }
                    });
                }
            }
            return servers;
        }

        private List<ServerInfo> GetTestData()
        {
            List<ServiceInfo> views = new List<ServiceInfo>();
            List<EndPointInfo> endpoints = new List<EndPointInfo>();
            endpoints.Add(new EndPointInfo() { IPAddress = "190.1.1.23", Port = 200 });
            views.Add(new ServiceInfo()
            {
                ServiceTypeId = Guid.Parse("a0002016-e009-b019-e001-abcd11300200"),
                EndPoints = endpoints
            });
            views.Add(new ServiceInfo()
            {
                ServiceTypeId = Guid.Parse("a0002016-e009-b019-e001-abcd11300202"),
                EndPoints = endpoints
            });
            var searchInfo = new
            {
                EndPointsJson = "[{\"IPAddress\":\"2.3.6.5\",\"Port\":0}]",
                Services = views
            };
            List<ServerInfo> servers = new List<ServerInfo>();
            servers.Add(new ServerInfo()
            {
                EndPointsJson = "[{\"IPAddress\":\"2.3.6.5\",\"Port\":0}]",
                Services = views,
            });
            return servers;
        }

        /// <summary>
        /// 等待回复
        /// </summary>
        async Task Recv()
        {
            while (true)
            {
                var recv = await m_UdpClient.ReceiveAsync();
                string msg = Encoding.UTF8.GetString(recv.Buffer);
                //Json
                try
                {
                    if (msg.Contains("rspNetwork"))
                    {
                        var response = JsonConvert.DeserializeObject<CmdResponse<ServerInfo>>(msg);
                        List<ServerInfo> servers = null;
                        if (ServerSet.ContainsKey(response.SN))
                            servers = ServerSet[response.SN] as List<ServerInfo>;
                        else
                        {
                            servers = new List<ServerInfo>();
                            ServerSet[response.SN] = servers;
                        }
                        ServerInfo serverInfo = response.Message;//JsonConvert.DeserializeObject<ServerInfo>(response.Message);
                        if (serverInfo != null)
                        {
                            //检查是否存在重复的数据.出现广播请求后，服务器收到2次请求指令
                            bool exists = false;
                            foreach (var server in servers)
                            {
                                if (server.EndPointsJson.Equals(serverInfo.EndPointsJson))
                                {
                                    exists = true;
                                    break;
                                }
                            }
                            if (!exists)
                                servers.Add(serverInfo);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("通用消息协议处理异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                }
            }
        }

    }
}
