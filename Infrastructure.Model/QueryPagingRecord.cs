using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Model
{
    /// <summary>
    /// 查询分页记录
    /// </summary>
    public class QueryPagingRecord
    {
        /// <summary>
        /// 记录总数
        /// </summary>
        public int SumRecordCount
        {
            get;set;
        }

        /// <summary>
        /// 当前分页的数据
        /// </summary>
        public Object  Record
        {
            get;set;
        }
    }
}
