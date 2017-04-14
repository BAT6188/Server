using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HttpClientEx;
using PAPS.Data;
using Resources.Model;
using System.Text;
using Microsoft.Extensions.Logging;
using Infrastructure.Utility;
using Newtonsoft.Json;

namespace HttpClientEx
{
    /// <summary>
    /// 哨位中心服务器api
    /// </summary>
    public class ASCSApi
    {
        ILogger<ASCSApi> _logger = Logger.CreateLogger<ASCSApi>();

        public ASCSApi(ServiceInfo ascs)
        {
            ASCS = ascs;
            if (ASCS == null)
                throw new ArgumentNullException("哨位中心服务不能为null！");
        }

        /// <summary>
        /// 哨位中心服务信息
        /// </summary>
        private ServiceInfo ASCS { get; set; }

        /// <summary>
        /// 确认/取消哨位报警
        /// </summary>
        /// <returns></returns>
        public HttpRequestResult SendDeviceAlarmStatus(DeviceAlarmStatus status)
        {
            string url = string.Format(@"http://{0}:{1}/ASCS/SentinelAlarmStatus", ASCS.EndPoints[0].IPAddress, ASCS.EndPoints[0].Port);
            _logger.LogInformation("发送报警状态确认 Begin.\r\nUrl:{0},\r\n Protocol:{1}...", url, JsonUtility.CamelCaseSerializeObject(status));
            var result = HttpClientHelper.Put(status, url);
            _logger.LogInformation("发送报警状态确认 End,retur {0}...", result);
            return result;
        }

        //public bool SendDefenseAlarmStatus(DefenceAlarmStatus status)
        //{
        //    if (ASCS == null)
        //        throw new Exception("请配置哨位中心服务！");
        //    string url = string.Format(@"http://{0}:{1}/ASCS/DefenceAlarmStatus", ASCS.EndPoints[0].IPAddress, ASCS.EndPoints[0].Port);
        //    string result = string.Empty;
        //    return HttpClientHelper.Put(status, url, ref result);
        //}

        /// <summary>
        /// 报警复位
        /// </summary>
        /// <returns></returns>
        public HttpRequestResult ResetAlarm()
        {
            //if (ASCS == null)
            //    throw new Exception("请配置哨位中心服务！");
            string url = string.Format(@"http://{0}:{1}/ASCS/ResetAlarm", ASCS.EndPoints[0].IPAddress, ASCS.EndPoints[0].Port);
            _logger.LogInformation("发送报警复位Begin...\r\nUrl:{0}",url);
            string result = string.Empty;
            bool success = HttpClientHelper.Delete(url, ref result);
            _logger.LogInformation("发送报警复位End,Return {0}...", success);
            return new HttpRequestResult() { Success=success, ResultText=result};
        }

        /// <summary>
        /// 子弹箱控制
        /// </summary>
        /// <param name="cartidges"></param>
        /// <returns></returns>
        public HttpRequestResult CatridgeBoxStatus(List<CartidgeBoxStatus> cartidges)
        {
            //if (ASCS == null)
            //    throw new Exception("请配置哨位中心服务！");
            string url = string.Format(@"http://{0}:{1}/ASCS/BulletBoxStatus", ASCS.EndPoints[0].IPAddress, ASCS.EndPoints[0].Port);
            _logger.LogInformation("子弹箱控制 Begin, \r\nUrl:{0}\r\nProtocol:{1}...", url, JsonUtility.CamelCaseSerializeObject(cartidges));
            var result = HttpClientHelper.Post<List<CartidgeBoxStatus>>(cartidges, url);
            _logger.LogInformation("子弹箱控制 End,Return {0}", result);
            return result;
        }

