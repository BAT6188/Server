using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resources.Model
{
    /// <summary>
    /// 哨位和指纹关系
    /// </summary>
    public class SentinelFingerPrintMapping
    {
        public Guid SentinelFingerPrintMappingId { get; set; }

        public Guid FingerprintId { get; set; }

        public virtual Fingerprint Fingerprint { get; set; }

        public Guid SentinelId { get; set; }

        public virtual Sentinel Sentinel { get; set; }
    }
}
