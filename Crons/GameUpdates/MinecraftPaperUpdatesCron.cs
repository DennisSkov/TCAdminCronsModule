using System;
using System.Linq;
using System.Threading.Tasks;
using TCAdmin.GameHosting.SDK.Objects;
using TCAdmin.Interfaces.Logging;
using TCAdmin.SDK;
using TCAdminCrons.Configuration;
using TCAdminCrons.Models.Minecraft.Paper;

namespace TCAdminCrons.Crons.GameUpdates
{
    public class MinecraftPaperUpdatesCron : TcAdminCronJob
    {
        private readonly MinecraftCronConfiguration _minecraftCronConfiguration =
            MinecraftCronConfiguration.GetConfiguration();

        public override async Task DoAction()
        {
            if (!_minecraftCronConfiguration.PaperSettings.Enabled)
            {
                LogManager.Write("[Minecraft Paper Update Cron] - Disabled in Configuration.", LogType.Console);
                return;
            }
            try
            {
                LogManager.Write("[Minecraft Paper Update Cron] Running...", LogType.Console);
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
            var paperUpdates = PaperManifest.GetManifest();

            foreach (var version in paperUpdates.Versions.Take(_minecraftCronConfiguration.PaperSettings.GetLastReleaseUpdates))
            {
                var gameUpdate = PaperManifest.GetGameUpdate(version);
                if (!gameUpdates.Any(x => x.Name == gameUpdate.Name && x.GroupName == gameUpdate.GroupName))
                {
                    gameUpdate.Save();
                    LogManager.Write($"[Minecraft Paper Update Cron] Saved Game Update for {version}", LogType.Console);
                }
                else
                {
                    LogManager.Write("[Minecraft Paper Update Cron] Game Update already exists for " + version, LogType.Console);
                }
            }
        }
    }
}