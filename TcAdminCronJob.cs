using Alexr03.Common.Logging;
using Alexr03.Common.TCAdmin.Logging;
using FluentScheduler;

namespace TCAdminCrons
{
    public abstract class TcAdminCronJob : IJob
    {
        protected TcAdminCronJob()
        {
            Logger = LogManager.Create(GetType());
        }

        public LogManager Logger { get; }
        
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