using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Quartz;

namespace Ruanmou.DispatcherProject.QuartzNet.CustomJob
{
    public class GoodJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() =>
            {
                Console.WriteLine();

                Console.WriteLine($"This is GoodJob start {DateTime.Now.ToLongDateString()}");
                Thread.Sleep(1000);
                Console.WriteLine($"This is GoodJob   end {DateTime.Now.ToLongDateString()}");
                Console.WriteLine();
            });
        }
    }
}
