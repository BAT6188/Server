using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Model
{
    /// <summary>
    /// 时间周期
    /// </summary>
    public class TimePeriod
    {
        /// <summary>
        /// 时间周期ID
        /// </summary>
        public Guid TimePeriodId { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }


        /// <summary>
        /// 自定义序号
        /// </summary>
        public int OrderNo
        {
            get; set;
        }

        /// <summary>
        /// 查勤包附加属性
        /// </summary>
        public string ExtraJson
        {
            get;set;
        }

        /// <summary>
        ///查勤包附加属性
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public TimePeriodExtra TimePeriodExtra
        {
            get
            {
                if (string.IsNullOrEmpty(ExtraJson))
                    return null;
                return JsonConvert.DeserializeObject<TimePeriodExtra>(ExtraJson);
            }
            set
            {
                ExtraJson = JsonConvert.SerializeObject(value);
            }
        }

    }
}
