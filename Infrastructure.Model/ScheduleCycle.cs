using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Model
{
    /// <summary>
    /// 排程周期
    /// </summary>
    public class ScheduleCycle
    {
        public Guid ScheduleCycleId
        {
            get; set;
        }

        /// <summary>
        /// 周期类型 日，周，月
        /// </summary>
        public Guid? CycleTypeId
        {
            get;set;
        }

        public virtual SystemOption CycleType
        {
            get;set;
        }

        ///// <summary>
        ///// 时间段
        ///// </summary>
        //public List<TimePeriod> TimePeriods { get; set; }

        /// <summary>
        /// 时间段
        /// </summary>
        public List<DayPeriod> DayPeriods { get; set; }

        /// <summary>
        /// 最后执行时间
        /// </summary>
        public DateTime? LastExecute { get; set; }

        /// <summary>
        /// 下次执行时间
        /// </summary>
        public DateTime? NextExecute { get; set; }

        public string MonthsJson
        {
            get; set;
        }

        public string DaysJson
        {
            get; set;
        }

        /// <summary>
        /// 月份
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public int[] Months
        {
            get
            {
                if (MonthsJson != null)
                    return JsonConvert.DeserializeObject<int[]>(MonthsJson);
                return null;
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
        [JsonIgnore]
        public int[] Days
        {
            get
            {
                if (DaysJson != null)
                    return JsonConvert.DeserializeObject<int[]>(DaysJson);
                return null;
            }
            set
            {
                DaysJson = JsonConvert.SerializeObject(value);
            }
        }

        public string WeekDayJson
        {
            get; set;
        }

        /// <summary>
        /// 指定周几
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public int[] WeekDays
        {
            get
            {
                if (WeekDayJson != null)
                    return JsonConvert.DeserializeObject<int[]>(WeekDayJson);
                return null;
            }
            set
            {
                WeekDayJson = JsonConvert.SerializeObject(value);
            }
        }
    }
}
