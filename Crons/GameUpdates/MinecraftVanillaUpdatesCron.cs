using System;
using System.Linq;
using System.Threading.Tasks;
using Alexr03.Common.TCAdmin.Configuration;
using TCAdmin.GameHosting.SDK.Objects;
using TCAdmin.Interfaces.Logging;
using TCAdmin.SDK;
using TCAdminCrons.Configuration;
using TCAdminCrons.Models.Minecraft.Vanilla;
using TCAdminCrons.Models.Objects;

namespace TCAdminCrons.Crons.GameUpdates
{
    public class MinecraftVanillaUpdatesCron : TcAdminCronJob
    {
        private VanillaSettings _vanillaSettings;
        
        public MinecraftVanillaUpdatesCron() : base(Alexr03.Common.Logging.Logger.Create<MinecraftVanillaUpdatesCron>())
        {
        }

        public override async System.Threading.Tasks.Task DoAction()
        {
            Logger.LogMessage($"|------------------------|Log Initialised @ {DateTime.Now:s}|------------------------|");

            _vanillaSettings = new CronJob(1).GetConfiguration<VanillaSettings>();

            if (!_vanillaSettings.Enabled)
            {
                Logger.LogMessage("Disabled in Configuration.");
                return;
            }
            try
            {
                Logger.LogMessage("Running...");
                AddUpdatesForMcTemp();
            }
            catch (Exception e)
            {
                Logger.LogException(e);
                throw;
            }
            finally
            {
                Logger.LogMessage("|----------------------------------------------------------------------------------|");
            }
        }

        public void AddUpdatesForMcTemp()
        {
            var gameUpdates = GameUpdate.GetUpdates(_vanillaSettings.GameId).Cast<GameUpdate>().ToList();
            var snapshots = MinecraftVersionManifest.GetManifests().Versions
                .Where(x => x.Type.ToLower() == "snapshot").Take(_vanillaSettings.GetLastSnapshotUpdates);

            var releases = MinecraftVersionManifest.GetManifests().Versions
                .Where(x => x.Type.ToLower() == "release").Take(_vanillaSettings.GetLastReleaseUpdates);

            foreach (var metaData in snapshots.Select(version => version.GetMetadata()))
            {
                var gameUpdate = metaData.CreateGameUpdate();
                if (!gameUpdates.Any(x => x.Name == gameUpdate.Name && x.GroupName == gameUpdate.GroupName))
                {
                    gameUpdate.Save();
                    Logger.LogMessage($"Saved Game Update for {metaData.Id}");
                }
                else
                {
                    Logger.LogMessage("Game Update already exists for " + metaData.Id);
                }
            }

            foreach (var metaData in releases.Select(version => version.GetMetadata()))
            {
                var gameUpdate = metaData.CreateGameUpdate();
                if (!gameUpdates.Any(x => x.Name == gameUpdate.Name && x.GroupName == gameUpdate.GroupName))
                {
                    gameUpdate.Save();
                    Logger.LogMessage($"Saved Game Update for {metaData.Id}");
                }
                else
                {
                    Logger.LogMessage("Game Update already exists for " + metaData.Id);
                }
            }
        }
    }
}