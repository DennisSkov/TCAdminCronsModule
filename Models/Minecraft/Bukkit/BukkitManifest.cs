using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using TCAdmin.GameHosting.SDK.Objects;
using TCAdminCrons.Configuration;
using TCAdminCrons.Models.Objects;

namespace TCAdminCrons.Models.Minecraft.Bukkit
{
    public class BukkitResponse
    {
        [JsonProperty("version")] 
        public string Version { get; set; }

        [JsonProperty("file")] 
        public string File { get; set; }

        [JsonProperty("md5")] 
        public string Md5 { get; set; }

        [JsonProperty("built")] 
        public int Built { get; set; }
        
        public GameUpdate GetGameUpdate()
        {
            var config = new CronJob(2).Configuration.GetConfiguration<BukkitSettings>();
            
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
            return $"https://serverjars.com/api/fetchJar/bukkit/{version}";
        }
    }
    public class BukkitVersionManifest
    {
        [JsonProperty("response")] public IList<BukkitResponse> Version { get; set; }

        public static BukkitVersionManifest GetManifests()
        {
            using (var wc = new WebClient())
            {
                return JsonConvert.DeserializeObject<BukkitVersionManifest>(
                    wc.DownloadString("https://serverjars.com/api/fetchAll/bukkit"));
            }
        }
    }
    
}