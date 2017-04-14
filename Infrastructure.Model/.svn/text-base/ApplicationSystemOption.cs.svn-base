using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Model
{
    /// <summary>
    /// 应用与系统选项的多对多映射
    /// </summary>
    public class ApplicationSystemOption
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        public Guid ApplicationId
        {
            get;set;
        }

        /// <summary>
        /// 应用
        /// </summary>
        public virtual Application Application
        {
            get;set;
        }

        /// <summary>
        /// 系统选项ID
        /// </summary>
        public Guid SystemOptionId
        {
            get;set;
        }

        /// <summary>
        /// 系统选项
        /// </summary>
        public virtual SystemOption SystemOption
        {
            get;set;
        }

    }
}
