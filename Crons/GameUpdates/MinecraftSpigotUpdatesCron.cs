using System;
using System.Linq;
using TCAdmin.GameHosting.SDK.Objects;
using TCAdminCrons.Configuration;
using TCAdminCrons.Models.Minecraft.Spigot;
using TCAdminCrons.Models.Objects;

namespace TCAdminCrons.Crons.GameUpdates
{
    public class MinecraftSpigotUpdatesCron : TcAdminCronJob
    {
        private SpigotSettings _spigotSettings;

        public MinecraftSpigotUpdatesCron() : base()
        {
        }

        public override async System.Threading.Tasks.Task DoAction()
        {
            Logger.Information($"|------------------------|Log Initialised @ {DateTime.Now:s}|------------------------|");
            
            _spigotSettings = new CronJob(5).Configuration.Parse<SpigotSettings>();
            if (!_spigotSettings.Enabled)
            {
                Logger.Information("Disabled in Configuration.");
                return;
            }
            try
            {
                Logger.Information("Running...");
                AddUpdatesForMcTemp();
            }
            catch (Exception e)
            {
                Logger.LogException(e);
                throw;
            }
            finally
            {
                Logger.Information("|----------------------------------------------------------------------------------|");
            }
        }
        public void AddUpdatesForMcTemp()
        {
            var gameUpdates = GameUpdate.GetUpdates(_spigotSettings.GameId).Cast<GameUpdate>().ToList();
            var spigotUpdates = SpigotVersionManifest.GetManifests().Version;

            foreach (var version in spigotUpdates.Take(_spigotSettings.GetLastReleaseUpdates))
            {
                var gameUpdate = version.GetGameUpdate();
                if (!gameUpdates.Any(x => x.Name == gameUpdate.Name && x.GroupName == gameUpdate.GroupName))
                {
                    gameUpdate.Save();
                    Logger.Information($"[Minecraft Spigot Update Cron] Saved Game Update for {version.Version}");
                }
                else
                {
                    Logger.Information("[Minecraft Spigot Update Cron] Game Update already exists for " + version.Version);
                }
            }
        }
    }
}