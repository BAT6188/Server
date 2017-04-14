using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources.Model
{
    /// <summary>
    /// 自动巡航组
    /// </summary>
    public class CruiseScanGroup
    {
        public Guid CruiseScanGroupId
        {
            get; set;
        }

        /// <summary>
        /// 巡航组名称
        /// </summary>
        public string GroupName
        {
            get; set;
        }

        /// <summary>
        /// 巡航组编号，取值范围1-4
        /// </summary>
        public int GroupIndex
        { get; set; }

        /// <summary>
        /// 巡航预置点
        /// </summary>
        public List<CruiseScanGroupPresetSite> PresetSites
        {
            get; set;
        }

        /// <summary>
        /// 监控点id
        /// </summary>
        public Guid CameraId
        {
            get; set;
        }

        //public virtual Camera Camera
        //{
        //    get; set;
        //}
    }
}
