using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PAPS.Model
{
    /// <summary>
    /// 查勤处理方式
    /// </summary>
    public class DutyCheckLogDispose
    {
        [Key]
        public Guid DutyCheckLogDisposeId
        {
            get;set;
        }

        /// <summary>
        /// 
        /// </summary>
        public Guid DutyCheckLogId
        {
            get;set;
        }

        [JsonIgnore]
        public virtual DutyCheckLog DutyCheckLog
        {
            get;set;
        }

        public Guid DisposeId
        {
            get;set;
        }

        public virtual SystemOption Dispose
        {
            get;set;
        }

    }
}
