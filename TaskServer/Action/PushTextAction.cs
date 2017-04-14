using AlarmAndPlan.Model;
using HttpClientEx;
using Infrastructure.Model;
using Infrastructure.Utility;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PAPS.Data;
using Resources.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskServer.Argument;

namespace TaskServer.Action
{
    /// <summary>
    /// 文字推送 
    /// 
    /// </summary>
    internal sealed class PushTextAction : PlanActionProvider
    {
        List<PushTextArgument> m_actionArgumentList = null;

        ILogger<PushTextAction> _logger = Logger.CreateLogger<PushTextAction>();

        public override void AddPlanActionItem(PlanActionArgument argument)
        {
            if (m_actionArgumentList == null)
                m_actionArgumentList = new List<PushTextArgument>();

            var arg = JsonConvert.DeserializeObject<PushTextArgument>(argument.Argument);
            m_actionArgumentList.Add(arg);
            //m_actionArgumentList.Add(JsonConvert.DeserializeObject<PushTextArgument>(argument.Argument));
            SoundLightAction.UpdateActionArgument(PlanId, arg); //暂时加上，到时动作可单独运行时，将注释当前动作
        }

        public override void Execute()
        {
            return;
            try
            {
                _logger.LogInformation("执行LED消息推送动作 {0} Begin....." ,PlanId);
                //获取声光报警设备，并按组织机构分组
                string url = string.Format("{0}/Resources/IPDevice/systemOptionId={1}", GlobalSetting.AppServerBaseUrl, "A0002016-E009-B019-E001-ABCD11000006");
                IEnumerable<IPDeviceInfo> soundLightDevices = HttpClientHelper.Get<IPDeviceInfo>(url);
                var soundLightDeviceGroup = soundLightDevices.Join(m_actionArgumentList, p => p.IPDeviceInfoId, q => q.SoundLightDeviceId, (p, q) => new { Device = p, Message = q.LedText }).GroupBy(t => t.Device.OrganizationId);

                //获取哨位中心服务
                //url = string.Format("{0}/Resources/Service/ServiceTypeId={1}", GlobalSetting.AppServerBaseUrl, "A0002016-E009-B019-E001-ABCD11300206");
                //IEnumerable<ServiceInfo> services = HttpClientHelper.Get<ServiceInfo>(url);

                //url = string.Format("{0}//Infrastructure/Organization/isorganizationtype=false", GlobalSetting.AppServerBaseUrl);
                //IEnumerable<Organization> orgs = HttpClientHelper.Get<Organization>(url);
                foreach (var sg in soundLightDeviceGroup)
                {
                    //var deviceOrg = orgs.FirstOrDefault(t => t.OrganizationId.Equals(sg.Key));
                    //ServiceInfo service = services.FirstOrDefault(t => t.ServerInfo.OrganizationId.Equals(deviceOrg.ParentOrganizationId) ||
                    //        t.ServerInfo.OrganizationId.Equals(sg.Key));
                    var service = TaskUtility.GetASCService(sg.Key);
                    if (service != null)
                    {
                        //判断是否有报警
                        ASCSApi ascs = new ASCSApi(service);
                        foreach (var device in sg)
                        {
                            string message = device.Message;
                            if (PlanTrigger != null)
                            {
                                var alarmTextObj = TaskUtility.SoundlightConfigList.FirstOrDefault(t => t.AlarmTypeId.Equals(PlanTrigger.AlarmType.SystemOptionId));
                                var alarmdesc = alarmTextObj != null ? alarmTextObj.Description : "";
                                if (PlanId.Equals(PlanTrigger.BeforePlanId))
                                {
                                    message = string.Format("{0}{1},各小组请注意观察!", PlanTrigger.AlarmSource.IPDeviceName, alarmdesc);
                                }
                                else if (PlanId.Equals(PlanTrigger.EmergencyPlanId))
                                {
                                    message = string.Format("{0}{1},各小组按预案处置!", alarmdesc);
                                }
                            }

                            //message = "\0"; //清空
                            _logger.LogInformation("推送消息 Begin ....发送[{0}]到{1}", message, device.Device.IPDeviceName);
                            PushMessage msg = new PushMessage()
                            {
                                DeviceCode = device.Device.IPDeviceCode,
                                Message = message
                            };
                            ascs.PushMessage(msg);
                            _logger.LogInformation("推送消息 End");
                        }
                        _logger.LogInformation("执行LED消息推送动作 {0} End.....",PlanId);
                    }
                    else
                        _logger.LogInformation("未配置哨位中心服务，取消推送消息");
                }
            }catch(Exception ex)
            {
                _logger.LogError("执行文字推送动作异常：{0}\r\n{1}", ex.Message, ex.StackTrace);
            }
        }

        public override void Stop()
        {
            Console.WriteLine("Stop LEDAction............................");
        }
    }
}
