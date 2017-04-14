using AlarmAndPlan.Model;
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
using System.Threading.Tasks;
using TaskServer.Argument;

namespace TaskServer.Action
{
    /// <summary>
    /// 声光报警动作执行，已过期文字、警灯、语音单独执行
    /// 2016-12-16 考虑到声光报警单独控制的接口还没有，当前还是集中控制
    /// </summary>
    internal class SoundLightAction
    {
        static ILogger<SoundLightAction> _logger = Logger.CreateLogger<SoundLightAction>();

        /// <summary>
        /// 声光报警动作参数，key是预案id
        /// </summary>
        static Dictionary<Guid, List<SoundLightArgument>> SoundLightActionHash = new Dictionary<Guid, List<SoundLightArgument>>();

        /// <summary>
        /// 声光报警控制
        /// </summary>
        static List<SoundLightActionRecord> s_SoundLightActionRecords = new List<SoundLightActionRecord>();

        /// <summary>
        /// 更新语言播报参数
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="argument"></param>
        public static void UpdateActionArgument(Guid planId, BroadcastAudioArgument argument)
        {
            if (!SoundLightActionHash.ContainsKey(planId))
                SoundLightActionHash[planId] = new List<SoundLightArgument>();
            var actions = SoundLightActionHash[planId] as List<SoundLightArgument>;
            var action = actions.FirstOrDefault(t => t.SoundLightDeviceId.Equals(argument.SoundLightDeviceId));
            if (action != null)
            {
                action.AudioAction = true;
                action.AudioFile = argument.AudioFile;
            }
            else
            {
                actions.Add(new SoundLightArgument()
                {
                    AudioAction = true,
                    SoundLightDeviceId = argument.SoundLightDeviceId,
                    AudioFile = argument.AudioFile
                });
            }
        }

        /// <summary>
        /// 更新语言推送参数
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="argument"></param>
        public static void UpdateActionArgument(Guid planId, PushTextArgument argument)
        {
            if (!SoundLightActionHash.ContainsKey(planId))
                SoundLightActionHash[planId] = new List<SoundLightArgument>();
            var actions = SoundLightActionHash[planId] as List<SoundLightArgument>;
            var action = actions.FirstOrDefault(t => t.SoundLightDeviceId.Equals(argument.SoundLightDeviceId));
            if (action != null)
            {
                action.PushMessage = true;
                action.Message = argument.LedText;
            }
            else
            {
                actions.Add(new SoundLightArgument()
                {
                    PushMessage = true,
                    SoundLightDeviceId = argument.SoundLightDeviceId,
                    Message = argument.LedText
                });
            }
        }

        /// <summary>
        /// 更新警灯控制参数
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="argument"></param>
        public static void UpdateActionArgument(Guid planId, TurnonAlarmLightArgument argument)
        {
            if (!SoundLightActionHash.ContainsKey(planId))
                SoundLightActionHash[planId] = new List<SoundLightArgument>();
            var actions = SoundLightActionHash[planId] as List<SoundLightArgument>;
            var action = actions.FirstOrDefault(t => t.SoundLightDeviceId.Equals(argument.SoundLightDeviceId));
            if (action != null)
            {
                action.TurnonLightAction = true;
            }
            else
            {
                actions.Add(new SoundLightArgument()
                {
                    TurnonLightAction = true,
                    SoundLightDeviceId = argument.SoundLightDeviceId,
                });
            }
        }

        ///// <summary>
        /////  执行声光联动动作
        ///// </summary>
        ///// <param name="plan"></param>
        //public static void Execute(Plan plan)
        //{
        //    try
        //    {
        //        var planId = plan.PlanId;
        //        var planTrigger = plan.PlanTrigger;
        //        _logger.LogInformation("执行声光报警联动动作 Begin .....");

        //        RemoveBeforePlanAction(plan);

        //        //获取声光报警设备，并按组织机构分组
        //        if (SoundLightActionHash.ContainsKey(planId))
        //        {
        //            var arguments = SoundLightActionHash[planId] as List<SoundLightArgument>;
        //            if (arguments.Count == 0)
        //                return;
        //            _logger.LogInformation("声光报警器按组织机构分组 Begin .....");
        //            string url = string.Format("{0}/Resources/IPDevice/systemOptionId={1}", GlobalSetting.AppServerBaseUrl, "A0002016-E009-B019-E001-ABCD11000006");
        //            IEnumerable<IPDeviceInfo> soundLightDevices = HttpClientHelper.Get<IPDeviceInfo>(url);
        //            var soundLightDeviceGroup = soundLightDevices.Join(arguments, p => p.IPDeviceInfoId, q => q.SoundLightDeviceId, (p, q) =>new { Device = p, Argument = q }).
        //                GroupBy(t => t.Device.OrganizationId);
        //            _logger.LogInformation("声光报警器按组织机构分组 End .....");

