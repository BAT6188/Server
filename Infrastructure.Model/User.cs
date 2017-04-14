using System;
using System.Collections.Generic;

namespace Infrastructure.Model
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid UserId
        {
            get;set;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string UserName
        {
            get;set;
        }

        /// <summary>
        /// 组织机构Id
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
        /// 角色集合
        /// </summary>
        public List<UserRole> UserManyToRole
        {
            get;set;
        }

        /// <summary>
        /// 可管理的资源
        /// </summary>
        public List<ControlResources> ControlResources { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PasswordHash
        {
            get;set;
        }

        /// <summary>
        /// 所属系统Id
        /// </summary>
        public Guid ApplicationId
        {
            get; set;
        }

        /// <summary>
        /// 所属系统
        /// </summary>
        public virtual Application Application
        {
            get; set;
        }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable
        {
            get;set;
        }

        /// <summary>
        /// 两个因素
        /// </summary>
        public int TwoFactorEnabled
        {
            get;set;
        }

        /// <summary>
        /// 防伪印章
        /// </summary>
        public string SecurityStamp
        {
            get;set;
        }

        /// <summary>
        /// 电话确认
        /// </summary>
        public int PhoneNumberConfirmed
        {
            get;set;
        }

        /// <summary>
        /// 锁定时间
        /// </summary>
        public DateTime LockoutEndDateUtc
        {
            get;set;
        }

        /// <summary>
        /// 是否被锁定
        /// </summary>
        public bool LockoutEnabled
        {
            get;set;
        }

        /// <summary>
        /// 重连失败次数
        /// </summary>
        public int AccessFailed
        {
            get;set;
        }
        
        /// <summary>
        /// 备注
        /// </summary>
        public string Description
        {
            get; set;
        }

        /// <summary>
        /// 用户设置
        /// </summary>
        public ICollection<UserSettingMapping> UserSettings
        {
            get;set;
        }
    }
}