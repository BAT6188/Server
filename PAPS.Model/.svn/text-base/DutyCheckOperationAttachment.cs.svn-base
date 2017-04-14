using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAPS.Model
{
    /// <summary>
    /// 查勤操作
    /// </summary>
    public class DutyCheckOperationAttachment
    {
        /// <summary>
        /// 查勤操作附件ID
        /// </summary>
        public Guid DutyCheckOperationAttachmentId { get; set; }

        /// <summary>
        /// 查勤操作ID
        /// </summary>
        public Guid DutyCheckOperationId { get; set; }

        /// <summary>
        /// 附件ID
        /// </summary>
        public Guid AttachmentId { get; set; }



        /// <summary>
        /// 附件
        /// </summary>
        public virtual Attachment Attachment
        {
            get;set;
        }

        /// <summary>
        /// 附件类型ID（例如：截图/录像）
        /// </summary>
        public Guid AttachmentTypeId { get; set; }

        /// <summary>
        /// 附件类型
        /// </summary>
        public virtual SystemOption AttachmentType { get; set; }

    }
}
