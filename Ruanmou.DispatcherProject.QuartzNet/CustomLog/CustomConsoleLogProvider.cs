using Quartz.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.DispatcherProject.QuartzNet.CustomLog
{
    /// <summary>
    /// 自定义框架日志
    /// </summary>
    public class CustomConsoleLogProvider : ILogProvider
    {
        public Logger GetLogger(string name)
        {
            return new Logger((level, func, exception, parameters) =>
            {
                if (level >= LogLevel.Info && func != null)
                {
                    Console.WriteLine($"[{ DateTime.Now.ToLongTimeString()}] [{ level}] { func()} {string.Join(";", parameters.Select(p => p == null ? " " : p.ToString()))}  自定义日志{name}");
                }
                return true;
            });
        }
        public IDisposable OpenNestedContext(string message)
        {
            throw new NotImplementedException();
        }

        public IDisposable OpenMappedContext(string key, string value)
        {
            throw new NotImplementedException();
        }
    }
}
