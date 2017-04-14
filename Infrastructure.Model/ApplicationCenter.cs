using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Model
{
    /// <summary>
    /// 应用中心
    /// eg. GB28181属性定义
    /// </summary>
    public class ApplicationCenter
    {
        [Key]
        public Guid ApplicationCenterId
        {
            get;set;
        }

        /// <summary>
        /// 本地中心编码
        /// </summary>
        public string ApplicationCenterCode
        {
            get;set;
        }

        /// <summary>
        /// 上级中心编码
        /// </summary>
        public string ParentApplicationCenterCode
        {
            get;set;
        }
        
        public string EndPointsJson
        {
            get;set;
        }
        /// <summary>
        /// 本地IP端点
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public List<EndPointInfo> EndPoints
        {
            get
            {
                if (string.IsNullOrEmpty(EndPointsJson))
                    return null;
                return JsonConvert.DeserializeObject<List<EndPointInfo>>(EndPointsJson);
            }
            set
            {
                EndPointsJson = JsonConvert.SerializeObject(value);
            }
        } 

        /// <summary>
        /// 注册用户名
        /// </summary>
        public string RegisterUser
        {
            get;set;
        }

        /// <summary>
        /// 注册密码
        /// </summary>
        public string RegisterPassword
        {
            get;set;
        }
    }
}
