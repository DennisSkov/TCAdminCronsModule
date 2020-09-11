using System;
using System.Threading.Tasks;
using FluentScheduler;

namespace TCAdminCrons
{
    public abstract class TcAdminCronJob : IJob
    {
        public abstract System.Threading.Tasks.Task DoAction();

        public void Execute()
        {
            System.Threading.Tasks.Task.Run(async () => await DoAction()).Wait();
        }
    }
}