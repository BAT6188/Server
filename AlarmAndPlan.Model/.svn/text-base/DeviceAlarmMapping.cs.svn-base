using Infrastructure.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace AlarmAndPlan.Model
{
    public class DeviceAlarmMapping
    {
        [Key]
        public Guid DeviceAlarmMappingId
        {
            get;set;
        }

        public Guid DeviceTypeId
        {
            get; set;
        }

        public Guid AlarmTypeId
        {
            get; set;
        }

        public virtual SystemOption DeviceType { get; set; }

        public virtual SystemOption AlarmType { get; set; }
    }
}
