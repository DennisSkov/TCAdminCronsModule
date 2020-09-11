using System;
using System.Linq;
using System.Threading.Tasks;
using TCAdmin.GameHosting.SDK.Objects;
using TCAdmin.Interfaces.Logging;
using TCAdmin.SDK;
using TCAdminCrons.Configuration;
using TCAdminCrons.Models.Minecraft.Vanilla;

namespace TCAdminCrons.Crons.GameUpdates
{
    public class MinecraftVanillaUpdatesCron : TcAdminCronJob
    {
        private readonly MinecraftCronConfiguration _minecraftCronConfiguration = MinecraftCronConfiguration.GetConfiguration();

        public override async System.Threading.Tasks.Task DoAction()
        {
            if (!_minecraftCronConfiguration.VanillaSettings.Enabled)
            {
                LogManager.Write("[Minecraft Vanilla Update Cron] - Disabled in Configuration.", LogType.Console);
                return;
            }
            try
            {
                LogManager.Write("[Minecraft Vanilla Update Cron] Running...", LogType.Console);
                AddUpdatesForMcTemp();
            }
            catch (Exception e)
            {
                LogManager.WriteError(e, e.Message);
                throw;
            }
        }

        public void AddUpdatesForMcTemp()
        {
            var gameUpdates = GameUpdate.GetUpdates(_minecraftCronConfiguration.GameId).Cast<GameUpdate>().ToList();
            var snapshots = MinecraftVersionManifest.GetManifests().Versions
                .Where(x => x.Type.ToLower() == "snapshot").Take(_minecraftCronConfiguration.VanillaSettings.GetLastSnapshotUpdates);

            var releases = MinecraftVersionManifest.GetManifests().Versions
                .Where(x => x.Type.ToLower() == "release").Take(_minecraftCronConfiguration.VanillaSettings.GetLastReleaseUpdates);

            foreach (var metaData in snapshots.Select(version => version.GetMetadata()))
            {
                var gameUpdate = metaData.CreateGameUpdate();
                if (!gameUpdates.Any(x => x.Name == gameUpdate.Name && x.GroupName == gameUpdate.GroupName))
                {
                    gameUpdate.Save();
                    LogManager.Write($"[Minecraft Vanilla Update Cron] Saved Game Update for {metaData.Id}", LogType.Console);
                }
                else
                {
                    LogManager.Write("[Minecraft Vanilla Update Cron] Game Update already exists for " + metaData.Id, LogType.Console);
                }
            }

            foreach (var metaData in releases.Select(version => version.GetMetadata()))
            {
                var gameUpdate = metaData.CreateGameUpdate();
                if (!gameUpdates.Any(x => x.Name == gameUpdate.Name && x.GroupName == gameUpdate.GroupName))
                {
                    gameUpdate.Save();
                    LogManager.Write($"[Minecraft Vanilla Update Cron] Saved Game Update for {metaData.Id}", LogType.Console);
                }
                else
                {
                    LogManager.Write("[Minecraft Vanilla Update Cron] Game Update already exists for " + metaData.Id, LogType.Console);
                }
            }
        }
    }
}