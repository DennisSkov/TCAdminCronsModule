using Alexr03.Common.Logging;
using FluentScheduler;

namespace TCAdminCrons
{
    public abstract class TcAdminCronJob : IJob
    {
        protected TcAdminCronJob(Logger logger)
        {
            Logger = logger;
        }

        public Logger Logger { get; private set; }
        
        public abstract System.Threading.Tasks.Task DoAction();

        public void Execute()
        {
            lock (TcAdminCronsService.CronLock)
            {
                System.Threading.Tasks.Task.Run(async () => await DoAction()).Wait();
            }
        }
    }
}