        /// <summary>
        /// 子弹箱灯控制
        /// </summary>
        /// <param name="cartidges"></param>
        /// <returns></returns>
        public HttpRequestResult CartidgeLightStatus(List<CatridgeLightStatus> cartidges)
        {
            //if (ASCS == null)
            //    throw new Exception("请配置哨位中心服务！");
            string url = string.Format(@"http://{0}:{1}/ASCS/BulletLedStatus", ASCS.EndPoints[0].IPAddress, ASCS.EndPoints[0].Port);
            _logger.LogInformation("子弹箱灯控制 Begin, \r\nUrl:{0}\r\nProtocol:{1}...", url, JsonUtility.CamelCaseSerializeObject(cartidges));
            var result = HttpClientHelper.Post<List<CatridgeLightStatus>>(cartidges, url);
            _logger.LogInformation("子弹箱灯控制 End,Return {0}", result);
            return result;
        }

        /// <summary>
        /// 消息发布
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public HttpRequestResult PushMessage(PushMessage message)
        {
            //if (ASCS == null)
            //    throw new Exception("请配置哨位中心服务！");
            string url = string.Format(@"http://{0}:{1}/ASCS/SendMsgToDevice", ASCS.EndPoints[0].IPAddress, ASCS.EndPoints[0].Port);
            _logger.LogInformation("发送LED文字消息 Begin, \r\nUrl:{0}\r\nProtocol:{1}...", url, JsonUtility.CamelCaseSerializeObject(message));
            var result = HttpClientHelper.Post<PushMessage>(message, url);
            _logger.LogInformation("发送LED文字消息 End...，return {0}", result);
            return result;
        }

        /// <summary>
        /// •	更新哨位台输出端口状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public HttpRequestResult SendSentinelOutputStatus(List<SentinelOutputStatus> status)
        {
            //if (ASCS == null)
            //    throw new Exception("请配置哨位中心服务！");
            string url = string.Format(@"http://{0}:{1}/ASCS/SentinelOutputStatus", ASCS.EndPoints[0].IPAddress, ASCS.EndPoints[0].Port);
            var result = HttpClientHelper.Post<List<SentinelOutputStatus>>(status, url);
            return result;
        }

        ///// <summary>
        /////  声光报警器LED灯闪烁状态
        ///// </summary>
        ///// <param name="status"></param>
        ///// <returns></returns>
        //public bool SendLedLightStatus(List<LedLightStatus> status)
        //{
        //    if (ASCS == null)
        //        throw new Exception("请配置哨位中心服务！");
        //    string url = string.Format(@"http://{0}:{1}/ASCS/LedLightStatus", ASCS.EndPoints[0].IPAddress, ASCS.EndPoints[0].Port);
        //    string result = string.Empty;
        //    return HttpClientHelper.Post<List<LedLightStatus>>(status, url, ref result);
        //}

        /// <summary>
        /// 查询哨位版本信息
        /// </summary>
        /// <param name="deviceCode"></param>
        /// <returns></returns>
        public DeviceVersion GetDeviceVersion(int deviceCode)
        {
            //if (ASCS == null)
            //    throw new Exception("请配置哨位中心服务！");
            string url = string.Format(@"http://{0}:{1}/ASCS/SentinelVersion?SentinelCode={2}", ASCS.EndPoints[0].IPAddress, ASCS.EndPoints[0].Port, deviceCode);
            string result = string.Empty;
            return HttpClientHelper.GetOne<DeviceVersion>(url);
        }

        /// <summary>
        /// 查询设备诊断
        /// </summary>
        /// <param name="deviceCode"></param>
        /// <returns></returns>
        public DeviceDiagnosis GetDeviceDiagnosis(int deviceCode)
        {
            //if (ASCS == null)
            //    throw new Exception("请配置哨位中心服务！");
            string url = string.Format(@"http://{0}:{1}/ASCS/SentinelDiagnosis?SentinelCode={2}", ASCS.EndPoints[0].IPAddress, ASCS.EndPoints[0].Port, deviceCode);
            string result = string.Empty;
            return HttpClientHelper.GetOne<DeviceDiagnosis>(url);
        }

