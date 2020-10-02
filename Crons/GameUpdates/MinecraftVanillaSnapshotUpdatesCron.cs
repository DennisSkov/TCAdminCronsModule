using System;
using System.Linq;
using TCAdmin.GameHosting.SDK.Objects;
using TCAdminCrons.Configuration;
using TCAdminCrons.Models.Minecraft.Vanilla;
using TCAdminCrons.Models.Objects;

namespace TCAdminCrons.Crons.GameUpdates
{
    public class MinecraftVanillaSnapshotUpdatesCron : TcAdminCronJob
    {
        private VanillaSnapshotSettings _vanillaSnapshotSettings;
        
        public MinecraftVanillaSnapshotUpdatesCron() : base(Alexr03.Common.Logging.Logger.Create<MinecraftVanillaSnapshotUpdatesCron>())
        {
        }

        public override async System.Threading.Tasks.Task DoAction()
        {
            Logger.LogMessage($"|------------------------|Log Initialised @ {DateTime.Now:s}|------------------------|");

            _vanillaSnapshotSettings = new CronJob(5).Configuration.Parse<VanillaSnapshotSettings>();

            if (!_vanillaSnapshotSettings.Enabled)
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
            var gameUpdates = GameUpdate.GetUpdates(_vanillaSnapshotSettings.GameId).Cast<GameUpdate>().ToList();
            var releases = MinecraftVersionManifest.GetManifests().Versions
                .Where(x => x.Type.ToLower() == "snapshot").Take(_vanillaSnapshotSettings.GetLastReleaseUpdates);

            foreach (var metaData in releases.Select(version => version.GetMetadata()))
            {
                var gameUpdate = metaData.CreateGameUpdateSnapshot();
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