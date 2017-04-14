using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskServer.Argument;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Surveillance.ViewModel;
using HttpClientEx;
using Infrastructure.Model;
using AlarmAndPlan.Model;
using Infrastructure.Utility;

namespace TaskServer.Action
{
    /// <summary>
    /// 执行视频上墙动作
    /// </summary>
    internal sealed class TvAction : PlanActionProvider
    {
        ILogger<TvAction> _logger = Logger.CreateLogger<TvAction>();

        List<TvVideoPlayArgument> m_actionArgumentList;

        List<TVCameraView> m_actionCameraList = new List<TVCameraView>();

        public VideoRoundTask VideoRoundTask = null;

        /// <summary>
        /// 缓存的上墙轮巡场景，暂时先放着缓存队列，后续优化PlanActionProvider，增加任务暂停和恢复功能，任务的执行要有状态/优先级跟踪
        /// </summary>
        static List<TvAction> s_cacheTvAction = new List<TvAction>(); 

        public override void AddPlanActionItem(PlanActionArgument item)
        {
            if (m_actionArgumentList == null)
                m_actionArgumentList = new List<TvVideoPlayArgument>();
            m_actionArgumentList.Add(JsonConvert.DeserializeObject<TvVideoPlayArgument>(item.Argument));
        }

        /// <summary>
        /// 临时加上，后续调整
        /// </summary>
        /// <param name="item"></param>
        public  void AddPlanActionItem(TvVideoPlayArgument item)
        {
            if (m_actionArgumentList == null)
                m_actionArgumentList = new List<TvVideoPlayArgument>();
            m_actionArgumentList.Add(item);
        }

        public override void Execute()
        {
            try
            {
                //视频上墙参数整理
                _logger.LogInformation("执行视频上墙动作 Begin....");
                if (m_actionArgumentList != null && m_actionArgumentList.Count > 0)
                {
                    //获取上墙场景数据
                    var videoRoundSceneId = m_actionArgumentList.FirstOrDefault().VideoRoundSceneId;
                    string url = string.Format("{0}/Resources/VideoRoundScene/View/videoRoundSceneId={1}", GlobalSetting.AppServerBaseUrl, 
                        videoRoundSceneId);
                    VideoRoundSceneView sceneView = HttpClientHelper.GetOne<VideoRoundSceneView>(url);
                    if (sceneView != null)
                    {
                        Resources.Model.ServiceInfo dvm = TaskUtility.GetDefaultDMCService();
                        if (dvm != null)
                        {
                            m_TvActionApi = new TvActionApi(dvm);
                            VideoRoundTask = new VideoRoundTask(sceneView);
                            VideoRoundTask.ExecuteRoundAction += ExecuteRoundActionInvoke;
                            //判断是否为报警预案
                            bool executed = false;
                            if (PlanTrigger != null)
                            {
                                RemoveBeforePlanAction();
                                _logger.LogInformation("报警联动预案，优先响应！");
                                executed = true;
                            }
                            else //普通预案启动
                            {
                                //判断是否有报警预案启动，有则不启动
                                var alarmTvAction = s_cacheTvAction.FirstOrDefault(t => t.PlanTrigger != null && t.VideoRoundTask != null
                                    && t.VideoRoundTask.RoundStatus == RoundStatus.Running);
                                if (alarmTvAction != null)
                                    _logger.LogInformation("当前有报警预案正在联动执行，将上墙动作执行缓存到列表");
                                else
                                    executed = true;
                            }
                            //暂停所有其他视频上墙
                            if (executed)
                            {
                                s_cacheTvAction.ForEach(t =>
                                {
                                    if (t.VideoRoundTask != null && t.VideoRoundTask.RoundStatus == RoundStatus.Running)
                                        t.VideoRoundTask.Pause();
                                });
                                _logger.LogInformation("满足条件，执行视频上墙动作");
                                VideoRoundTask.Start();
                            }
                            s_cacheTvAction.Add(this);
                        }
                        else
                        {
                            _logger.LogInformation("没有配置矩阵服务器，取消视频上墙动作");
                        }
                    }
                    else
                    {
                        _logger.LogInformation("没有找到预案场景！！！");
                    }
                }
                _logger.LogInformation("执行视频上墙动作 End....");
            }
            catch (Exception ex)
            {
                _logger.LogError("执行实时视频预览异常：Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
            }
        }

        TvActionApi m_TvActionApi = null;

        /// <summary>
        /// 轮巡动作执行
        /// </summary>
        /// <param name="section"></param>
        private  void ExecuteRoundActionInvoke(VideoRoundSectionView section)
        {
            int cameraIndex = 0;
            _logger.LogInformation("执行视频上墙轮巡动作");
            foreach (var camera in section.PlayInfoList)
            {
                TVCameraView tvPlayParam = new TVCameraView()
                {
                    CameraView = camera.CameraView,
                    MonitorView = section.Monitors[cameraIndex]
                };
                cameraIndex++;
                try
                {
                    m_TvActionApi.StartTvVideo(tvPlayParam);
                }
                catch (Exception ex)
                {
                    _logger.LogError("执行上墙预案异常：{0}", ex.InnerException);
                }
            }
        }

        /// <summary>
        /// 移除报警确认前动作
        /// </summary>
        /// <param name="plan"></param>
        private void RemoveBeforePlanAction()
        {
            foreach (var action in s_cacheTvAction)
            {
                if (action.PlanTrigger != null)
                {
                    if (this.PlanTrigger.AlarmLogId.Equals(action.PlanTrigger.AlarmLogId))
                    {
                        action.VideoRoundTask.Stop();
                        s_cacheTvAction.Remove(action);
                        _logger.LogInformation("找到报警确认前视频上墙动作，停止并移除");
                        break;
                    }
                }
            }
        }

        public override void Stop()
        {
            _logger.LogError("停止视频上墙");
            if (VideoRoundTask != null)
                VideoRoundTask.Stop();

            s_cacheTvAction.Remove(this);

            //先恢复报警预案
            _logger.LogError("从列表中查找上一视频上墙动作 Begin...");
            TvAction resumeAction = null;
            resumeAction = s_cacheTvAction.LastOrDefault(t => t.PlanTrigger != null && t.VideoRoundTask != null && t.VideoRoundTask.RoundStatus != RoundStatus.Running);
            //恢复普通预案
            if (resumeAction == null)
                resumeAction = s_cacheTvAction.LastOrDefault(t => t.VideoRoundTask != null && t.VideoRoundTask.RoundStatus != RoundStatus.Running);
            _logger.LogError("从列表中查找上一视频上墙动作 End...");
            if (resumeAction != null)
            {
                if (resumeAction.VideoRoundTask.RoundStatus == RoundStatus.Pausing)
                {
                    resumeAction.VideoRoundTask.Resume();
                    _logger.LogInformation("恢复上一视频上墙动作！！");
                }
                else
                {
                    resumeAction.VideoRoundTask.Start();
                    _logger.LogInformation("启动上一视频上墙动作！！");
                }
            }
            else
            {
                _logger.LogInformation("没有找到要恢复的视频上墙动作！！");
            }
        }
    }
}
