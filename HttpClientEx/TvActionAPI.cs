using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Surveillance.ViewModel;
using Resources.Model;
using HttpClientEx;

namespace HttpClientEx
{
    /// <summary>
    /// 视频上墙动作
    /// </summary>
    public class TvActionApi
    {
        /// <summary>
        /// 数字矩阵服务中心
        /// </summary>
        private Resources.Model.ServiceInfo DigitMatrixCenter
        {
            get; set;
        }

        public TvActionApi(Resources.Model.ServiceInfo digitMatrixCenter)
        {
            this.DigitMatrixCenter = digitMatrixCenter;
        }

        /// <summary>
        /// 视频上墙
        /// </summary>
        /// <param name="tvPlayParam"></param>
        /// <returns></returns>
        public int StartTvVideo(TVCameraView tvPlayParam)
        {
            if (DigitMatrixCenter == null)
                throw new Exception("请配置数字矩阵中心！");
            string url = string.Format("http://{0}:{1}/dmc/tv/start", DigitMatrixCenter.EndPoints[0].IPAddress, DigitMatrixCenter.EndPoints[0].Port);
            HttpClientHelper.Post<TVCameraView>(tvPlayParam, url,false);
            return 0;
        }

        /// <summary>
        /// 停止视频上墙
        /// </summary>
        /// <param name="tvStopParam"></param>
        /// <returns></returns>
        public int StopTvVideo(MonitorView tvStopParam)
        {
            if (DigitMatrixCenter == null)
                throw new Exception("请配置数字矩阵中心！");
            string url = string.Format("http://{0}:{1}/dmc/tv/stop", DigitMatrixCenter.EndPoints[0].IPAddress, DigitMatrixCenter.EndPoints[0].Port);
            var result = HttpClientHelper.Post<MonitorView>(tvStopParam, url);
            return 0;
        }

        /// <summary>
        /// 监视器分屏
        /// </summary>
        /// <param name="monitorDivideParam"></param>
        /// <returns></returns>
        public int DivideMonitor(MonitorView monitorDivideParam)
        {
            if (DigitMatrixCenter == null)
                throw new Exception("请配置数字矩阵中心！");
            string url = string.Format("http://{0}:{1}/}/dmc/tv/split", DigitMatrixCenter.EndPoints[0].IPAddress, DigitMatrixCenter.EndPoints[0].Port);
            HttpClientHelper.Put<MonitorView>(monitorDivideParam, url);
            return 0;
        }

        /// <summary>
        /// 窗口拼接
        /// </summary>
        /// <param name="monitorCmbParam"></param>
        /// <returns></returns>
        public int CombineMonitor(CombineWindow monitorCmbParam)
        {
            if (DigitMatrixCenter == null)
                throw new Exception("请配置数字矩阵中心！");
            string url = string.Format("http://{0}:{1}/dmc/combinewnd", DigitMatrixCenter.EndPoints[0].IPAddress, DigitMatrixCenter.EndPoints[0].Port);
            HttpClientHelper.Put<CombineWindow>(monitorCmbParam, url);
            return 0;
        }

        /// <summary>
        /// 删除拼接/漫游屏
        /// </summary>
        /// <param name="monitor"></param>
        /// <param name="windowId"></param>
        /// <returns></returns>
        public int DeleteWindow(int monitor, int windowId)
        {
            if (DigitMatrixCenter == null)
                throw new Exception("请配置数字矩阵中心！");
            string url = string.Format("http://{0}:{1}/dmc/Monitor={2}?WindowID={3}", DigitMatrixCenter.EndPoints[0].IPAddress, DigitMatrixCenter.EndPoints[0].Port, windowId);
            string result = string.Empty;
            HttpClientHelper.Delete(url, ref result);
            return 0;
        }

        /// <summary>
        /// 创建漫游窗口
        /// </summary>
        /// <param name="roamWnd"></param>
        /// <returns></returns>
        public int CreateRoamWindow(RoamWindow roamWnd)
        {
            if (DigitMatrixCenter == null)
                throw new Exception("请配置数字矩阵中心！");
            string url = string.Format("http://{0}:{1}/dmc/roamwnd", DigitMatrixCenter.EndPoints[0].IPAddress, DigitMatrixCenter.EndPoints[0].Port);
            HttpClientHelper.Post<RoamWindow>(roamWnd, url);
            return 0;
        }

        /// <summary>
        /// 漫游
        /// </summary>
        /// <param name="roamWnd"></param>
        /// <returns></returns>
        public int UpdateRoamWindow(RoamWindow roamWnd)
        {
            if (DigitMatrixCenter == null)
                throw new Exception("请配置数字矩阵中心！");
            string url = string.Format("http://{0}:{1}/dmc/roamwnd", DigitMatrixCenter.EndPoints[0].IPAddress, DigitMatrixCenter.EndPoints[0].Port);
            HttpClientHelper.Put<RoamWindow>(roamWnd, url);
            return 0;
        }

        /// <summary>
        /// 解码器布局
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public int SetLayout(int rows, int columns)
        {
            if (DigitMatrixCenter == null)
                throw new Exception("请配置数字矩阵中心！");
            string url = string.Format("http://{0}:{1}/dmc/layout", DigitMatrixCenter.EndPoints[0].IPAddress, DigitMatrixCenter.EndPoints[0].Port);
            Layout layout = new Layout{ Row = rows, Column = columns };
            HttpClientHelper.Post<Layout>(layout, url);
            return 0;
        }

        /// <summary>
        /// 获取解码器布局
        /// </summary>
        /// <returns></returns>
        public DecoderLayout GetLayout()
        {
            if (DigitMatrixCenter == null)
                throw new Exception("请配置数字矩阵中心！");
            string url = string.Format("http://{0}:{1}/dmc/monitor/layout", DigitMatrixCenter.EndPoints[0].IPAddress, DigitMatrixCenter.EndPoints[0].Port);

            return HttpClientHelper.GetOne<DecoderLayout>(url);
        }
    }

    public class Layout
    {
        public int Row { get; set; }

        public int Column { get; set; }
    }
}
