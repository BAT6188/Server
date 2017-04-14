using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Infrastructure.Utility
{
    public class GlobalSetting
    {
        static GlobalSetting()
        {
            PlanActionOptions = new Dictionary<string, string>();

            var builder = new ConfigurationBuilder()
                 //.SetBasePath(env.ContentRootPath)
                 .AddJsonFile("globalsetting.json", optional: true, reloadOnChange: true)
                 .AddEnvironmentVariables();
            var config = builder.Build();

            //store appsetting in memory
            IEnumerable<IConfigurationSection> actionSections = config.GetSection("ActionProvider").GetChildren();
            foreach (IConfigurationSection secion in actionSections)
            {
                PlanActionOptions[secion.Value] = secion.Key;
            }

            //app server 
            AppServerBaseUrl = config["AppServer:BaseUrl"].ToString();
            WebBuilderPort = Int32.Parse(config["WebBuilder:Port"].ToString());
            ConnectionString = config["AppServer:ConnectionString"].ToString();

            //mq host
            MqBrokerHost = config["MQ:BrokerHost"].ToString();

            TaskServerBaseUrl = config["TaskServer:BaseUrl"].ToString();

            DcpBaseUrl = config["DCP:BaseUrl"].ToString();

            SearchServerTimeout = Int32.Parse(config["AppServer:SearchServerTimeout"].ToString());
        }

        /// <summary>
        /// 预案动作执行
        /// </summary>
        public static Dictionary<string, string> PlanActionOptions
        {
            get; set;
        }

        /// <summary>
        /// 应用服务器访问root url
        /// </summary>
        public static string AppServerBaseUrl
        {
            get; set;
        }

        /// <summary>
        /// 服务器Http监听端口
        /// </summary>
        public static int WebBuilderPort
        {
            get; set;
        }

        public static string MqBrokerHost
        {
            get; set;
        }

        /// <summary>
        /// 哨位台中心服务访问root url
        /// </summary>
        public static string PostServerBaseUrl
        {
            get; set;
        }

        /// <summary>
        /// 任务服务器访问url
        /// </summary>
        public static string TaskServerBaseUrl
        {
            get;set;
        }

        public static string ConnectionString
        {
            get;set;
        }

        public static Dictionary<Guid, string> AlarmTextDict
        {
            get;set;
        }

        /// <summary>
        /// DCP基础连接
        /// </summary>
        public static string DcpBaseUrl
        {
            get;set;
        }

        /// <summary>
        /// 服务器搜索等待时间
        /// </summary>
        public static int SearchServerTimeout
        {
            get;set;
        }
    }
}
