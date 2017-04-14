using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAPS.Data
{
    /// <summary>
    /// 哨位台地图
    /// </summary>
    public class SentinelMapInfo
    {
        public Guid DeviceId { get; set; }

        public uint SentinelCode { get; set; }

        /// <summary>
        /// 地图数据,对应XML数据
        /// </summary>
        public string MapInfo { get; set; }

    }
}
