using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Surveillance.ViewModel
{
    /// <summary>
    /// 截图参数，用于调用dcp 截图api传递参数
    /// </summary>
    public class SnapshotParam
    {
        /// <summary>
        /// 抓拍设备id
        /// </summary>
         public Guid IPDeviceInfoId { get; set; }

	    public Guid AttachmentId { get; set; }

        /// <summary>
        /// 事件Id 如打卡记录id，供弹记录id，暂时保留
        /// </summary>
        public Guid? EventId { get; set; }
	
        /// <summary>
        /// 事件类型，暂时保留
        /// </summary>
        public string EventType { get; set; }

        /// <summary>
        /// 截图名称
        /// </summary>
        public string FileName { get; set; }
    }
}
