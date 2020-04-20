using Ruanmou.DispatcherProject.QuartzNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.DispatcherProject
{
    /// <summary>
    /// 1 quartZ引入&初始化&使用  
    /// 2 核心对象Job、Trigger解析
    /// 3 三种Listener扩展订制
    /// 
    /// 定时调度：大半夜跑一些数据统计，排行榜
    ///           数据同步---id&name更新--lucene索引更新
    ///           其实跟数据库作业很像，但是这个不只是数据库
    /// 
    /// QuartZ.Net3.0+
    /// 
    /// 
    /// 1 定时任务可视化界面管理
    /// 2 配置文件使用和IOC容器结合
    /// 3 WindowsService应用
    /// 
    /// 可视化管理工具：就是为了解决定时任务执行过程中，需要监控--人工介入这种需求
    ///           Web系统(只能运行在当前服务器)
    /// a 建立web项目--4.5.2以上版本--
    /// b 网站添加quartz--CrystalQuartz.Remote
    ///   webconfig有个SchedulerHost---网站和服务交互渠道---需要在定时Scheduler启动时做好配置
    /// c 定时任务的SchedulerFactory完成配置
    /// d 二者端口统一；如果监听不到，或者防火墙
    /// 
    /// 
    /// 配置文件：把trigger和job都通过配置文件指定
    ///           初始化Scheduler使用XMLSchedulingDataProcessor
    ///           
    /// 结合起来，开发者需要写的，就是一个Job业务
    ///            再就是做好配置文件 job-trigger
    ///           负责人做下可视化的配置，加上初始化Scheduler
    ///           可以扩展下日志--listener
    ///           
    /// QuartZ跟网站结合起来的例子，有，但是我几乎没看过
    /// MVC+WebApi+WebService 都是寄宿在IIS，而IIS会定时回收进程池
    /// (假设默认没有请求--30分钟就回收--再请求再启动)
    /// 整点的第1分钟执行操作，所以是无法保障的
    /// 合适的工具做合适的事儿，一般会把服务独立，windows service
    /// 
    /// 历史数据归档---Order&OrderHistory---00:30--定时操作
    /// 发邮件---618节日促销---100w用户都邮件一遍---多个邮箱服务器多时段
    /// 排行数据  报表  数据更新 同步。。。
    /// 
    /// Windows Services 非常适合做定时服务---还有托管WCF
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("欢迎来到.Net高级班VIP课程，今晚是Eleven老师带来的QuartZ.Net定时调度");
                DispatcherManager.Init().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}
