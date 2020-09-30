using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Alexr03.Common.Misc.Strings;
using Newtonsoft.Json;
using TCAdmin.GameHosting.SDK.Objects;
using TCAdminCrons.Configuration;
using TCAdminCrons.Models.Objects;

namespace TCAdminCrons.Models.Minecraft.Vanilla
{
    public class Server
    {

        [JsonProperty("sha1")]
        public string Sha1 { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class Downloads
    {
        [JsonProperty("server")]
        public Server Server { get; set; }
    }

    public class MinecraftVersionMetadata
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("downloads")]
        public Downloads Downloads { get; set; }

        [JsonProperty("releaseTime")]
        public DateTime ReleaseTime { get; set; }

        [JsonProperty("time")]
        public DateTime Time { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        public GameUpdate CreateGameUpdate()
        {
            var config = new CronJob(1).Configuration.GetConfiguration<VanillaSettings>();
            
            var newId = Regex.Replace(this.Id, "[^0-9]", "");
            int.TryParse(newId, out var parsedId);

            var variables = new Dictionary<string, object>
            {
                {"Update", this},
                {"Update.Version", this.Id}
            };

            var gameUpdate = new GameUpdate
            {
                Name = config.NameTemplate.ReplaceWithVariables(variables),
                GroupName = config.Group,
                WindowsFileName = $"{this.Downloads.Server.Url} {config.FileName.ReplaceWithVariables(variables)}",
                LinuxFileName = $"{this.Downloads.Server.Url} {config.FileName.ReplaceWithVariables(variables)}",
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
        
        public GameUpdate CreateGameUpdateSnapshot()
        {
            var config = new CronJob(5).Configuration.GetConfiguration<VanillaSnapshotSettings>();
            
            var newId = Regex.Replace(this.Id, "[^0-9]", "");
            int.TryParse(newId, out var parsedId);

            var variables = new Dictionary<string, object>
            {
                {"Update", this},
                {"Update.Version", this.Id}
            };

            var gameUpdate = new GameUpdate
            {
                Name = config.NameTemplate.ReplaceWithVariables(variables),
                GroupName = config.Group,
                WindowsFileName = $"{this.Downloads.Server.Url} {config.FileName.ReplaceWithVariables(variables)}",
                LinuxFileName = $"{this.Downloads.Server.Url} {config.FileName.ReplaceWithVariables(variables)}",
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
    }
}