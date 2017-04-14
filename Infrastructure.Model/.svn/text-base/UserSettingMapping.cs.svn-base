using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Model
{
    /// <summary>
    /// 用户与用户设置 (n->n): 针对Client
    /// </summary>
    public class UserSettingMapping
    {
        public Guid UserSettingId
        {
            get;set;
        }

        public UserSetting UserSetting
        {
            get;set;
        }

        [JsonIgnore]
        public Guid UserId
        {
            get;set;
        }
        
        [JsonIgnore]
        public User User
        {
            get; set;
        }
    }
}
