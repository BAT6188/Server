using Infrastructure.Model;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAPS.Data
{
    /// <summary>
    /// 指纹操作参数（下发/清除）
    /// </summary>
    public class FingerAction
    {
        public List<Staff> Staffs { get; set; }

        public List<Sentinel> Sentinels { get; set; }
    }
}
