using Infrastructure.Model;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resources.Data
{
    public class DefenseDeviceView
    {
        public virtual IPDeviceInfo DeviceInfo { get; set; }


        /// <summary>
        /// 防区编号
        /// </summary>
        public int DefenseNo { get; set; }

        /// <summary>
        /// 报警输入开关编号
        /// </summary>
        public int AlarmIn { get; set; }

        /// <summary>
        /// 报警输出开关编号
        /// </summary>
        public int AlarmOut { get; set; }

        /// <summary>
        /// 报警输入是否常开
        /// </summary>
        public bool AlarmInNormalOpen { get; set; }

        /// <summary>
        /// 防区方向
        /// </summary>
        public int DefenseDirectionCode { get; set; }

    }
}
