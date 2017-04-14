using PAPS.Model;
using Resources.Model;
using Surveillance.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAPS.Data
{
    /// <summary>
    /// 查勤点明细
    /// </summary>
    public class DutyCheckPointView
    {
        /// <summary>
        /// 查勤记录
        /// </summary>
        public DutyCheckLog DutyCheckLog
        {
            get;set;
        }

        /// <summary>
        /// 监控点视频信息
        /// </summary>
        public CameraView CameraView
        {
            get;set;
        }
    }
}
