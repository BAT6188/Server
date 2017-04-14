using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resources.Model
{
    /// <summary>
    /// 预置点
    /// </summary>
    public class PresetSite
    {
        /// <summary>
        /// 巡航Id
        /// </summary>
        public Guid PresetSiteId { get; set; }

        /// <summary>
        /// 预置点名称
        /// </summary>
        public string PresetSizeName { get; set; }

        /// <summary>
        /// 预置点编号
        /// </summary>
        public byte PresetSiteNo { get; set; }


        ///// <summary>
        ///// 单点巡航时长：s
        ///// </summary>
        //public byte ScanInterval { get; set; }

        ///// <summary>
        ///// many-to-many
        ///// </summary>
        //public List<CruiseScanGroupPresetSite> Groups { get; set; }

        /// <summary>
        /// 摄像机id
        /// </summary>
        public Guid CameraId
        {
            get;set;
        }

        public virtual Camera Camera
        {
            get;set;
        }
    }
}
