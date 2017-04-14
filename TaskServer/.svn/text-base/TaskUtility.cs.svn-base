using HttpClientEx;
using Infrastructure.Model;
using Infrastructure.Utility;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace TaskServer
{
    public class TaskUtility
    {
        static TaskUtility()
        {
            RefreshASCServiceInfo();

            var appFileName = Process.GetCurrentProcess().MainModule.FileName;
            var appPath = Path.GetDirectoryName(appFileName);
            var alarmJsonFile = Path.Combine(appPath, "alarm.json");
            string content = File.ReadAllText(alarmJsonFile);
            SoundlightConfigList = JsonConvert.DeserializeObject<List<SoundlightConfig>>(content);
        }

        /// <summary>
        /// 声光报警响应报警配置列表
        /// </summary>
        public static List<SoundlightConfig> SoundlightConfigList
        {
            get;set;
        }

        static ILogger<TaskUtility> _logger = Logger.CreateLogger<TaskUtility>();

        static List<ServiceInfo> s_services = new List<ServiceInfo>();

        static List<Organization> s_organization = new List<Organization>();

        /// <summary>
        /// 缓存设备列表
        /// </summary>
        public static List<IPDeviceInfo> s_cacheDeviceList = new List<IPDeviceInfo>();

        /// <summary>
        /// 刷新哨位服务信息
        /// </summary>
        private static void RefreshASCServiceInfo()
        {
            try
            {
                _logger.LogInformation("刷新任务数据 Begin...");

                //string url = string.Format("{0}/Resources/Service/ServiceTypeId={1}", GlobalSetting.AppServerBaseUrl, "A0002016-E009-B019-E001-ABCD11300206");
                string url = string.Format("{0}/Resources/Service", GlobalSetting.AppServerBaseUrl);
                IEnumerable<ServiceInfo> services = HttpClientHelper.Get<ServiceInfo>(url);
                if (services != null)
                    s_services = services.ToList();
                _logger.LogInformation("完成刷新服务数据...");

                url = string.Format("{0}/Infrastructure/Organization/isorganizationtype=false", GlobalSetting.AppServerBaseUrl);
                IEnumerable<Organization> orgs = HttpClientHelper.Get<Organization>(url);
                if (orgs != null)
                    s_organization = orgs.ToList();
                _logger.LogInformation("完成刷新组织机构节点数据...");

                url = string.Format("{0}/Infrastructure/Organization/isorganizationtype=true", GlobalSetting.AppServerBaseUrl);
                orgs = HttpClientHelper.Get<Organization>(url);
                if (orgs != null)
                    s_organization.AddRange(orgs.ToList());
                _logger.LogInformation("完成刷新组织机构数据...");

                url = string.Format("{0}/Resources/IPDevice", GlobalSetting.AppServerBaseUrl);
                IEnumerable<IPDeviceInfo> devices = HttpClientHelper.Get<IPDeviceInfo>(url);
                if (devices != null)
                    s_cacheDeviceList.AddRange(devices);
                _logger.LogInformation("完成刷新设备数据...");
                _logger.LogInformation("刷新任务数据 End...");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("刷新任务数据：Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 根据设备所在的组织机构id获取所属的哨位中心
        /// </summary>
        /// <param name="deviceOrganizationId"></param>
        /// <returns></returns>
        public static ServiceInfo GetASCService(Guid deviceOrganizationId)
        {
            ServiceInfo service = null;
            if (s_organization != null && s_services != null)
            {
                var deviceOrg = s_organization.FirstOrDefault(t => t.OrganizationId.Equals(deviceOrganizationId));
                Guid serviceTypeId = Guid.Parse("A0002016-E009-B019-E001-ABCD11300206");
                service = s_services.FirstOrDefault(t => (t.ServerInfo.OrganizationId.Equals(deviceOrg.ParentOrganizationId) ||
                       t.ServerInfo.OrganizationId.Equals(deviceOrg.OrganizationId)) && t.ServiceTypeId.Equals(serviceTypeId));
            }
            return service;
        }

        /// <summary>
        /// 获取默认哨位中心服务
        /// </summary>
        /// <returns></returns>
        public static ServiceInfo GetDefaultASCService()
        {
            ServiceInfo service = null;
            if (s_services != null)
            {
                Guid serviceTypeId = Guid.Parse("A0002016-E009-B019-E001-ABCD11300206");
                service = s_services.FirstOrDefault(t => t.ServiceTypeId.Equals(serviceTypeId));
            }
            return service;
        }

        /// <summary>
        /// 获取默认矩阵中心服务
        /// </summary>
        /// <returns></returns>
        public static ServiceInfo GetDefaultDMCService()
        {
            ServiceInfo service = null;
            if (s_services != null)
            {
                Guid serviceTypeId = Guid.Parse("a0002016-e009-b019-e001-abcd11300205");
                service = s_services.FirstOrDefault(t => t.ServiceTypeId.Equals(serviceTypeId));
            }
            return service;
        }
    }
}
