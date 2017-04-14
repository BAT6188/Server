using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Utility
{
    public class JsonUtility
    {
        /// <summary>
        /// 首字母消息json序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string CamelCaseSerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj,
                 new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }
}
