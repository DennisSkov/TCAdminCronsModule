using System;
using System.Linq;
using Alexr03.Common.Logging;
using TCAdmin.GameHosting.SDK.Objects;
using TCAdmin.SDK;
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
            var purpurUpdates = PurpurManifest.GetManifest();

            foreach (var version in purpurUpdates.Versions.Take(_purpurSettings.GetLastReleaseUpdates))
            {
                var gameUpdate = PurpurManifest.GetGameUpdate(version);
                if (!gameUpdates.Any(x => x.Name == gameUpdate.Name && x.GroupName == gameUpdate.GroupName))
                {
                    gameUpdate.Save();
                    Logger.Information($"Saved Game Update for {version}");
                }
                else
                {
                    Logger.Information("Game Update already exists for " + version);
                }
            }
        }
    }
}
