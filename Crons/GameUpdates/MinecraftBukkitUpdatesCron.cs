using System;
using System.Linq;
using System.Threading.Tasks;
using TCAdmin.GameHosting.SDK.Objects;
using TCAdmin.Interfaces.Logging;
using TCAdmin.SDK;
using TCAdminCrons.Configuration;
using TCAdminCrons.Models.Minecraft.Bukkit;

namespace TCAdminCrons.Crons.GameUpdates
{
    public class MinecraftBukkitUpdatesCron : TcAdminCronJob
    {
        private readonly MinecraftCronConfiguration _minecraftCronConfiguration =
            MinecraftCronConfiguration.GetConfiguration();

        public override async Task DoAction()
        {
            if (!_minecraftCronConfiguration.BukkitSettings.Enabled)
            {
                LogManager.Write("[Minecraft Bukkit Update Cron] - Disabled in Configuration.", LogType.Console);
                return;
            }
            try
            {
                LogManager.Write("[Minecraft Bukkit Update Cron] Running...", LogType.Console);
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
            var bukkitUpdates = BukkitVersionManifest.GetManifests().Version;

            foreach (var version in bukkitUpdates.Take(_minecraftCronConfiguration.BukkitSettings.GetLastReleaseUpdates))
            {
                var gameUpdate = version.GetGameUpdate();
                if (!gameUpdates.Any(x => x.Name == gameUpdate.Name && x.GroupName == gameUpdate.GroupName))
                {
                    gameUpdate.Save();
                    LogManager.Write($"[Minecraft Bukkit Update Cron] Saved Game Update for {version.Version}", LogType.Console);
                }
                else
                {
                    LogManager.Write("[Minecraft Bukkit Update Cron] Game Update already exists for " + version.Version, LogType.Console);
                }
            }
        }
    }
}