        //            List<SoundLightActionEx> actions = new List<SoundLightActionEx>(); //缓存执行的动作
        //            foreach (var sg in soundLightDeviceGroup)
        //            {
        //                var service = TaskUtility.GetASCService(sg.Key);
        //                if (service != null)
        //                {
        //                    //判断是否有报警
        //                    ASCSApi ascs = new ASCSApi(service);
        //                    foreach (var device in sg)
        //                    {
        //                        string message = "";
        //                        int sentinelCode = -1;
        //                        if (planTrigger != null && planTrigger.AlarmSource != null && planTrigger.AlarmSource.Organization != null)
        //                        {
        //                            Int32.TryParse(planTrigger.AlarmSource.Organization.OrganizationCode, out sentinelCode);
        //                        }

        //                        ///哨位节点名称
        //                        //推送文字
        //                        if (device.Argument.PushMessage)
        //                        {
        //                            if (!string.IsNullOrEmpty(device.Argument.Message))
        //                                message = device.Argument.Message;
        //                            else 
        //                            {
        //                                if (planTrigger != null)
        //                                {
        //                                    string sentinelNodeName = planTrigger.AlarmSource.Organization.OrganizationShortName;
        //                                    message = GetPushText(planId, planTrigger, sentinelNodeName);
        //                                }
        //                            }
        //                        }
        //                        _logger.LogInformation("推送消息内容[{0}]到{1}", message, device.Device.IPDeviceName);

        //                        //开启警灯
        //                        int ledbitmask = 0;
        //                        if (device.Argument.TurnonLightAction)
        //                        {
        //                            if (planTrigger != null) 
        //                            {
        //                                var soundlightCfg = TaskUtility.SoundlightConfigList.FirstOrDefault(t => t.AlarmTypeId.Equals(planTrigger.AlarmType.SystemOptionId));
        //                                if (soundlightCfg != null)
        //                                    ledbitmask = soundlightCfg.Ledbitmask;
        //                            }
        //                            else
        //                                ledbitmask = 0x80;
        //                        }
        //                        //语音推送
        //                        string audioFile = "";
        //                        if (device.Argument.AudioAction)
        //                        {
        //                            audioFile = device.Argument.AudioFile;
        //                            if (planTrigger != null)
        //                                audioFile = GetAudioFile(planId, planTrigger);
        //                        }

        //                        SoundLightControl startAction = new SoundLightControl()
        //                        {
        //                            DeviceCode = device.Device.IPDeviceCode,
        //                            Message = message,
        //                            Ledbitmask = ledbitmask,
        //                            Audio = audioFile,
        //                            SentinelCode = sentinelCode //planTrigger.AlarmSource.IPDeviceCode
        //                        };
        //                        _logger.LogInformation("下发声光报警联动动作： Begin");
        //                        ascs.SendSoundLightAction(startAction);
        //                        _logger.LogInformation("启动动作 \r\n:{0}", JsonUtility.CamelCaseSerializeObject(startAction));
        //                        actions.Add(new SoundLightActionEx() {
        //                            Ascs = service,
        //                            SoundLightControl = startAction
        //                        });
        //                        _logger.LogInformation("下发声光报警联动动作： End ");
        //                    }
        //                }
        //                else
        //                    _logger.LogInformation("未配置哨位中心服务，取消声光报警联动");
        //            }
        //            //保存执行动作
        //            s_SoundLightActionRecords.Add(new SoundLightActionRecord()
        //            {
        //                ActionId = plan.PlanTrigger != null ? plan.PlanTrigger.AlarmLogId.Value : plan.PlanId,
        //                Actions = actions
        //            });
        //            _logger.LogInformation("执行声光报警联动动作 End .....");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("执行声光报警动作异常：{0}\r\n{1}", ex.Message, ex.StackTrace);
        //    }
        //}

