using Infrastructure.Model;
using System;

namespace PAPS.Model
{
    /// <summary>
    /// 查勤问题选项
    /// </summary>
    public class DutyCheckMatter
    {
        /// <summary>
        /// 存在问题ID
        /// </summary>
        public Guid DutyCheckMatterId
        {
            get; set;
        }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public Guid OrganizationId
        {
            get; set;
        }

        /// <summary>
        /// 组织机构
        /// </summary>
        public virtual Organization Organization
        {
            get;set;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get;set;
        }



        /// <summary>
        /// 存在问题名称
        /// </summary>
        public string MatterName
        {
            get;set;
        }

        /// <summary>
        /// 存在问题分值
        /// </summary>
        public int MatterScore
        {
            get;set;
        }

        /// <summary>
        /// 图标Id
        /// </summary>
        public Guid? MatterICOId
        {
            get; set;
        }

        /// <summary>
        /// 图标
        /// </summary>
        public virtual Attachment MatterICO
        {
            get;set;
        }

        /// <summary>
        /// 序号
        /// </summary>
        public int OrderNo
        {
            get;set;
        }

        /// <summary>
        /// 语音文件Id
        /// </summary>
        public Guid? VoiceFileId
        {
            get; set;
        }

        /// <summary>
        /// 语音文件
        /// </summary>
        public Attachment VoiceFile
        {
            get;set;
        }
    }
}