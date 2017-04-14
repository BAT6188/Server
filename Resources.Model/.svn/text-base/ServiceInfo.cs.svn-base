using Infrastructure.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resources.Model
{
    public class ServiceInfo
    {
        public Guid ServiceInfoId
        {
            get;set;
        }

        [MaxLength(32)]
        public string ServiceName
        {
            get; set;
        }

        public Guid ServerInfoId { get; set; }

        public virtual ServerInfo ServerInfo { get; set; }

        public Guid ServiceTypeId
        {
            get; set;
        }

        /// <summary>
        /// 服务器类型
        /// </summary>
        public virtual SystemOption ServiceType { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public String Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public String Password { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime Modified
        {
            get; set;
        }

        /// <summary>
        /// 修改人
        /// </summary>
        public User ModifiedBy
        {
            get; set;
        }
        public Guid ModifiedByUserId
        {
            get; set;
        }

        //public int Port
        //{
        //    get;set;
        //}


        public string EndPointsJson
        {
            get; set;
        }

        [NotMapped]
        //[JsonIgnore]
        public List<EndPointInfo> EndPoints
        {
            get
            {
                if (string.IsNullOrEmpty(EndPointsJson))
                {
                    return null;
                }
                return JsonConvert.DeserializeObject<List<EndPointInfo>>(EndPointsJson);
            }
            set
            {
                EndPointsJson = JsonConvert.SerializeObject(value);
            }
        }
    }
}
