using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using FluentScheduler;
using TCAdmin.Interfaces.Logging;
using TCAdmin.Interfaces.Server;
using TCAdmin.SDK;
using TCAdminCrons.Configuration;
using TCAdminCrons.Crons.GameUpdates;

namespace TCAdminCrons
{
    public class TcAdminCronsService : IMonitorService
    {
        public static Registry CronRegistry;

        public TcAdminCronsService()
        {
            this.ConfigurationKey = "TCAdminCrons";
        }

        public void Initialize(params object[] args)
        {
        }

        public void Start()
        {
            LogManager.Write("Starting TCAdmin Crons...", LogType.Console);
            
            Task.Run(() =>
            {
                var createdThreadConnection = TCAdmin.SDK.Database.DatabaseManager.CreateDatabaseManagerForThread();

                try
                {
                    this.Status = ServiceStatus.Starting;
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                    LogManager.Write(
                        "TCAdmin Crons - By Alexr03 | Contributors: M0RG4N01 | Version: " +
                        Assembly.GetExecutingAssembly().GetName().Version, LogType.Console);

                    CronRegistry = new Registry();
                    RegisterCrons();

                    Status = ServiceStatus.Running;
                }
                catch
                {
                    if (createdThreadConnection)
                    {
                        TCAdmin.SDK.Database.DatabaseManager.DisconnectThreadConnection();
                    }
                    Status = ServiceStatus.StartError;
                }
            });
            
            LogManager.Write("TCAdmin Crons has started.", LogType.Console);
        }

        public void Stop()
        {
            this.Status = ServiceStatus.Stopping;
            JobManager.RemoveAllJobs();
            JobManager.Stop();
            this.Status = ServiceStatus.Stopped;
        }

        public void Pause()
        {
            this.Stop();
        }

        public void Resume()
        {
            this.Start();
        }

        public void Restart()
        {
            this.Stop();
            this.Start();
        }

        public static void RegisterCrons()
        {
            LogManager.Write("Initializing Cron Registry", LogType.Console);
            var config = MinecraftCronConfiguration.GetConfiguration();

            CronRegistry.NonReentrantAsDefault();
            CronRegistry.Schedule<MinecraftVanillaUpdatesCron>().AndThen<MinecraftPaperUpdatesCron>()
                .AndThen<MinecraftSpigotUpdatesCron>().AndThen<MinecraftBukkitUpdatesCron>().ToRunNow().AndEvery(config.Seconds)
                .Seconds();

            JobManager.Initialize(CronRegistry);

            foreach (var schedule in JobManager.AllSchedules)
            {
                LogManager.Write($"{schedule.Name} has been scheduled to run at {schedule.NextRun:F}",
                    LogType.Console);
            }
        }

        public string ConfigurationKey { get; }
        public ServiceStatus Status { get; set; }
    }
}