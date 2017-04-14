using Infrastructure.Utility;
using Microsoft.Extensions.Logging;
using Resources.Model;
using Surveillance.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskServer.Action
{ 
    /// <summary>
    /// 视频轮巡任务
    /// </summary>
    public  class VideoRoundTask
    {
        ILogger<VideoRoundTask> _logger = Logger.CreateLogger<VideoRoundTask>();
        //protected List<Task> m_ActionTask = new List<Task>();
        private bool GoToNext = true;

        /// <summary>
        /// 轮巡状态
        /// </summary>
        protected RoundStatus m_RoundStatus = RoundStatus.None;

        /// <summary>
        /// 轮巡状态
        /// </summary>
        public RoundStatus RoundStatus
        {
            get { return m_RoundStatus; }
        }

        /// <summary>
        /// 轮巡片段索引
        /// </summary>
        protected int m_RoundSectionIndex = -1;

        /// <summary>
        /// 轮巡场景
        /// </summary>
        protected VideoRoundSceneView m_videoRoundScene;

        ///// <summary>
        /////  轮巡片段切换事件的方法
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public delegate void RoundSectionSwitchEventHandler(object sender, RoundEventArgs e);

        //public event RoundSectionSwitchEventHandler OnRoundSectionSwitch;

        //protected VideoRoundSection m_LastRoundSection;

        protected VideoRoundSectionView m_CurrentRoundSection;

        private Task m_Executor;

        #region 等待令牌
        private CancellationTokenSource m_CancellationTokeSource;

        private CancellationTokenSource m_WaitTokenSource;
        #endregion

        /// <summary>
        /// 适用于场景轮巡
        /// </summary>
        /// <param name="roundScene">null 引发ArgumentNullException异常</param>
        public VideoRoundTask(VideoRoundSceneView roundScene)
        {
            if (roundScene == null)
                throw new ArgumentNullException("参数roundScene未设置为对象引用实例");
            this.m_videoRoundScene = roundScene;
        }

        /// <summary>
        /// 适用于普通视频轮巡
        /// </summary>
        /// <param name="cameras">轮巡监控点</param>
        /// <param name="template">视频模板</param>
        /// <param name="roundInterval">轮巡间隔</param>
        public VideoRoundTask(IEnumerable<CameraView> cameras, TemplateLayout template, int roundInterval)
        {
            if (cameras == null)
                throw new ArgumentNullException("参数monitorysites未设置为对象引用实例");
            if (cameras.Count() == 0)
                throw new Exception("参数monitorysitesl元素数量不能少于0");
            if (template == null)
                throw new ArgumentNullException("参数template未设置为对象引用实例");
            if (roundInterval < 0)
                throw new Exception("参数roundInterval不能少于0");

            int stepLength = template.Cells.Count();
            int monitorysiteCount = cameras.Count();
            int sectionCount = monitorysiteCount % stepLength == 0 ? monitorysiteCount / stepLength : monitorysiteCount / stepLength + 1;
            m_videoRoundScene = new VideoRoundSceneView();
            m_videoRoundScene.VideoRoundSections = new List<VideoRoundSectionView>();
            for (int i = 0; i < sectionCount; ++i)
            {
                VideoRoundSectionView section = new VideoRoundSectionView();
                section.RoundInterval = roundInterval;
                section.TemplateLayout = template;
                List<RealPlayParam> playInfoList = new List<RealPlayParam>();
                cameras.Skip(i * stepLength).Take(stepLength).ToList().ForEach(t => {
                    playInfoList.Add(new RealPlayParam() { CameraView = t });
                });
                section.PlayInfoList = playInfoList;
                m_videoRoundScene.VideoRoundSections.Add(section);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="manualControl">动作触发</param>
        public void Next(bool manualControl = false)
        {
            ////section that is going to stop
            //if (m_RoundSectionIndex >= 0)
            //    m_LastRoundSection = m_videoRoundScene.VideoRoundSections.ElementAt(m_RoundSectionIndex);
            if (manualControl && m_videoRoundScene.VideoRoundSections.Count > 1)
            {
                m_WaitTokenSource.Cancel(); //中断，进入下一片段
                return;
            }

            //section that is going to run 
            m_RoundSectionIndex++;
            if (m_RoundSectionIndex == m_videoRoundScene.VideoRoundSections.Count())
                m_RoundSectionIndex = 0;
            m_CurrentRoundSection = m_videoRoundScene.VideoRoundSections.ElementAt(m_RoundSectionIndex);

            Console.WriteLine("{0:HH:mm:ss}--do next,Current section index:{1}" ,DateTime.Now, m_RoundSectionIndex);
            if (ExecuteRoundAction != null)
                ExecuteRoundAction(m_CurrentRoundSection);
        }

        ///// <summary>
        ///// 触发
        ///// </summary>
        //private void RaiseRoundSectionSwitchEvent()
        //{
        //    if (OnRoundSectionSwitch != null)
        //    {
        //        m_CurrentRoundSection = m_videoRoundScene.VideoRoundSections.ElementAt(m_RoundSectionIndex);
        //        RoundEventArgs arg = new RoundEventArgs() { Section = m_CurrentRoundSection };
        //        OnRoundSectionSwitch(this, arg);
        //    }
        //}

        ///// <summary>
        ///// 执行轮巡动作
        ///// </summary>
        //protected abstract void ExecuteRoundAction();

        public void Pause()
        {
            _logger.LogInformation("暂停视频上墙");
            if (m_CancellationTokeSource == null || m_CancellationTokeSource.IsCancellationRequested)
                m_CancellationTokeSource = new CancellationTokenSource();
            m_RoundStatus = RoundStatus.Pausing;
        }

        /// <summary>
        /// 是否人工触发
        /// </summary>
        /// <param name="manualControl"></param>
        public void Previous(bool manualControl = false)
        {
            //if (m_RoundSectionIndex >= 0)
            //    m_LastRoundSection = m_videoRoundScene.VideoRoundSections.ElementAt(m_RoundSectionIndex);
            if (manualControl && m_videoRoundScene.VideoRoundSections.Count > 1)
            {
                GoToNext = false;
                m_WaitTokenSource.Cancel();
                return;
            }
            m_RoundSectionIndex--;
            if (m_RoundSectionIndex < 0)
                m_RoundSectionIndex = m_videoRoundScene.VideoRoundSections.Count() - 1;
            Console.WriteLine("{0:HH:mm:ss}--do previous, Current section index:{1}", DateTime.Now, m_RoundSectionIndex);
            m_CurrentRoundSection = m_videoRoundScene.VideoRoundSections.ElementAt(m_RoundSectionIndex);

            if (ExecuteRoundAction != null)
                ExecuteRoundAction(m_CurrentRoundSection);
        }

        public void Resume()
        {
            _logger.LogInformation("恢复视频上墙");
            if (m_CancellationTokeSource !=null && !m_CancellationTokeSource.IsCancellationRequested)
                m_CancellationTokeSource.Cancel();
            m_RoundStatus = RoundStatus.Running;
        }

        public void Start()
        {
            _logger.LogInformation("启动视频上墙");
            m_CancellationTokeSource = new CancellationTokenSource();
            m_Executor = Task.Factory.StartNew(() => { Run(); }, m_CancellationTokeSource.Token);
        }

        public void Stop()
        {
            _logger.LogInformation("停止视频上墙");
            m_RoundStatus = RoundStatus.Exit;
            if (m_CancellationTokeSource != null && !m_CancellationTokeSource.IsCancellationRequested)
                m_CancellationTokeSource.Cancel();
        }

        private void Run()
        {
            m_RoundStatus = RoundStatus.Running;
            while (true)
            {
                if (m_RoundStatus == RoundStatus.Exit)
                    break;

                var token = new CancellationTokenSource();
                if (m_RoundStatus == RoundStatus.Pausing)
                {
                    try
                    {
                        //暂停
                        Task.Delay(-1).Wait(m_CancellationTokeSource.Token);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("等待异常，Message:{0},\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                    }
                }
                if (GoToNext)
                    Next();
                else
                {
                    Previous();
                    GoToNext = true;
                }
                if (m_videoRoundScene.VideoRoundSections.Count == 1) //不轮巡
                    Task.Delay(-1).Wait(m_CancellationTokeSource.Token);
                else
                {
                    try
                    {
                        //休眠
                        m_WaitTokenSource = new CancellationTokenSource();
                        Task.Delay(m_CurrentRoundSection.RoundInterval * 1000).Wait(m_WaitTokenSource.Token);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            Console.WriteLine("Round End at {0}, Task Status:{1}", DateTime.Now, m_Executor.Status);
        }

        /// <summary>
        /// 执行预案动作
        /// </summary>
        public Action<VideoRoundSectionView> ExecuteRoundAction
        {
            get;set;
        }
    }

    /// <summary>
    /// 轮巡状态
    /// </summary>
    public enum RoundStatus
    {
        None,
        Running,
        Pausing,
        Exit
    }
}
