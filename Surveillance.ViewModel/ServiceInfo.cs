using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Surveillance.ViewModel
{

    public class ServiceInfo
    {
        public List<EndPointInfo> EndPoints { get; set; }

        public string User { get; set; }

        public string Password { get; set; }
    }
}