        /// <summary>
        /// 分发指纹
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public HttpRequestResult DispatchFinger(DispatchFigureInfo info)
        {
            //if (ASCS == null)
            //    throw new Exception("请配置哨位中心服务！");
            string url = string.Format(@"http://{0}:{1}/ASCS/FingerDispatch", ASCS.EndPoints[0].IPAddress, ASCS.EndPoints[0].Port);
            _logger.LogInformation("指纹下发 Begin, \r\nUrl:{0}\r\nProtocol:{1}...", url, JsonUtility.CamelCaseSerializeObject(info));
            var result  = HttpClientHelper.Post<DispatchFigureInfo>(info, url);
            _logger.LogInformation("指纹下发 End,Result:{0}", result);
            return result;
        }

        /// <summary>
        /// 清除指纹
        /// </summary>
        /// <param name="sentinelCode"></param>
        /// <param name="figureCode">指纹编码 备注（0：清除本哨位台上所有人员指纹）</param>
        /// <returns></returns>
        public HttpRequestResult CleanFinger(int sentinelCode, int figureCode)
        {
            //if (ASCS == null)
            //    throw new Exception("请配置哨位中心服务！");
            string url = string.Format(@"http://{0}:{1}/ASCS/FingerClean?SentinelCode={2}&FingerCode={3}",
                ASCS.EndPoints[0].IPAddress, ASCS.EndPoints[0].Port, sentinelCode, figureCode);
            _logger.LogInformation("指纹清除 Begin, \r\nUrl:{0}", url);
            string result = string.Empty;
            bool success = HttpClientHelper.Delete(url, ref result);
            _logger.LogInformation("指纹清除 End");
            return new HttpRequestResult() { Success = success, ResultText = result };
        }

        /// <summary>
        /// 哨位时间同步
        /// </summary>
        /// <param name="sentinelCode"></param>
        /// <returns></returns>
        public HttpRequestResult SynTimer(int[] sentinelCode)
        {
            //if (ASCS == null)
            //    throw new Exception("请配置哨位中心服务！");
            string url = string.Format(@"http://{0}:{1}/ASCS/Syntimer",
                ASCS.EndPoints[0].IPAddress, ASCS.EndPoints[0].Port);
            var result = HttpClientHelper.Post<int[]>(sentinelCode, url);
            return result;
        }

        /// <summary>
        /// 发起对讲
        /// </summary>
        /// <param name="talk"></param>
        /// <returns></returns>
        public HttpRequestResult StartVoipTalk(VoipTalk talk)
        {
            //if (ASCS == null)
            //    throw new Exception("请配置哨位中心服务！");
            string url = string.Format(@"http://{0}:{1}/ASCS/StartCall",
                ASCS.EndPoints[0].IPAddress, ASCS.EndPoints[0].Port);
            var result = HttpClientHelper.Post<VoipTalk>(talk, url);
            return result;
        }

        /// <summary>
        /// 终止对讲
        /// </summary>
        /// <param name="Caller"></param>
        /// <returns></returns>
        public HttpRequestResult StopVoipTalk(int Caller)
        {
            //if (ASCS == null)
            //    throw new Exception("请配置哨位中心服务！");
            string url = string.Format(@"http://{0}:{1}/ASCS/StopCall?Caller={2}",
                ASCS.EndPoints[0].IPAddress, ASCS.EndPoints[0].Port, Caller);
            string result = string.Empty;
            bool success = HttpClientHelper.Delete(url, ref result);
            return new HttpRequestResult() { Success = success, ResultText = result };
        }

        /// <summary>
        /// Voip 群呼
        /// </summary>
        /// <param name="caller">群呼号</param>
        /// <returns></returns>
        public bool Conference(int[] caller)
        {
            //if (ASCS == null)
            //    throw new Exception("请配置哨位中心服务！");
            string url = string.Format(@"http://{0}:{1}/ASCS/Conference",
                ASCS.EndPoints[0].IPAddress, ASCS.EndPoints[0].Port);
            return HttpClientHelper.Post<int[]>(caller, url).Success;
        }

