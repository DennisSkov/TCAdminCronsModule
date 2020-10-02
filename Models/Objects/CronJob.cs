using System.Collections.Generic;
using System.Linq;
using Alexr03.Common.TCAdmin.Objects;
using TCAdmin.Interfaces.Database;

namespace TCAdminCrons.Models.Objects
{
    public class CronJob : DynamicTypeBase
    {
        public CronJob() : base("tcmodule_cron_jobs")
        {
            this.SetValue("id", -1);
        }

        public CronJob(int id) : this()
        {
            this.SetValue("id", id);
            this.ValidateKeys();
            if (!this.Find())
            {
                throw new KeyNotFoundException("Could not find CronJob with Id: " + id);
            }
        }

        public int ExecuteOrder
        {
            get => this.GetIntegerValue("executeOrder");
            set => this.SetValue("executeOrder", value);
        }

        public int ExecuteEverySeconds
        {
            get => this.GetIntegerValue("executeEverySeconds");
            set => this.SetValue("executeEverySeconds", value);
        }

        public void ExecuteCron()
        {
            var tcAdminCronJob = this.Create<TcAdminCronJob>();
            tcAdminCronJob.Execute();
        }
        
        public string GetLogFile()
        {
            var tcAdminCronJob = this.Create<TcAdminCronJob>();
            return tcAdminCronJob.Logger.GetCurrentLogFile().FullName;
        }

        public static List<CronJob> GetCronJobs()
        {
            return new CronJob().GetObjectList(new WhereList()).Cast<CronJob>().OrderBy(x => x.ExecuteOrder).ToList();
        }
    }
}