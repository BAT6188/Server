using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.IO;
using Microsoft.AspNet.Hosting.Internal;
using System.Diagnostics;

namespace MainServer
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
           
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin();
            corsBuilder.AllowCredentials();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", corsBuilder.Build());
            });

            services.AddMvc();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var appFileName = Process.GetCurrentProcess().MainModule.FileName;
            var appFilePath = Path.GetDirectoryName(appFileName);
            loggerFactory.AddNLog();
            env.ConfigureNLog("nlog.config");
            loggerFactory.CreateLogger<Program>().LogInformation("-----------------------------Start Application Server-----------------------------");
            app.UseCors("AllowAll");
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            string uiFilePath = Path.Combine(appFilePath, "PAPS.UI");
            Console.WriteLine(uiFilePath);
            app.UseStaticFiles(
                new StaticFileOptions() {
                    FileProvider = new PhysicalFileProvider(uiFilePath),
                    ServeUnknownFileTypes = true,
                    RequestPath=new PathString("/ui")
                }
                );

            //截图等附件
            uiFilePath = Path.Combine(appFilePath, "Attach");
            Console.WriteLine(uiFilePath);
            app.UseStaticFiles(
                new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(uiFilePath),
                    ServeUnknownFileTypes = true,
                    RequestPath = new PathString("/attach")
                }
                );
        }
    }
}
