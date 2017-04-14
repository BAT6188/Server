using Infrastructure.Model;
using System;

namespace PAPS.Model
{
    /// <summary>
    /// 查勤评价选项
    /// </summary>
    public class DutyCheckAppraise
    {
        /// <summary>
        /// 评价ID
        /// </summary>
        public Guid DutyCheckAppraiseId
        {
            get;set;
        }

        /// <summary>
        /// 评价名称
        /// </summary>
        public string DutyCheckAppraiseName
        {
            get;set;
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
        /// 备注
        /// </summary>
        public string Description
        {
            get;set;
        }

        /// <summary>
        /// 评价类型Id
        /// </summary>
        public Guid AppraiseTypeId
        {
            get; set;
        }

        /// <summary>
        /// 评价类型
        /// </summary>
        public virtual SystemOption AppraiseType
        {
            get;set;
        }

        /// <summary>
        /// 评价ICOId
        /// </summary>
        public Guid? AppraiseICOId
        {
            get; set;
        }

        /// <summary>
        /// 评价ICO
        /// </summary>
        public virtual Attachment AppraiseICO
        {
            get;set;
        }

        /// <summary>
        /// 自定义序号
        /// </summary>
        public int OrderNo
        {
            get; set;
        }
    }
}