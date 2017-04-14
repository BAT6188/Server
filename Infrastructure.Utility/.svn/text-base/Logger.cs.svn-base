using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace Infrastructure.Utility
{
    public class Logger
    {
        public Logger()
        {
        }
        private static ILoggerFactory LoggerFactory { get; } = new LoggerFactory().AddConsole().AddNLog();

        public static ILogger<T> CreateLogger<T>()
        {
            return LoggerFactory.CreateLogger<T>();
        }
    }
}
