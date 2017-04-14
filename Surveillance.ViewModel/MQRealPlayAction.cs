using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Surveillance.ViewModel
{
    public class MQRealPlayAction
    {
        public List<RealPlayParam> Cameras { get; set; }

        public Guid AlarmLogId { get; set; }

        public bool IsForwardAlarm { get; set; }
    }
}
