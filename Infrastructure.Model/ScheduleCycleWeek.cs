using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Model
{
    /// <summary>
    /// 每周循环
    /// </summary>
   public class ScheduleCycleWeek//:ScheduleCycle
    {
        public string WeekDayJson
        {
            get;set;
        }

        /// <summary>
        /// 指定周几
        /// </summary>
        [NotMapped]
        public int[] WeekDays
        {
            get
            {
                return JsonConvert.DeserializeObject<int[]>(WeekDayJson);
            }
            set
            {
                WeekDayJson = JsonConvert.SerializeObject(value);
            }
        }
    }
}
