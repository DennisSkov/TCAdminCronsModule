using Alexr03.Common.Logging;
using FluentScheduler;

namespace TCAdminCrons
{
    public abstract class TcAdminCronJob : IJob
    {
        protected TcAdminCronJob()
        {
            Logger = Logger.Create(GetType());
        }

        public Logger Logger { get; }
        
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