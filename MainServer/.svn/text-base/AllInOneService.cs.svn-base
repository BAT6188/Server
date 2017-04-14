using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DasMulli.Win32.ServiceUtils;
using Microsoft.AspNetCore.Hosting;
using AlarmAndPlan.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Infrastructure.Utility;
using Microsoft.Extensions.Configuration;
using TaskServer;
using AllInOneContext;
using PAPS.Services;

namespace MainServer
{
    public class AllInOneService: IWin32Service
    {
        
        private IWebHost WebHost;
        public string ServiceName => "AllInOne";

        private bool StopRequestedByWindows = false;

        ILogger<AllInOneService> _logger = Logger.CreateLogger<AllInOneService>();

        public void Start(string[] startupArguments, ServiceStoppedCallback serviceStoppedCallback)
        {
            //Task.Delay(20000).Wait();
            _logger.LogInformation("-----------------------------Start  All In One Service-----------------------------");
            _logger.LogInformation("-----------------------------Run ForwardAlarmLogTask-----------------------------");
            MyMigration.Migrate();

            //启动报警上传检测
            ForwardAlarmLogTask.Instance.Run();

            //启动预案任务
            PlanTaskScheduler.Instance.Start();

            //启动查勤包生成线程
            DutyCheckPackageRunner.Instance.Start();

            WebHost = new WebHostBuilder().UseKestrel()
                .UseUrls("http://*:5001")
                .UseStartup<Startup>()
                .Build();

            WebHost
              .Services
              .GetRequiredService<IApplicationLifetime>()
              .ApplicationStopped
              .Register(() =>
              {
                  if (StopRequestedByWindows == false)
                  {
                      serviceStoppedCallback();
                  }
              });
            _logger.LogInformation("-----------------------------Run WebHostBuilder-----------------------------");
            WebHost.Start(); //注意：以服务的方式启动不要用扩展方法Run(),避免服务不能完全启动。（可能是阻塞导致）
        }

        public void Stop()
        {
            StopRequestedByWindows = true;
            WebHost.Dispose();
        }
    }
}
