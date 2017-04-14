using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveillance.ViewModel
{
    /// <summary>
    /// 录像上墙任务设置参数
    /// </summary>
    class TvRecordTaskSettingParam
    {
        public ServerInfo DVM
        {
            get;set;
        }

        public string TaskXML
        {
            get;set;
        }
    }
}
