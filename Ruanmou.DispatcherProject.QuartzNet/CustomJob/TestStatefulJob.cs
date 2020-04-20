using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ruanmou.DispatcherProject.QuartzNet.CustomJob
{
    /// <summary>
    /// 有状态的job：上一次执行的结果，能影响到下一次
    /// </summary>
    [PersistJobDataAfterExecution]//特性：执行后保留数据,更新JobDataMap
    [DisallowConcurrentExecution]//特性：拒绝同一时间重复执行，同一任务串行
    //但是并没有完成数据的更新？！！ 留给大家解决下。。
    public class TestStatefulJob : IJob//IStatefulJob
    {
        public TestStatefulJob()
        {
            Console.WriteLine("This is TestStatefulJob的构造。。。");
        }
        private static object TempData = new object();//或者存储到第三方
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() =>
            {
                Console.WriteLine();
                Console.WriteLine("*****************************");
                {
                    JobDataMap dataMap = context.JobDetail.JobDataMap;
                    Console.WriteLine(dataMap.Get("student1"));
                    Console.WriteLine(dataMap.Get("student2"));
                    Console.WriteLine(dataMap.Get("student3"));
                    Console.WriteLine(dataMap.GetInt("Year"));

                    dataMap.Put("Year", 2030);
                }
                Console.WriteLine($"This is {Thread.CurrentThread.ManagedThreadId} {DateTime.Now}");
                //可以换成去数据库查询，可以做啥啥啥
                //但是很多情况下，我们也是需要参数的
                {
                    JobDataMap dataMap = context.Trigger.JobDataMap;
                    Console.WriteLine(dataMap.Get("student4"));
                    Console.WriteLine(dataMap.Get("student5"));
                    Console.WriteLine(dataMap.Get("student6"));
                    Console.WriteLine(dataMap.GetInt("Year"));
                }
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
