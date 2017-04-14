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
    /// 打开子弹箱控制
    /// </summary>
    internal  sealed class CartidgeboxAction : PlanActionProvider
    {
        private List<CartidgeboxArgument> m_actionArgumentList = null;

        ILogger<CartidgeboxAction> _logger = Logger.CreateLogger<CartidgeboxAction>();

        public override void AddPlanActionItem(PlanActionArgument argument)
        {
            if (m_actionArgumentList == null)
                m_actionArgumentList = new List<CartidgeboxArgument>();
            m_actionArgumentList.Add(JsonConvert.DeserializeObject<CartidgeboxArgument>(argument.Argument));
        }

        //public override void Execute()
        //{
        //    //获取哨位设备，并按组织机构分组
        //    try
        //    {
        //        _logger.LogInformation("执行打开子弹箱动作 {0} Begin...",PlanId);
        //        string url = string.Format("{0}/Resources/IPDevice/systemOptionId={1}", GlobalSetting.AppServerBaseUrl, "A0002016-E009-B019-E001-ABCD11000004");
        //        IEnumerable<IPDeviceInfo> sentinels = HttpClientHelper.Get<IPDeviceInfo>(url);
        //        var sentinelGroup = sentinels.Join(m_actionArgumentList, p => p.IPDeviceInfoId, q => q.SentinelId, (p, q) => p).GroupBy(t => t.OrganizationId);

        //        ////获取哨位中心服务
        //        //url = string.Format("{0}/Resources/Service/ServiceTypeId={1}", GlobalSetting.AppServerBaseUrl, "A0002016-E009-B019-E001-ABCD11300206");
        //        //IEnumerable<ServiceInfo> services = HttpClientHelper.Get<ServiceInfo>(url);

        //        //url = string.Format("{0}//Infrastructure/Organization/isorganizationtype=false", GlobalSetting.AppServerBaseUrl);
        //        //IEnumerable<Organization> orgs = HttpClientHelper.Get<Organization>(url);

        //        foreach (var sg in sentinelGroup)
        //        {
        //            //var deviceOrg = orgs.FirstOrDefault(t => t.OrganizationId.Equals(sg.Key));
        //            //ServiceInfo ascs = services.FirstOrDefault(t => t.ServerInfo.OrganizationId.Equals(deviceOrg.ParentOrganizationId) ||
        //            //        t.ServerInfo.OrganizationId.Equals(sg.Key));
        //            var ascs = TaskUtility.GetASCService(sg.Key);
        //            if (ascs != null)
        //            {
        //                List<CartidgeBoxStatus> statuses = new List<CartidgeBoxStatus>();

        //                foreach (var device in sg)
        //                {
        //                    var sentinelCode = 0;
        //                    if (Int32.TryParse(device.Organization.OrganizationCode, out sentinelCode))
        //                        device.IPDeviceCode = sentinelCode;
        //                    CartidgeBoxStatus status = new CartidgeBoxStatus()
        //                    {
        //                        DeviceId = device.IPDeviceInfoId,
        //                        SentinelCode = sentinelCode, //device.IPDeviceCode,
        //                        BulletBoxStatus = true
        //                    };
        //                    statuses.Add(status);
        //                }
        //                _logger.LogInformation("下发打开子弹箱 Begin ：{0}", JsonUtility.CamelCaseSerializeObject(statuses));
        //                ASCSApi api = new ASCSApi(ascs);
        //                api.CatridgeBoxStatus(statuses);
        //                _logger.LogInformation("下发打开子弹箱 End...");
        //            }
        //        }
        //        _logger.LogInformation("执行打开子弹箱动作 {0} End...",PlanId);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("执行打开子弹箱动作异常：{0}\r\n{1}", ex.Message, ex.StackTrace);
        //    }
        //}

        public override void Execute()
        {
            //获取哨位设备，并按组织机构分组
            try
            {
                _logger.LogInformation("执行打开子弹箱动作 {0} Begin...", PlanId);

                var sentinels = TaskUtility.s_cacheDeviceList;
                if (sentinels == null || sentinels.Count == 0)
                {
                    string url = string.Format("{0}/Resources/IPDevice/systemOptionId={1}", GlobalSetting.AppServerBaseUrl, "A0002016-E009-B019-E001-ABCD11000004");
                    sentinels = HttpClientHelper.Get<IPDeviceInfo>(url).ToList();
                }
                var sentinelIpdevices = sentinels.Join(m_actionArgumentList, p => p.IPDeviceInfoId, q => q.SentinelId, (p, q) => p).ToList();

                var ascs = TaskUtility.GetDefaultASCService();

                if (ascs != null)
                {
                    List<CartidgeBoxStatus> statuses = new List<CartidgeBoxStatus>();
                    foreach (var device in sentinelIpdevices)
                    {
                        var sentinelCode = 0;
                        if (Int32.TryParse(device.Organization.OrganizationCode, out sentinelCode))
                            device.IPDeviceCode = sentinelCode;
                        CartidgeBoxStatus status = new CartidgeBoxStatus()
                        {
                            DeviceId = device.IPDeviceInfoId,
                            SentinelCode = sentinelCode,
                            BulletBoxStatus = true
                        };
                        statuses.Add(status);
                    }
                    _logger.LogInformation("下发打开子弹箱 Begin ：{0}", JsonUtility.CamelCaseSerializeObject(statuses));
                    ASCSApi api = new ASCSApi(ascs);
                    api.CatridgeBoxStatus(statuses);
                    _logger.LogInformation("下发打开子弹箱 End...");
                }
                _logger.LogInformation("执行打开子弹箱动作 {0} End...", PlanId);
            }
            catch (Exception ex)
            {
                _logger.LogError("执行打开子弹箱动作异常：{0}\r\n{1}", ex.Message, ex.StackTrace);
            }
        }

        public override void Stop()
        {
            Console.WriteLine("Stop BulletAction............................");
        }
    }
}
