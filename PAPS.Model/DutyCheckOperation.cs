using Infrastructure.Model;
using System;
using System.Collections.Generic;

namespace PAPS.Model
{
    /// <summary>
    /// 查勤操作
    /// </summary>
    public class DutyCheckOperation
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid DutyCheckOperationId
        {
            get;set;
        }


        /// <summary>
        /// 附件集合
        /// </summary>
        public ICollection<DutyCheckOperationAttachment> Attachments
        {
            get;set;
        }         
    }
}
