using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ruanmou.DispatcherProject.QuartzNet.CustomListener
{
    public class CustomJobListener : IJobListener
    {
        public string Name => "CustomJobListener";

        public async Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.Run(()=> {
                Console.WriteLine();
                Console.WriteLine($"CustomJobListener JobExecutionVetoed {context.JobDetail.Description}");
            });
        }

        public async Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.Run(() => {
                Console.WriteLine();
                Console.WriteLine($"CustomJobListener JobToBeExecuted {context.JobDetail.Description}");
            });
        }

        public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.Run(() => {
                Console.WriteLine();
                Console.WriteLine($"CustomJobListener JobWasExecuted {context.JobDetail.Description}");
            });
        }
    }
}
