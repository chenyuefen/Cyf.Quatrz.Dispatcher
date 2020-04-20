using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using Quartz.Simpl;
using Quartz.Xml;
using Ruanmou.DispatcherProject.QuartzNet.CustomJob;
using Ruanmou.DispatcherProject.QuartzNet.CustomListener;
using Ruanmou.DispatcherProject.QuartzNet.CustomLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.DispatcherProject.QuartzNet
{
    /// <summary>
    /// 1 nuget添加引用
    /// 
    /// 2 三大核心对象:
    ///    IScheduler:单元/实例，在这里去完成定时任务的配置
    ///    只有单元启动，里面的作业才能正常运行
    ///    IJob:任务，定时执行动作就是Job
    ///    ITrigger：定时策略 
    ///    就可以完成基本的定时任务
    ///    
    /// 3  传参数问题
    ///      a)jobDetail.JobDataMap.Add
    ///      b) trigger.JobDataMap.Add
    ///      c) 要注意，使用MergedJobDataMap有覆盖，后者为准
    ///      
    /// 4 为啥是job+trigger(更灵活)
    ///   拆分真的挺好，方便复用，一个job绑定多个trigger
    ///   刚才的传参数，就也应该分开一下
    ///   
    ///   归档数据的任务(业务表+归档表--把30天之前的数据都移到归档表)
    ///   订单表(30天)---死循环Task--检查日期---做一次归档
    ///   物流表(60天)---死循环Task--检查日期---做一次归档
    ///   。。。。100个类似需求
    ///   希望作业能通用，表不一样，操作差不多，job完成通用逻辑
    ///   需要业务表+归档表，这个由IJobDetail传递参数来确定
    ///  
    ///   不同的detail可能频率不同，所以拆分成2个
    /// 
    ///   执行频率不同的，就需要不同的trigger，这个时候可能条件是不一样的
    ///   按天执行，就只检测day；
    ///   按小时执行，就得细致到Hour；
    ///   按分钟执行，会细致到minute
    ///   所以trigger也得传参数
    ///   
    /// 5 常用Trigggr：
    ///      SimpleTrigger:从什么时间开始，间隔多久执行重复操作，可以限制最大次数
    ///      Cron：表达式的方式，可以灵活订制时间规则(详情见文档)
    ///      
    /// 6 Listener框架的各个环节--事件能做的监听
    ///   CustomSchedulerListener
    ///   CustomTriggerListener
    ///   CustomJobListener
    ///   
    /// 7 LogProvider可以展示框架运行的一些信息
    /// </summary>
    public class DispatcherManager
    {
        public async static Task Init()
        {
            #region 自定义框架日志
            LogProvider.SetCurrentLogProvider(new CustomConsoleLogProvider());
            #endregion

            #region scheduler
            Console.WriteLine("初始化scheduler......");
            //StdSchedulerFactory factory = new StdSchedulerFactory();
            //IScheduler scheduler = await factory.GetScheduler();
            IScheduler scheduler = await ScheduleManager.BuildScheduler();

            {
                //使用配置文件
                XMLSchedulingDataProcessor processor = new XMLSchedulingDataProcessor(new SimpleTypeLoadHelper());
                await processor.ProcessFileAndScheduleJobs("~/CfgFiles/quartz_jobs.xml", scheduler);
            }


            scheduler.ListenerManager.AddSchedulerListener(new CustomSchedulerListener());
            scheduler.ListenerManager.AddTriggerListener(new CustomTriggerListener());
            scheduler.ListenerManager.AddJobListener(new CustomJobListener());
            await scheduler.Start();
            #endregion

            //IJob  ITrigger
            //{
            //    //创建作业
            //    //IJobDetail jobDetail = JobBuilder.Create<TestJob>()
            //    //    .WithIdentity("testjob", "group1")
            //    //    .WithDescription("This is TestJob")
            //    //    .Build();

            //    IJobDetail jobDetail = JobBuilder.Create<TestStatefulJob>()
            //        .WithIdentity("testjob", "group1")
            //        .WithDescription("This is TestJob")
            //        .Build();

            //    jobDetail.JobDataMap.Add("student1", "Milor");
            //    jobDetail.JobDataMap.Add("student2", "心如迷醉");
            //    jobDetail.JobDataMap.Add("student3", "宇洋");
            //    jobDetail.JobDataMap.Add("Year", DateTime.Now.Year);

            //ITrigger trigger = TriggerBuilder.Create()
            //       .WithIdentity("trigger1", "group1")
            //       .StartNow()
            //       .WithSimpleSchedule(x => x
            //           .WithIntervalInSeconds(10)
            //           .WithRepeatCount(10)
            //           .RepeatForever())
            //           .WithDescription("This is testjob's Trigger")
            //       .Build();

            //    //创建时间策略
            //    ITrigger trigger = TriggerBuilder.Create()
            //                  .WithIdentity("testtrigger1", "group1")
            //                  .StartAt(new DateTimeOffset(DateTime.Now.AddSeconds(10)))
            //                 //.StartNow()//StartAt
            //                 .WithCronSchedule("5/10 * * * * ?")//每隔一分钟
            //                                                    //"10,20,30,40,50,0 * * * * ?"
            //                 .WithDescription("This is testjob's Trigger")
            //                 .Build();

            //    trigger.JobDataMap.Add("student4", "Ray");
            //    trigger.JobDataMap.Add("student5", "心欲无痕");
            //    trigger.JobDataMap.Add("student6", "风在飘动");
            //    trigger.JobDataMap.Add("Year", DateTime.Now.Year + 1);

            //    await scheduler.ScheduleJob(jobDetail, trigger);
            //    Console.WriteLine("scheduler作业添加完成1......");
            //}
            //{
            //    IJobDetail jobDetail = JobBuilder.Create<GoodJob>()
            //      .WithIdentity("GoodJob", "软谋教育高级班")
            //      .WithDescription("This is 软谋教育高级班的GoodJob")
            //      .Build();

            //    ITrigger trigger = TriggerBuilder.Create()
            //                  .WithIdentity("GoodJobTrigger1", "软谋教育高级班")
            //                  .StartAt(new DateTimeOffset(DateTime.Now.AddSeconds(10)))
            //                 .WithCronSchedule("3/20 * * * * ?")
            //                 .WithDescription("This is GoodJob's Trigger")
            //                 .Build();

            //    await scheduler.ScheduleJob(jobDetail, trigger);
            //    Console.WriteLine("scheduler作业添加完成2......");
            //}
        }
    }
}
