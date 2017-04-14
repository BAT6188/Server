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
    /// 语音播报 
    /// 
    /// </summary>
    class BroadcastAudioAction : PlanActionProvider
    {
        List<BroadcastAudioArgument> m_actionArgumentList;

        ILogger<BroadcastAudioAction> _logger =Logger.CreateLogger<BroadcastAudioAction>();

        public override void AddPlanActionItem(PlanActionArgument item)
        {
            if (m_actionArgumentList == null)
                m_actionArgumentList = new List<BroadcastAudioArgument>();
            var arg = JsonConvert.DeserializeObject<BroadcastAudioArgument>(item.Argument);
            m_actionArgumentList.Add(arg);
            SoundLightAction.UpdateActionArgument(PlanId, arg); //暂时加上，到时动作可单独运行时，将注释当前动作
        }

        public override void Execute()
        {
            return;
            _logger.LogInformation("执行语言播报联动动作 {0} Begin.....",PlanId);
            try
            {
                //获取声光报警设备，并按组织机构分组
                string url = string.Format("{0}/Resources/IPDevice/systemOptionId={1}", GlobalSetting.AppServerBaseUrl, "A0002016-E009-B019-E001-ABCD11000006");
                IEnumerable<IPDeviceInfo> soundLightDevices = HttpClientHelper.Get<IPDeviceInfo>(url);
                var soundLightDeviceGroup = soundLightDevices.Join(m_actionArgumentList, p => p.IPDeviceInfoId, q => q.SoundLightDeviceId, (p, q) =>
                   new { Device = p, Argument = q }).GroupBy(t => t.Device.OrganizationId);

                ////获取哨位中心服务
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
                        ASCSApi ascs = new ASCSApi(service);
                        foreach (var device in sg)
                        {
                            string audioFile = device.Argument.AudioFile;
                            int sentinelCode = -1;
                            if (PlanTrigger != null)
                            {
                                sentinelCode = PlanTrigger.AlarmSource.IPDeviceCode;//Int32.Parse(PlanTrigger.AlarmSource.Organization.OrganizationCode);
                                if (PlanId.Equals(PlanTrigger.EmergencyPlanId))
                                {
                                    audioFile = string.Format("sound/{0}处置/{1}号哨{2}处置.wav", PlanTrigger.AlarmType.SystemOptionName,
                                       sentinelCode /*PlanTrigger.AlarmSource.IPDeviceCode*/, PlanTrigger.AlarmType.SystemOptionName);
                                }
                                else if (PlanId.Equals(PlanTrigger.BeforePlanId))
                                {
                                    audioFile = string.Format("sound/{0}一级/{1}号哨发生犯人{2}.wav", PlanTrigger.AlarmType.SystemOptionName,
                                       sentinelCode /*PlanTrigger.AlarmSource.IPDeviceCode*/, PlanTrigger.AlarmType.SystemOptionName);
                                }
                            }
                            //audioFile = device.Argument.AudioFile;
                            SoundLightControl action = new SoundLightControl()
                            {
                                DeviceCode = device.Device.IPDeviceCode,
                                Message = string.Empty,
                                Ledbitmask = -1,
                                Audio = audioFile,
                                SentinelCode = sentinelCode
                            };
                            _logger.LogInformation("下发语音播报：{0}", JsonUtility.CamelCaseSerializeObject(action));
                            ascs.SendSoundLightAction(action);
                            _logger.LogInformation("下发语音播报完成");
                        }
                    }
                    else
                    {
                        _logger.LogInformation("找不到设备对应的哨位中心服务，取消播报音频!!!");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("执行语音播报动作异常：{0}\r\n{1}", ex.Message, ex.StackTrace);
            }
            _logger.LogInformation("执行语言播报联动动作 {0} End.....", PlanId);
        }

        public override void Stop()
        {
          
        }
    }
}
