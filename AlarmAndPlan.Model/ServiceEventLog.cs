using Infrastructure.Model;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AlarmAndPlan.Model
{
    /// <summary>
    /// 服务器使事件
    /// </summary>
    public class ServiceEventLog
    {
        public Guid ServiceEventLogId { get; set; }

        /// <summary>
        /// 事件来源
        /// </summary>
        public Guid EventSourceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ServiceInfo EventSource { get; set; }

        /// <summary>
        /// 事件类型
        /// </summary>
        public Guid EventTypeId { get; set; }

        public virtual SystemOption EventType { get; set; }

        public DateTime TimeCreated { get; set; }

        /// <summary>
        /// 事件描述
        /// </summary>
        public string Description { get; set; }

        //根据服务器类型和端点确认服务器
        /// <summary>
        /// 服务器类型
        /// </summary>
        [NotMapped]
        public Guid ServerTypeId { get; set; }

        /// <summary>
        /// 服务器端点
        /// </summary>
        [NotMapped]
        public EndPointInfo EndPoint { get; set; }
    }
}
