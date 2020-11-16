using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using Alexr03.Common.Misc.Strings;
using Newtonsoft.Json;
using TCAdmin.GameHosting.SDK.Objects;
using TCAdminCrons.Configuration;
using TCAdminCrons.Crons.GameUpdates;
using TCAdminCrons.Models.Objects;

namespace TCAdminCrons.Models.Minecraft.Paper
{
    public class PaperManifest
    {
        [JsonProperty("project")]
        public string Project { get; set; }

        [JsonProperty("versions")]
        public IList<string> Versions { get; set; }

        public static PaperManifest GetManifest()
        {
            using (var wc = new WebClient())
            {
                return JsonConvert.DeserializeObject<PaperManifest>(
                    wc.DownloadString("https://papermc.io/api/v1/paper/"));
            }
        }

        public static GameUpdate GetGameUpdate(string version)
        {
            var config = new CronJob().FindByType(typeof(MinecraftPaperUpdatesCron)).Configuration.Parse<PaperSettings>();
            
            var newId = Regex.Replace(version, "[^0-9]", "");
            int.TryParse(newId, out var parsedId);
            
            var variables = new Dictionary<string, object>
            {
                {"Update.Version", version}
            };
            
            var gameUpdate = new GameUpdate
            {
                Name = config.NameTemplate.ReplaceWithVariables(variables),
                GroupName = config.Group,
                WindowsFileName = $"{GetDownloadUrl(version)} {config.FileName.ReplaceWithVariables(variables)}",
                LinuxFileName = $"{GetDownloadUrl(version)} {config.FileName.ReplaceWithVariables(variables)}",
                ImageUrl = config.ImageUrl,
                ExtractPath = config.ExtractPath,
                Reinstallable = true,
                DefaultInstall = false,
                GameId = config.GameId,
                Comments = config.Description.ReplaceWithVariables(variables),
                UserAccess = true,
                SubAdminAccess = true,
                ResellerAccess = true,
                ViewOrder = config.UseVersionAsViewOrder ? parsedId : 0
            };

            gameUpdate.GenerateKey();
            return gameUpdate;
        }

        private static string GetDownloadUrl(string version)
        {
            return $"https://papermc.io/api/v1/paper/{version}/latest/download";
        }
    }
}