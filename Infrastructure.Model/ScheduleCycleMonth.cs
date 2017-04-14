using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Model
{
    /// <summary>
    /// 按月循环
    /// </summary>
    public class ScheduleCycleMonth //: ScheduleCycle
    {
        public string MonthsJson
        {
            get;set;
        }

        public string DaysJson
        {
            get;set;
        }

        /// <summary>
        /// 月份
        /// </summary>
        //[NotMapped]
        [NotMapped]
        public int[] Months
        {
            get
            {
                return JsonConvert.DeserializeObject<int[]>(MonthsJson);
            }
            set
            {
                MonthsJson = JsonConvert.SerializeObject(value);
            }
        }

        /// <summary>
        /// 天
        /// </summary>
        [NotMapped]
        public int[] Days
        {
            get
            {
                return JsonConvert.DeserializeObject<int[]>(DaysJson);
            }
            set
            {
                DaysJson = JsonConvert.SerializeObject(value);
            }
        }

    }
}