        /// <summary>
        /// 设备信息变更通知
        /// </summary>
        /// <param name="caller">群呼号</param>
        /// <returns></returns>
        public HttpRequestResult NotifyDeviceInfoChange(DeviceInfoChange notify)
        {
            //if (ASCS == null)
            //    throw new Exception("请配置哨位中心服务！");
            _logger.LogInformation("设备消息变化通知Begin, Protocol:\r\n{0}", JsonUtility.CamelCaseSerializeObject(notify));
            string url = string.Format(@"http://{0}:{1}/ASCS/DeviceInfo",
                ASCS.EndPoints[0].IPAddress, ASCS.EndPoints[0].Port);
            var result = HttpClientHelper.Post<DeviceInfoChange>(notify, url);
            _logger.LogInformation("设备消息变化通知End...");
            return result;
        }

        /// <summary>
        /// 地图数据同步
        /// </summary>
        /// <param name="mapInfo"></param>
        /// <returns></returns>
        public HttpRequestResult SyncSentinelMap(SentinelMapInfo mapInfo)
        {
            //if (ASCS == null)
            //    throw new Exception("请配置哨位中心服务！");
            string url = string.Format(@"http://{0}:{1}/ASCS/MapInfo",
                ASCS.EndPoints[0].IPAddress, ASCS.EndPoints[0].Port);
            _logger.LogInformation("哨位地图同步Begin.\r\nUrl:{0}.\r\nProtocol:{1}", url, JsonUtility.CamelCaseSerializeObject(mapInfo));
            var result = HttpClientHelper.Post<SentinelMapInfo>(mapInfo, url, Encoding.GetEncoding("gb2312"));
            _logger.LogInformation("哨位地图同步End ,return {0}", result);
            return result;
        }

        /// <summary>
        /// 喊话控制
        /// </summary>
        /// <param name="speakInfo"></param>
        /// <returns></returns>
        public HttpRequestResult Shout(SentinelShout speakInfo)
        {
            //if (ASCS == null)
            //    throw new Exception("请配置哨位中心服务！");
            string url = string.Format(@"http://{0}:{1}/ASCS/LinkageSpeak",
                ASCS.EndPoints[0].IPAddress, ASCS.EndPoints[0].Port);
            var result = HttpClientHelper.Put<SentinelShout>(speakInfo, url);
            return result;
        }

        /// <summary>
        /// 声光报警联动动作控制
        /// </summary>
        /// <param name="ledControl"></param>
        /// <returns></returns>
        public HttpRequestResult SendSoundLightAction(SoundLightControl ledControl)
        {
            //if (ASCS == null)
            //    throw new Exception("请配置哨位中心服务！");
            string url = string.Format(@"http://{0}:{1}/ASCS/LinkageLedSound",
                ASCS.EndPoints[0].IPAddress, ASCS.EndPoints[0].Port);
            var result =  HttpClientHelper.Post<SoundLightControl>(ledControl, url);
            return result;
        }

        /// <summary>
        /// 终端设备升级
        /// </summary>
        /// <returns></returns>
        public bool UpgradeDevice()
        {
            return true;
        }

        /// <summary>
        /// 防瞌睡设置
        /// </summary>
        /// <returns></returns>
        public bool PreventDoze()
        {
            return true;
        }

        /// <summary>
        /// LED 文字显示控制
        /// </summary>
        /// <param name="ledDisplay"></param>
        /// <returns></returns>
        public HttpRequestResult ControLedDisplayl(LedDisplay ledDisplay)
        {
            //if (ASCS == null)
            //    throw new Exception("请配置哨位中心服务！");
            try
            {
                string url = string.Format(@"http://{0}:{1}/ASCS/LedDisplay  ",
                    ASCS.EndPoints[0].IPAddress, ASCS.EndPoints[0].Port);
                _logger.LogInformation("LED文字显示控制 Begin.\r\nUrl:{0}.\r\nProtocol:{1}", url, JsonUtility.CamelCaseSerializeObject(ledDisplay));
                var result = HttpClientHelper.Post<LedDisplay>(ledDisplay, url, Encoding.GetEncoding("gb2312"));
                _logger.LogInformation("LED文字显示控制 End ,return {0}", result);
                return result;
            }
            catch (Exception ex)
            {
                return new HttpRequestResult() { Success = false, ResultText = ex.Message };
            }
        }
    }
}
