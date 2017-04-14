using Infrastructure.Utility;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskServer.Argument;

namespace TaskServer.Action
{
    internal class FireShotAction : PlanActionProvider
    {
        List<FireShotArgument> m_actionArgumentList;

        ILogger<FireShotAction> _logger = Logger.CreateLogger<FireShotAction>();

        public override void AddPlanActionItem(PlanActionArgument item)
        {
            //if (m_actionArgumentList == null)
            //    m_actionArgumentList = new List<FireShotArgument>();
            //m_actionArgumentList.Add(JsonConvert.DeserializeObject<FireShotArgument>(item.Argument));
        }

        public override void Execute()
        {
            _logger.LogInformation("执行鸣枪警告联动动作.....");
        }

        public override void Stop()
        {

        }
    }
}
