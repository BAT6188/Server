using HttpClientEx;
using Infrastructure.Model;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using PAPS.Data;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskServer.Argument;

namespace TaskServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string json = "{\"videoDeviceId\":\"ebe5518c-22bb-44cc-a768-fb22d1720d9c\",\"VideoStorageServerId\":\"\",\"recordTimeout\":\"\",\"streamType\":true}";
            var t = JsonConvert.DeserializeObject<VideoRecordArgument>(json);
            //Console.ReadLine();
            //return;

            var host = new WebHostBuilder().UseKestrel()
                .UseUrls(string.Format("http://*:{0}",GlobalSetting.WebBuilderPort))
.UseStartup<Startup>()
.Build();
            //启动预案任务
            PlanTaskScheduler.Instance.Start();
            host.Run();
        }
    }

}


