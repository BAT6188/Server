using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resources.Data
{
    /// <summary>
    /// 通用请求协议
    /// </summary>
    public class CmdRequest
    {
        /// <summary>
        /// 命令序号
        /// </summary>
        public Guid SN { get; set; }

        /// <summary>
        /// 命令
        /// </summary>
        public string Cmd { get; set; }

        /// <summary>
        /// 消息内容，Json格式
        /// </summary>
        public string Message { get; set; }

    }
}