        /// <summary>
        ///  执行声光联动动作
        /// </summary>
        /// <param name="plan"></param>
        public static void Execute(Plan plan)
        {
            try
            {
                var planId = plan.PlanId;
                var planTrigger = plan.PlanTrigger;
                _logger.LogInformation("执行声光报警联动动作 Begin .....");

                RemoveBeforePlanAction(plan);

                //获取声光报警设备，并按组织机构分组
                if (SoundLightActionHash.ContainsKey(planId))
                {
                    var arguments = SoundLightActionHash[planId] as List<SoundLightArgument>;
                    if (arguments.Count > 0)
                    {
                        _logger.LogInformation("获取声光报警器设备信息 Begin .....");
                        IEnumerable<IPDeviceInfo> soundLightDevices = soundLightDevices = TaskUtility.s_cacheDeviceList;
                        if (soundLightDevices == null || soundLightDevices.Count() == 0)
                        {
                            string url = string.Format("{0}/Resources/IPDevice/systemOptionId={1}", GlobalSetting.AppServerBaseUrl, "A0002016-E009-B019-E001-ABCD11000006");
                            soundLightDevices = HttpClientHelper.Get<IPDeviceInfo>(url);
                        }
                        var soundLightDeviceViewList = soundLightDevices.Join(arguments, p => p.IPDeviceInfoId, q => q.SoundLightDeviceId, (p, q) =>
                            new { Device = p, Argument = q }).ToList();
                        _logger.LogInformation("获取声光报警器设备信息 End .....");

                        List<SoundLightActionEx> actions = new List<SoundLightActionEx>(); //缓存执行的动作
                        var service = TaskUtility.GetDefaultASCService();
                        if (service != null)
                        {
                            ASCSApi ascs = new ASCSApi(service);
                            int sentinelCode = -1;
                            if (planTrigger != null && planTrigger.AlarmSource != null && planTrigger.AlarmSource.Organization != null)
                                Int32.TryParse(planTrigger.AlarmSource.Organization.OrganizationCode, out sentinelCode);
                            foreach (var device in soundLightDeviceViewList)
                            {
                                string message = "";
                                int ledbitmask = 0;
                                string audioFile = "";
                                if (planTrigger != null)
                                {
                                    //推送文字
                                    if (!string.IsNullOrEmpty(device.Argument.Message))
                                        message = device.Argument.Message;
                                    else
                                    {
                                        string sentinelNodeName = planTrigger.AlarmSource.Organization.OrganizationShortName;
                                        message = GetPushText(planId, planTrigger, sentinelNodeName);
                                    }
                                    //警灯
                                    var soundlightCfg = TaskUtility.SoundlightConfigList.FirstOrDefault(t => t.AlarmTypeId.Equals(planTrigger.AlarmType.SystemOptionId));
                                    if (soundlightCfg != null)
                                        ledbitmask = soundlightCfg.Ledbitmask;
                                    else
                                        ledbitmask = 0x80; //其他报警打开所有灯
                                    //播报语音
                                    audioFile = GetAudioFile(planId, planTrigger);
                                }
                                else
                                {
                                    ledbitmask = 0x80;
                                    audioFile = device.Argument.AudioFile;
                                }

                                _logger.LogInformation("Before :audio:{0}", audioFile);
                                SoundLightControl startAction = new SoundLightControl()
                                {
                                    DeviceCode = device.Device.IPDeviceCode,
                                    Message = device.Argument.PushMessage ? message : string.Empty,
                                    Ledbitmask = device.Argument.TurnonLightAction ? ledbitmask : 0,
                                    Audio = device.Argument.AudioAction ? audioFile : string.Empty,
                                    SentinelCode = sentinelCode
                                };

                                _logger.LogInformation("下发声光报警联动动作： Begin");
                                ascs.SendSoundLightAction(startAction);
                                actions.Add(new SoundLightActionEx()
                                {
                                    Ascs = service,
                                    SoundLightControl = startAction
                                });
                                _logger.LogInformation("下发声光报警联动动作： End ");
                            }
                            //保存执行动作
                            s_SoundLightActionRecords.Add(new SoundLightActionRecord()
                            {
                                ActionId = plan.PlanTrigger != null ? plan.PlanTrigger.AlarmLogId.Value : plan.PlanId,
                                Actions = actions
                            });
                        }
                        else
                            _logger.LogInformation("未配置哨位中心服务，取消声光报警联动");
                    }
                }
                _logger.LogInformation("执行声光报警联动动作 End .....");
            }
            catch (Exception ex)
            {
                _logger.LogError("执行声光报警动作异常：{0}\r\n{1}", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 移除报警确认前动作
        /// </summary>
        /// <param name="plan"></param>
        private static void RemoveBeforePlanAction(Plan plan)
        {
            var actionId = plan.PlanTrigger != null ? plan.PlanTrigger.AlarmLogId.Value : plan.PlanId;
            var outTimeAction = s_SoundLightActionRecords.FirstOrDefault(t => t.ActionId.Equals(actionId));
            if (outTimeAction != null)
            {
                s_SoundLightActionRecords.Remove(outTimeAction);
                _logger.LogInformation("移除声光报警记录");
            }
        }

        /// <summary>
        /// led推送文字
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="planTrigger"></param>
        /// <param name="sentinelNodeName">哨位节点名称</param>
        /// <returns></returns>
        static string GetPushText(Guid planId, PlanTriggerSource planTrigger, string sentinelNodeName)
        {
            string message = string.Empty;
            string alarmTypeName = planTrigger.AlarmType.SystemOptionName;
            var alarmTextObj = TaskUtility.SoundlightConfigList.FirstOrDefault(t => t.AlarmTypeId.Equals(planTrigger.AlarmType.SystemOptionId));
            var alarmdesc = alarmTextObj != null ? alarmTextObj.Description : alarmTypeName;
            if (planId.Equals(planTrigger.BeforePlanId))
                message = string.Format("{0}{1},各小组请注意观察!", sentinelNodeName, alarmdesc);
            else if (planId.Equals(planTrigger.EmergencyPlanId))
                message = string.Format("{0}{1},各小组按预案处置!", sentinelNodeName,alarmdesc);
            return message;
        }

        /// <summary>
        /// 获取语音播报文件
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="planTrigger"></param>
        /// <returns></returns>
        static string GetAudioFile(Guid planId, PlanTriggerSource planTrigger)
        {
            string audioFile = string.Empty;

            var alarmTextObj = TaskUtility.SoundlightConfigList.FirstOrDefault(t => t.AlarmTypeId.Equals(planTrigger.AlarmType.SystemOptionId));
            //非哨位报警
            if (alarmTextObj == null)
            {
                audioFile = "sound/警报音.wav";
            }
            else
            {
                string alarmTypeName = planTrigger.AlarmType.SystemOptionName;
                if ("越狱".Equals(alarmTypeName))
                    alarmTypeName = "逃跑";
                if (planId.Equals(planTrigger.EmergencyPlanId))
                {
                    audioFile = string.Format("sound/{0}处置/{1}号哨{2}处置.wav", alarmTypeName,
                        planTrigger.AlarmSource.IPDeviceCode, alarmTypeName);
                }
                else if (planId.Equals(planTrigger.BeforePlanId))
                {
                    var alarmdesc = alarmTextObj != null ? alarmTextObj.Description : "";
                    audioFile = string.Format("sound/{0}一级/{1}号哨{2}.wav", alarmTypeName,
                       planTrigger.AlarmSource.IPDeviceCode, alarmdesc);
                }
            }
            return audioFile;
        }

        /// <summary>
        /// 清除声光联动动作参数
        /// </summary>
        /// <param name="planId"></param>
        public static void RemoveActionArgument(Guid planId)
        {
            if (SoundLightActionHash.ContainsKey(planId))
            {
                SoundLightActionHash[planId] = new List<SoundLightArgument>();
            }
        }

        /// <summary>
        /// 停止声光报警动作
        /// 停止当前预案动作，然后恢复上一预案动作
        /// </summary>
        /// <param name="plan"></param>
        public static void Stop(Plan plan)
        {
            var actionId = plan.PlanTrigger != null ? plan.PlanTrigger.AlarmLogId.Value : plan.PlanId;
            var outTimeAction = s_SoundLightActionRecords.FirstOrDefault(t => t.ActionId.Equals(actionId));
            if (outTimeAction != null && outTimeAction.Actions != null && outTimeAction.Actions.Count > 0)
            {
                _logger.LogInformation("停止声光报警动作 Begin...");
                ASCSApi api = null;
                List<int> soundLightDeviceCode = new List<int>();
                foreach (var action in outTimeAction.Actions)
                {
                    action.SoundLightControl.Audio = string.Empty;
                    action.SoundLightControl.Ledbitmask = 0;
                    action.SoundLightControl.Message = string.Empty;
                    api = new ASCSApi(action.Ascs);
                    _logger.LogInformation("下发停止声光报警联动协议： \r\n:{0}", JsonUtility.CamelCaseSerializeObject(action.SoundLightControl));
                    api.SendSoundLightAction(action.SoundLightControl);
                    soundLightDeviceCode.Add(action.SoundLightControl.DeviceCode);
                }
                LedDisplay ledDisopay = new LedDisplay()
                {
                    Flag = 2,
                    Text = string.Empty,
                    TerminalCode = soundLightDeviceCode.ToArray()
                };
                _logger.LogInformation("下发恢复声光报警时间显示： \r\n:{0}", JsonUtility.CamelCaseSerializeObject(ledDisopay));
                api.ControLedDisplayl(ledDisopay);
                s_SoundLightActionRecords.Remove(outTimeAction);
                _logger.LogInformation("停止声光报警动作 End...");

                //执行下动作
                if (s_SoundLightActionRecords.Count > 0)
                {
                    _logger.LogInformation("恢复播放上一声光报警动作 begin！！");
                    var record = s_SoundLightActionRecords.Last();
                    foreach (var action in record.Actions)
                    {
                        api = new ASCSApi(action.Ascs);
                        api.SendSoundLightAction(action.SoundLightControl);
                    }
                    _logger.LogInformation("恢复播放上一声光报警动作 end！！");
                }
                else
                {
                    _logger.LogInformation("当前没有要恢复的声光报警动作");
                }
            }
        }
    }
}
