using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resources.Data
{
    public class ServiceInfoView
    {
        public Guid ServiceInfoId
        {
            get; set;
        }

        public string ServiceName
        {
            get; set;
        }

        public Guid ServiceTypeId
        {
            get;set;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public String Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public String Password { get; set; }

        public List<EndPointInfo> EndPoints
        {
            get; set;
        }
    }
}
