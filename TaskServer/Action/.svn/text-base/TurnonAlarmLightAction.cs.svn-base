using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskServer.Argument;
using AlarmAndPlan.Model;
using PAPS.Data;
using HttpClientEx;
using Resources.Model;
using Microsoft.Extensions.Logging;
using Infrastructure.Utility;
using Infrastructure.Model;

namespace TaskServer.Action
{
    /// <summary>
    /// 开启警灯动作 
    /// 
    /// </summary>
    class TurnonAlarmLightAction : PlanActionProvider
    {
        static Dictionary<Guid, int> AlarmLightDictionry = new Dictionary<Guid, int>();

        ILogger<TurnonAlarmLightAction> _logger = Logger.CreateLogger<TurnonAlarmLightAction>();

        static TurnonAlarmLightAction()
        {
            AlarmLightDictionry.Add(Guid.Parse("A0002016-E009-B019-E001-ABCD00002001"), 0x08);
            AlarmLightDictionry.Add(Guid.Parse("A0002016-E009-B019-E001-ABCD00002002"), 0x04);
            AlarmLightDictionry.Add(Guid.Parse("A0002016-E009-B019-E001-ABCD00002003"), 0x10);
            AlarmLightDictionry.Add(Guid.Parse("A0002016-E009-B019-E001-ABCD00002004"), 0x20);
            AlarmLightDictionry.Add(Guid.Parse("A0002016-E009-B019-E001-ABCD00002005"), 0x04);
            AlarmLightDictionry.Add(Guid.Parse("A0002016-E009-B019-E001-ABCD00002006"), 0x08);
        }

        List<TurnonAlarmLightArgument> m_actionArgumentList;

        public override void AddPlanActionItem(PlanActionArgument item)
        {
            if (m_actionArgumentList == null)
                m_actionArgumentList = new List<TurnonAlarmLightArgument>();
            //m_actionArgumentList.Add(JsonConvert.DeserializeObject<TurnonAlarmLightArgument>(item.Argument));

            var arg = JsonConvert.DeserializeObject<TurnonAlarmLightArgument>(item.Argument);
            SoundLightAction.UpdateActionArgument(PlanId, arg); //暂时加上，到时动作可单独运行时，将注释当前动作
        }

        public override void Execute()
        {
            return;
            try
            {
                _logger.LogInformation("执行开启警灯动作 {0} Begin....",PlanId);
                //获取声光报警设备，并按组织机构分组
                string url = string.Format("{0}/Resources/IPDevice/systemOptionId={1}", GlobalSetting.AppServerBaseUrl, "A0002016-E009-B019-E001-ABCD11000006");
                IEnumerable<IPDeviceInfo> soundLightDevices = HttpClientHelper.Get<IPDeviceInfo>(url);
                var soundLightDeviceGroup = soundLightDevices.Join(m_actionArgumentList, p => p.IPDeviceInfoId, q => q.SoundLightDeviceId, (p, q) =>
                   new { Device = p, Argument = q }).GroupBy(t => t.Device.OrganizationId);

                foreach (var sg in soundLightDeviceGroup)
                {
                    var service = TaskUtility.GetASCService(sg.Key);
                    if (service != null)
                    {
                        //判断是否有报警
                        ASCSApi ascs = new ASCSApi(service);
                        foreach (var device in sg)
                        {
                            int ledbitmask = 0x80;
                            var sentinelCode = -1;
                            if (PlanTrigger != null)
                            {
                                if (AlarmLightDictionry.ContainsKey(PlanTrigger.AlarmType.SystemOptionId))
                                    ledbitmask = AlarmLightDictionry[PlanTrigger.AlarmType.SystemOptionId];
                            }
                            SoundLightControl action = new SoundLightControl()
                            {
                                DeviceCode = device.Device.IPDeviceCode,
                                Message = string.Empty,
                                Ledbitmask = ledbitmask,
                                Audio = string.Empty,
                                SentinelCode = sentinelCode //PlanTrigger.AlarmSource.IPDeviceCode
                            };
                            ascs.SendSoundLightAction(action);
                        }
                    }
                    else
                        _logger.LogInformation("取消开启警灯动作执行，找不到设备对应的哨位中心服务!!!");
                }
                _logger.LogInformation("执行开启警灯动作 {0} End ....", PlanId);
            }
            catch (Exception ex)
            {
                _logger.LogError("执行开启警灯报动作异常：{0}\r\n{1}", ex.Message, ex.StackTrace);
            }
        }

        public override void Stop()
        {
            _logger.LogInformation("关闭警灯动作执行!!!");

        }

    }
}
