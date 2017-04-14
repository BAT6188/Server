using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskServer.Argument;
using AlarmAndPlan.Model;

namespace TaskServer.Action
{
    internal class GISAction : PlanActionProvider
    {
        private List<GISLocationArgument> m_actionArgumentList = null;

        public override void AddPlanActionItem(PlanActionArgument argument)
        {
            if (m_actionArgumentList == null)
                m_actionArgumentList = new List<GISLocationArgument>();
            m_actionArgumentList.Add(JsonConvert.DeserializeObject<GISLocationArgument>(argument.Argument));
        }

        public override void Execute()
        {
        }

        public override void Stop()
        {
        }
    }
}
