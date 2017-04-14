using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Model
{
    /// <summary>
    /// 授权信息
    /// </summary>
    public class AuthorizationInformation
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName
        {
            get; set;
        }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName
        {
            get;set;
        }

        /// <summary>
        /// 截止时间
        /// </summary>
        public DateTime Deadline
        {
            get;set;
        }

        /// <summary>
        /// 用户上限
        /// </summary>
        public int MaxOnlineUsers
        {
            get;set;
        }

        /// <summary>
        /// 摄像机路数上限
        /// </summary>
        public int MaxCameras
        {
            get;set;
        }


        /// <summary>
        ///  授权应用集合实际保存数据
        /// </summary>
        private string ApplicationJson
        {
            get; set;
        }


        /// <summary>
        /// 授权应用集合
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public List<Application> Applications
        {
            get
            {
                return (List<Application>)JsonConvert.DeserializeObject(ApplicationJson, typeof(List<Application>));
            }
            set
            {
                string jsontxt = JsonConvert.SerializeObject(value);
                ApplicationJson = jsontxt;
            }
        }
    }
}