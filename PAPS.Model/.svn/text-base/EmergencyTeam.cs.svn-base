using Infrastructure.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAPS.Model
{
    /// <summary>
    /// 应急小组 Many To Many
    /// </summary>
    public class EmergencyTeam
    {
        public Guid DutyGroupScheduleId
        {
            get;set;
        }

        [JsonIgnore]
        public virtual DutyGroupSchedule DutyGroupSchedule
        {
            get;set;
        }

        public Guid StaffId { get; set; }


        public virtual Staff Staff { get; set; }
    }
}
