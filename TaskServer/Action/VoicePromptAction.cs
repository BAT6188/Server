using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskServer.Argument;
using Newtonsoft.Json;
using AlarmAndPlan.Model;

namespace TaskServer.Action
{
    /// <summary>
    /// 执行语音提示动作
    /// </summary>
    internal class VoicePromptAction : PlanActionProvider
    {
        List<VoicePromptArgument> m_actionArgumentList;


        public override void AddPlanActionItem(PlanActionArgument argument)
        {
            if (m_actionArgumentList == null)
                m_actionArgumentList = new List<VoicePromptArgument>();
            m_actionArgumentList.Add(JsonConvert.DeserializeObject<VoicePromptArgument>(argument.Argument));
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
