using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PAPS.Model
{
    /// <summary>
    /// Many To Many 
    /// </summary>
    public class DutyCheckLogAppraise
    {
        [Key]
        public Guid DutyCheckLogAppraiseId
        {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public Guid DutyCheckLogId
        {
            get; set;
        }

        [JsonIgnore]
        public virtual DutyCheckLog DutyCheckLog
        {
            get; set;
        }

        public Guid DutyCheckAppraiseId
        {
            get; set;
        }


        public virtual DutyCheckAppraise DutyCheckAppraise
        {
            get;set;
        }
    }
}
