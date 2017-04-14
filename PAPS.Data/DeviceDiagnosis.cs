using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAPS.Data
{
    public class DeviceDiagnosis
    {
        public Guid DeviceId { get; set; }
        public uint SentinelCode { get; set; }
        public string Diagnosis { get; set; }

    }
}
