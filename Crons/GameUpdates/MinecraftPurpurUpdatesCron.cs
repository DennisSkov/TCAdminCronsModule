using System;
using System.Linq;
using TCAdmin.GameHosting.SDK.Objects;
using TCAdminCrons.Configuration;
using TCAdminCrons.Models.Minecraft.Purpur;
using TCAdminCrons.Models.Objects;

namespace TCAdminCrons.Crons.GameUpdates
{
    public class MinecraftPurpurUpdatesCron : TcAdminCronJob
    {
        private PurpurSettings _purpurSettings;

        public MinecraftPurpurUpdatesCron() : base()
        {
        }

        public override async System.Threading.Tasks.Task DoAction()
        {
            Logger.Information($"|------------------------|Log Initialised @ {DateTime.Now:s}|------------------------|");

            _purpurSettings = new CronJob().FindByType(this.GetType()).Configuration.Parse<PurpurSettings>();

            if (!_purpurSettings.Enabled)
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
            var gameUpdates = GameUpdate.GetUpdates(_purpurSettings.GameId).Cast<GameUpdate>().ToList();
            var purpurUpdates = PurpurVersionManifest.GetManifests().Version;

            foreach (var version in purpurUpdates.Take(_purpurSettings.GetLastReleaseUpdates))
            {
                var gameUpdate = version.GetGameUpdate();
                if (!gameUpdates.Any(x => x.Name == gameUpdate.Name && x.GroupName == gameUpdate.GroupName))
                {
                    gameUpdate.Save();
                    Logger.Information($"[Minecraft Purpur Update Cron] Saved Game Update for {version.Version}");
                }
                else
                {
                    Logger.Information("[Minecraft Purpur Update Cron] Game Update already exists for " + version.Version);
                }
            }
        }
    }
}
