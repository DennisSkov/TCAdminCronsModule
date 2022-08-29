using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using Alexr03.Common.Misc.Strings;
using Newtonsoft.Json;
using TCAdmin.GameHosting.SDK.Objects;
using TCAdminCrons.Configuration;
using TCAdminCrons.Crons.GameUpdates;
using TCAdminCrons.Models.Objects;

namespace TCAdminCrons.Models.Minecraft.Purpur
{
    public class PurpurResponse
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("file")]
        public string File { get; set; }

        [JsonProperty("md5")]
        public string Md5 { get; set; }

        [JsonProperty("built")]
        public long Built { get; set; }
        
        public GameUpdate GetGameUpdate()
        {
            var config = new CronJob().FindByType(typeof(MinecraftPurpurUpdatesCron)).Configuration.Parse<PurpurSettings>();
            
            var newId = Regex.Replace(this.Version, "[^0-9]", "");
            int.TryParse(newId, out var parsedId);
            
            var variables = new Dictionary<string, object>
            {
                {"Update", this}
            };
            
            var gameUpdate = new GameUpdate
            {
                Name = config.NameTemplate.ReplaceWithVariables(variables),
                GroupName = config.Group,
                WindowsFileName = $"{GetDownloadUrl(Version)} {config.FileName.ReplaceWithVariables(variables)}",
                LinuxFileName = $"{GetDownloadUrl(Version)} {config.FileName.ReplaceWithVariables(variables)}",
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
            return $"https://api.purpurmc.org/v2/purpur/{version}/latest/download";
        }
    }
    public class PurpurVersionManifest
    {
        [JsonProperty("versions")] public IList<PurpurResponse> Version { get; set; }

        public static PurpurVersionManifest GetManifests()
        {
            using (var wc = new WebClient())
            {
                return JsonConvert.DeserializeObject<PurpurVersionManifest>(
                    wc.DownloadString("https://api.purpurmc.org/v2/purpur"));
            }
        }
    }
}
