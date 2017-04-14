using HttpClientEx;
using Infrastructure.Model;
using Infrastructure.Utility;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PAPS.Data;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskServer.Argument;

namespace TaskServer.Action
{
    /// <summary>
    /// 喊话动作
    /// </summary>
    internal class ShoutAction : PlanActionProvider
    {

        private List<ShoutArgument> m_actionArgumentList = null;

        ILogger<ShoutAction> _logger = Logger.CreateLogger<ShoutAction>();

        public override void AddPlanActionItem(PlanActionArgument argument)
        {
            if (m_actionArgumentList == null)
                m_actionArgumentList = new List<ShoutArgument>();
            m_actionArgumentList.Add(JsonConvert.DeserializeObject<ShoutArgument>(argument.Argument));
        }

        public override void Execute()
        {
            //获取哨位设备，并按组织机构分组
            try
            {
                _logger.LogInformation("执行开启喊话动作！！！");
                string url = string.Format("{0}/Resources/IPDevice/systemOptionId={1}", GlobalSetting.AppServerBaseUrl, "A0002016-E009-B019-E001-ABCD11000004");
                IEnumerable<IPDeviceInfo> sentinels = HttpClientHelper.Get<IPDeviceInfo>(url);
                var sentinelGroup = sentinels.Join(m_actionArgumentList, p => p.IPDeviceInfoId, q => q.SentinelId, (p, q) => p).GroupBy(t => t.OrganizationId);

                ////获取哨位中心服务
                //url = string.Format("{0}/Resources/Service/ServiceTypeId={1}", GlobalSetting.AppServerBaseUrl, "A0002016-E009-B019-E001-ABCD11300206");
                //IEnumerable<ServiceInfo> services = HttpClientHelper.Get<ServiceInfo>(url);

                //url = string.Format("{0}//Infrastructure/Organization/isorganizationtype=false", GlobalSetting.AppServerBaseUrl);
                //IEnumerable<Organization> orgs = HttpClientHelper.Get<Organization>(url);
                foreach (var sg in sentinelGroup)
                {
                    //var deviceOrg = orgs.FirstOrDefault(t => t.OrganizationId.Equals(sg.Key));
                    //ServiceInfo ascs = services.FirstOrDefault(t => t.ServerInfo.OrganizationId.Equals(deviceOrg.ParentOrganizationId) ||
                    //        t.ServerInfo.OrganizationId.Equals(sg.Key));
                    var ascs = TaskUtility.GetASCService(sg.Key);
                    if (ascs != null)
                    {
                        foreach (var device in sg)
                        {
                            var sentinelCode = 0;
                            if (Int32.TryParse(device.Organization.OrganizationCode, out sentinelCode))
                                device.IPDeviceCode = sentinelCode;
                            SentinelShout shout = new SentinelShout()
                            {
                                DeviceId = device.IPDeviceInfoId,
                                SentinelCode = sentinelCode, //device.IPDeviceCode,
                                Open = true
                            };

                            ASCSApi api = new ASCSApi(ascs);
                            api.Shout(shout);
                        }
                    }
                }
                _logger.LogInformation("执行启动喊话动作完成！！！");
            }
            catch (Exception ex)
            {
                _logger.LogError("执行启动喊话动作异常：{0}\r\n{1}", ex.Message, ex.StackTrace);
            }
        }

        public override void Stop()
        {
        }
    }
}
