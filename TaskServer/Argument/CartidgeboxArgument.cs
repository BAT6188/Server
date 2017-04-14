using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskServer.Argument
{
    /// <summary>
    /// 哨位台控制参数
    /// </summary>
    public class CartidgeboxArgument
    {
        /// <summary>
        /// 供弹哨位台Id
        /// </summary>
        public Guid SentinelId
        {
            get;set;
        }

        ///// <summary>
        ///// 哨位服务器
        ///// </summary>
        //public Guid SentinelServerId
        //{
        //    get;set;
        //}

        /// <summary>
        /// 是否密码确认打开子弹箱
        /// </summary>
        public bool PasswordConfirm
        {
            get;set;
        }
    }
}
