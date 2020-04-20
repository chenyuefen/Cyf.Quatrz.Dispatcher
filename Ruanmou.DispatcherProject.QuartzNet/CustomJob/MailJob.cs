using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ruanmou.DispatcherProject.QuartzNet.CustomJob
{
    [PersistJobDataAfterExecution]
    [DisallowConcurrentExecution]
    public class MailJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(
               () =>
               {
                   try
                   {
                       Console.WriteLine($"MailJob 当前线程ID：{Thread.CurrentThread.ManagedThreadId}");

                       JobDataMap data1 = context.JobDetail.JobDataMap;
                       Console.WriteLine($"JobDetail :{data1.GetString("aaa2")}");
                       Console.WriteLine($"JobDetail :{data1.GetInt("bbb2")}");
                       Console.WriteLine($"JobDetail :{data1.GetString("ccc2")}");

                       JobDataMap data2 = context.Trigger.JobDataMap;
                       Console.WriteLine($"Trigger :{data2.GetString("ddd2")}");

                       JobDataMap data3 = context.MergedJobDataMap;
                       Console.WriteLine($"JobDetail :{data3.GetString("aaa2")}");
                       Console.WriteLine($"JobDetail :{data3.GetInt("bbb2")}");
                       Console.WriteLine($"JobDetail :{data3.GetString("ccc2")}");
                       Console.WriteLine($"JobDetail :{data3.GetString("ddd2")}");


                       int iCount = data3.GetInt("count");
                       Thread.Sleep(1000);
                       Console.WriteLine($"This is {iCount++}次执行 MailJob ");
                       data3.Put("count", iCount);
                   }
                   catch (Exception ex)
                   {
                       Console.WriteLine(ex.Message);
                   }
               });
        }
    }
}
