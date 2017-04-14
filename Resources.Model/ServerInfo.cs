using Infrastructure.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resources.Model
{
    /// <summary>
    /// 基础服务信息
    /// </summary>
    public class ServerInfo
    {
        /// <summary>
        /// pk
        /// </summary>
        public Guid ServerInfoId
        {
            get;set;
        }

        [MaxLength(32)]
        public string ServerName
        {
            get;set;
        }

        public Guid OrganizationId
        {
            get;set;
        }

        /// <summary>
        /// 所属机构
        /// </summary>
        public virtual Organization Organization
        {
            get; set;
        }
        
        public string EndPointsJson
        {
            get;set;
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
            get;set;
        }
        public Guid ModifiedByUserId
        {
            get; set;
        }

        [NotMapped]
        /// <summary>
        /// 服务列表
        /// </summary>
        public List<ServiceInfo> Services
        {
            get;set;
        }
    }
}
