using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveillance.ViewModel.Enum
{
    public enum VideoStream : uint
    {
        /// <summary>
        /// 主码流
        /// </summary>
        MainStream = 0,
        /// <summary>
        /// 子码流
        /// </summary>
        SubStream1st,
        /// <summary>
        /// 次子码流
        /// </summary>
        SubStream2nd
    }
}
