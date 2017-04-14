using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Model
{
    /// <summary>
    /// 附件
    /// </summary>
    public class Attachment
    {
        /// <summary>
        /// 附件ID
        /// </summary>
        public Guid AttachmentId
        {
            get; set;
        }

        /// <summary>
        /// 附件名称
        /// </summary>
        public string AttachmentName
        {
            get; set;
        }

        /// <summary>
        /// 附件路径
        /// </summary>
        public string AttachmentPath
        {
            get; set;
        }

        /// <summary>
        /// 文件类型
        /// </summary>
        public int AttachmentType
        {
            get; set;
        }

        /// <summary>
        /// 附件版本
        /// </summary>
        public double AttachmentVersion
        {
            get;set;
        }

        /// <summary>
        /// 修改者Id
        /// </summary>
        public Guid? ModifiedById { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        public virtual User ModifiedBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime Modified
        {
            get; set;
        }

        [NotMapped]
        /// <summary>
        /// 附件文件内容，用于上传/下载
        /// </summary>
        public IFormFile File
        {
            get;set;
        }

        [NotMapped]
        /// <summary>
        /// 附件文件数据，用于上传/下载
        /// </summary>
        public Byte[] FileData {
            get;set;
        }

        /// <summary>
        ///  内容类型，不用传递
        /// </summary>
        public string ContentType
        {
            get;set;
        }
    }
}