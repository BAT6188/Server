using System;

namespace Infrastructure.Model
{
    /// <summary>
    /// 登录用户
    /// </summary>
    public class OnlineUser 
    {
        /// <summary>
        /// 在线用户ID
        /// </summary>
        public Guid OnLineUserId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginTime
        {
            get;set;
        }

        /// <summary>
        /// 最后心跳时间
        /// </summary>
        public DateTime KeepAlived
        {
            get;set;
        }

        /// <summary>
        /// 用户终端ID
        /// </summary>
        public Guid LoginTerminalId
        {
            get; set;
        }

        /// <summary>
        /// 用户终端
        /// </summary>
        public virtual UserTerminal LoginTerminal
        {
            get;set;
        }
    }
}