
using PAPS.Data;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskServer.Action;

namespace TaskServer.Argument
{
    /// <summary>
    /// 声光报警动作响应记录
    /// </summary>
    internal class SoundLightActionRecord
    {
        public Guid ActionId { get; set; }

        public List<SoundLightActionEx> Actions
        {
            get;set;
        }
    }

    /// <summary>
    /// 声光报警控制
    /// </summary>
    internal class SoundLightActionEx
    {
        public SoundLightControl SoundLightControl
        {
            get; set;
        }
        /// <summary>
        /// 哨位中心服务
        /// </summary>
        public ServiceInfo Ascs
        {
            get; set;
        }
    }
}
