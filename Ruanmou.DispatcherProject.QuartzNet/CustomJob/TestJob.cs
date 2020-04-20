using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ruanmou.DispatcherProject.QuartzNet.CustomJob
{
    public class TestJob : IJob
    {
        public TestJob()
        {
            Console.WriteLine("This is TestJob的构造。。。");
        }


        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() =>
            {
                Console.WriteLine();
                //JobDetail接收参数
                Console.WriteLine("*****************************");
                {
                    JobDataMap dataMap = context.JobDetail.JobDataMap;
                    //取参数
                    Console.WriteLine(dataMap.Get("student1"));
                    Console.WriteLine(dataMap.Get("student2"));
                    Console.WriteLine(dataMap.Get("student3"));
                    Console.WriteLine(dataMap.GetInt("Year"));
                }
                Console.WriteLine($"This is {Thread.CurrentThread.ManagedThreadId} {DateTime.Now}");
                //可以换成去数据库查询，可以做啥啥啥
                //但是很多情况下，我们也是需要参数的
                //Trigger接收参数
                {
                    JobDataMap dataMap = context.Trigger.JobDataMap;
                    Console.WriteLine(dataMap.Get("student4"));
                    Console.WriteLine(dataMap.Get("student5"));
                    Console.WriteLine(dataMap.Get("student6"));
                    Console.WriteLine(dataMap.GetInt("Year"));
                }
                //两者合并接收参数/一样的key后面会被覆盖
                {
                    Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                    JobDataMap dataMap = context.MergedJobDataMap;
                    Console.WriteLine(dataMap.Get("student1"));
                    Console.WriteLine(dataMap.Get("student2"));
                    Console.WriteLine(dataMap.Get("student3"));
                    Console.WriteLine(dataMap.Get("student4"));
                    Console.WriteLine(dataMap.Get("student5"));
                    Console.WriteLine(dataMap.Get("student6"));
                    Console.WriteLine(dataMap.GetInt("Year"));
                }
                Console.WriteLine("*****************************");
                Console.WriteLine();
            });
        }
    }
}
