using AlarmAndPlan.Model;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlarmAndPlan.Services
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AlarmLog al = new AlarmLog() {
                AlarmLogId = Guid.NewGuid(),
                AlarmSourceId = Guid.NewGuid(),
                Description = "移动侦测报警.....",
                AlarmTypeId =Guid.Parse("A0002016-E009-B019-E001-ABCD00001001"),
                TimeCreated = DateTime.Now,
                AlarmLevelId = Guid.Parse("A0002016-E009-B019-E001-ABCD12900001"),
            };

            Console.WriteLine(JsonConvert.SerializeObject(al));
            Console.ReadLine();

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();
            host.Run();
        }
    }
}
