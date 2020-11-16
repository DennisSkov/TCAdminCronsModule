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
        
        public MinecraftVanillaSnapshotUpdatesCron() : base()
        {
        }

        public override async System.Threading.Tasks.Task DoAction()
        {
            Logger.Information($"|------------------------|Log Initialised @ {DateTime.Now:s}|------------------------|");

            _vanillaSnapshotSettings = new CronJob().FindByType(this.GetType()).Configuration.Parse<VanillaSnapshotSettings>();

            if (!_vanillaSnapshotSettings.Enabled)
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
            var gameUpdates = GameUpdate.GetUpdates(_vanillaSnapshotSettings.GameId).Cast<GameUpdate>().ToList();
            var releases = MinecraftVersionManifest.GetManifests().Versions
                .Where(x => x.Type.ToLower() == "snapshot").Take(_vanillaSnapshotSettings.GetLastReleaseUpdates);

            foreach (var metaData in releases.Select(version => version.GetMetadata()))
            {
                var gameUpdate = metaData.CreateGameUpdateSnapshot();
                if (!gameUpdates.Any(x => x.Name == gameUpdate.Name && x.GroupName == gameUpdate.GroupName))
                {
                    gameUpdate.Save();
                    Logger.Information($"Saved Game Update for {metaData.Id}");
                }
                else
                {
                    Logger.Information("Game Update already exists for " + metaData.Id);
                }
            }
        }
    }
}