using System;
using System.Linq;
using Alexr03.Common.Logging;
using TCAdmin.GameHosting.SDK.Objects;
using TCAdmin.SDK;
using TCAdminCrons.Configuration;
using TCAdminCrons.Models.Minecraft.Paper;
using TCAdminCrons.Models.Objects;

namespace TCAdminCrons.Crons.GameUpdates
{
    public class MinecraftPaperUpdatesCron : TcAdminCronJob
    {
        private PaperSettings _paperSettings;

        public MinecraftPaperUpdatesCron() : base()
        {
        }

        public override async System.Threading.Tasks.Task DoAction()
        {
            Logger.Information($"|------------------------|Log Initialised @ {DateTime.Now:s}|------------------------|");

            _paperSettings = new CronJob(4).Configuration.Parse<PaperSettings>();

            if (!_paperSettings.Enabled)
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
            var gameUpdates = GameUpdate.GetUpdates(_paperSettings.GameId).Cast<GameUpdate>().ToList();
            var paperUpdates = PaperManifest.GetManifest();

            foreach (var version in paperUpdates.Versions.Take(_paperSettings.GetLastReleaseUpdates))
            {
                var gameUpdate = PaperManifest.GetGameUpdate(version);
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