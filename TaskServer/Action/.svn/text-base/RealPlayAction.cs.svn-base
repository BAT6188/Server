using AlarmAndPlan.Model;
using HttpClientEx;
using Infrastructure.Utility;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Surveillance.ViewModel;
using Surveillance.ViewModel.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskServer.Argument;

namespace TaskServer.Action
{
    /// <summary>
    /// 实时视频预览
    /// </summary>
    internal class RealPlayAction : PlanActionProvider
    {
        ILogger<RealPlayAction> _logger = Logger.CreateLogger<RealPlayAction>();

        List<RealVideoPlayArgument> m_actionArgument;

        public  override void AddPlanActionItem(PlanActionArgument argument)
        {
            if (m_actionArgument == null)
                m_actionArgument = new List<RealVideoPlayArgument>();
            RealVideoPlayArgument arg = JsonConvert.DeserializeObject<RealVideoPlayArgument>(argument.Argument);
            m_actionArgument.Add(arg);
        }

        public override void Execute()
        {
            try
            {
                _logger.LogInformation("执行实时视频预览Begin....");
                //string ids = "";
                //m_actionArgument.ForEach(t => ids += "ids[]=" + t.VideoDeviceId + "&");
                //ids = ids.Substring(0, ids.Length - 1);
                string ids = JsonConvert.SerializeObject(m_actionArgument.Select(t => t.VideoDeviceId).ToList());
                string url = string.Format("{0}/Surveillance/Camera/ids={1}", GlobalSetting.AppServerBaseUrl, ids);
                IEnumerable<CameraView> cams = HttpClientHelper.Get<CameraView>(url);
                List<RealPlayParam> playParams = new List<RealPlayParam>();
                m_actionArgument.ForEach(t =>
                {
                    CameraView cam = cams.First(f => f.IPDeviceId.Equals(t.VideoDeviceId));
                    RealPlayParam rvpp = new RealPlayParam() {
                        CameraView = cam,
                        Snapshot = t.ScreenShot,
                        StreamType = (VideoStream)t.StreamType,
                        PresetSiteNo = t.PresetSiteNo
                    };
                    playParams.Add(rvpp);
                });
                MQRealPlayAction playRequest = new MQRealPlayAction() {
                    Cameras = playParams,
                    AlarmLogId = PlanTrigger != null && PlanTrigger.AlarmLogId != null ? PlanTrigger.AlarmLogId.Value : Guid.Empty,
                };
                MQPulish.PublishMessage("ExecuteRealPlayAction", playRequest);
                _logger.LogInformation("执行实时视频预览End....");
            }
            catch (Exception ex)
            {
                _logger.LogError("执行实时视频预览异常：Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
            }
        }

        public override void Stop()
        {
            try
            {
                _logger.LogInformation("停止实时视频预览Begin....");
                MQPulish.PublishMessage("StopRealPlayAction", "");
                _logger.LogInformation("停止实时视频预览End....");
            }
            catch (Exception ex)
            {
                _logger.LogError("执行实时视频预览异常：Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
            }
        }
    }
}
