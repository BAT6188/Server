using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resources.Model
{
    public class SIPTerminal
    {
        public Guid SIPTerminalId
        {
            get; set;
        }

        public string SIPTerminalName { get; set; }

        public Guid DeviceInfoId
        {
            get; set;
        }

        public IPDeviceInfo DeviceInfo
        {
            get; set;
        }

        public string User
        {
            get; set;
        }

        public string Password { get; set; }

        public int Phone { get; set; }

    }
}
