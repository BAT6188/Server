using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resources.Model
{
    public class CruiseScanGroupPresetSite
    {
        //public Guid CruiseScanGroupPresetSiteId
        //{
        //    get; set;
        //}
        public Guid CruiseScanGroupId
        {
            get; set;
        }

        [JsonIgnore]
        public CruiseScanGroup CruiseScanGroup
        {
            get;set;
        }

        public Guid PresetSiteID
        {
            get; set;
        }

        public PresetSite PresetSite
        {
            get;set;
        }

        /// <summary>
        /// 预置点巡航时长
        /// </summary>
        public int ScanInterval
        {
            get;set;
        }
    }
}
