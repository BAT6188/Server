using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Surveillance.ViewModel
{
    /// <summary>
    /// FTP回放/下载参数对象
    /// </summary>
    public class RecordPlayByFtp
    {
        public string FileName { get; set; }

        public string SaveFile { get; set; }

        public ServiceInfo FTPInfo { get; set; }
    }
}
