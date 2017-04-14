using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resources.Data
{
    /// <summary>
    /// 通用指令回复
    /// </summary>
    public class CmdResponse<T>
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
        public T Message { get; set; }
    }
}
