using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resources.Model
{
    /// <summary>
    /// 监控点分组
    /// </summary>
    public class MonitorySiteGroup
    {
        public Guid MonitorySiteGroupId
        {
            get; set;
        }

        /// <summary>
        /// 监控点分组名称
        /// </summary>
        [MaxLength(64)]
        public string GroupName
        {
            get;set;
        }

        /// <summary>
        /// 监控点列表
        /// </summary>
        public string MonitorySiteListJson
        {
            get;set;
        }

        [MaxLength(128)]
        public string Description
        {
            get;set;
        }

        public User ModifiedBy
        {
            get;set;
        }

        public DateTime? Mondified
        {
            get;set;
        }
    }
}
