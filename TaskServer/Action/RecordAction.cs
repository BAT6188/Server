using AlarmAndPlan.Model;
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
    internal sealed class RecordAction : PlanActionProvider
    {
        List<VideoRecordArgument> m_actionArgumentList;

        ILogger<RecordAction> _logger = Logger.CreateLogger<RecordAction>();

        public override void AddPlanActionItem(PlanActionArgument item)
        {
            if (m_actionArgumentList == null)
                m_actionArgumentList = new List<VideoRecordArgument>();
            try
            {
                var arg = JsonConvert.DeserializeObject<VideoRecordArgument>(item.Argument);
                m_actionArgumentList.Add(arg);
            }
            catch (Exception ex)
            {
                _logger.LogError("录像参数转换异常：{0}\r\n{1}", ex.Message, ex.StackTrace);
            }
        }

        public override void Execute()
        {
            Console.WriteLine("Run RecordAction............................");
        }

        public override void Stop()
        {
            Console.WriteLine("Stop RecordAction............................");
        }
    }
}
