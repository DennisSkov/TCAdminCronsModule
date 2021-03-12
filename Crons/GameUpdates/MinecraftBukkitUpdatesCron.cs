using System;
using System.Linq;
using System.Threading.Tasks;
using Alexr03.Common.Logging;
using TCAdmin.GameHosting.SDK.Objects;
using TCAdmin.Interfaces.Logging;
using TCAdmin.SDK;
using TCAdminCrons.Configuration;
using TCAdminCrons.Models.Minecraft.Bukkit;
using TCAdminCrons.Models.Objects;

namespace TCAdminCrons.Crons.GameUpdates
{
    public class MinecraftBukkitUpdatesCron : TcAdminCronJob
    {
        private BukkitSettings _bukkitSettings;

        public MinecraftBukkitUpdatesCron() : base()
        {
        }

        public override async System.Threading.Tasks.Task DoAction()
        {
            Logger.Information($"|------------------------|Log Initialised @ {DateTime.Now:s}|------------------------|");

            _bukkitSettings = new CronJob().FindByType(this.GetType()).Configuration.Parse<BukkitSettings>();

            if (!_bukkitSettings.Enabled)
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
                Logger.Fatal(e);
                throw;
            }
            finally
            {
                Logger.Information("|----------------------------------------------------------------------------------|");
            }
        }
        public void AddUpdatesForMcTemp()
        {
            var gameUpdates = GameUpdate.GetUpdates(_bukkitSettings.GameId).Cast<GameUpdate>().ToList();
            var bukkitUpdates = BukkitVersionManifest.GetManifests().Version;

            foreach (var version in bukkitUpdates.Take(_bukkitSettings.GetLastReleaseUpdates))
            {
                var gameUpdate = version.GetGameUpdate();
                if (!gameUpdates.Any(x => x.Name == gameUpdate.Name && x.GroupName == gameUpdate.GroupName))
                {
                    gameUpdate.Save();
                    Logger.Information($"Saved Game Update for {version.Version}");
                }
                else
                {
                    Logger.Information("Game Update already exists for " + version.Version);
                }
            }
        }
    }
}