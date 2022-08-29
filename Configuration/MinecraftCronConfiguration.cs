using System.ComponentModel.DataAnnotations;

namespace TCAdminCrons.Configuration
{
    public class VanillaSettings : GameUpdateSettings
    {
        public override bool Enabled { get; set; }
        public override string NameTemplate { get; set; } = "{Update.Version}";
        public override string Group { get; set; } = "Vanilla Release";
        public override string Description { get; set; } =
            "This is a vanilla server snapshot of Minecraft: Java Edition | Release Date: {Update.ReleaseTime} | Added by TCAdminCrons";

        public override bool UseVersionAsViewOrder { get; set; } = true;
        public override int GetLastReleaseUpdates { get; set; } = 15;
        public override string FileName { get; set; } = "minecraft_server.jar";
    }
    
    public class VanillaSnapshotSettings : GameUpdateSettings
    {
        public override bool Enabled { get; set; }
        public override string NameTemplate { get; set; } = "{Update.Version}";
        public override string Group { get; set; } = "Vanilla Snapshot Release";

        public override string Description { get; set; } =
            "This is a vanilla snapshot server snapshot of Minecraft: Java Edition | Release Date: {Update.ReleaseTime} | Added by TCAdminCrons";

        public override bool UseVersionAsViewOrder { get; set; } = true;
        public override int GetLastReleaseUpdates { get; set; } = 15;
        
        public override string FileName { get; set; } = "minecraft_server.jar";
    }

    public class PaperSettings : GameUpdateSettings
    {
        public override bool Enabled { get; set; }
        public override string Group { get; set; } = "Paper";
        public override string NameTemplate { get; set; } = "{Update.Version} (Build: {Update.Version.Build})";

        public override string Description { get; set; } =
            "Paper is the next generation of Minecraft server, compatible with Spigot plugins and offering uncompromising performance. | Added by TCAdminCrons";

        public override bool UseVersionAsViewOrder { get; set; } = true;
        public override int GetLastReleaseUpdates { get; set; } = 15;
        
        public override string FileName { get; set; } = "minecraft_server.jar";
    }
    
    public class PurpurSettings : GameUpdateSettings
    {
        public override bool Enabled { get; set; }
        public override string Group { get; set; } = "Purpur";
        public override string NameTemplate { get; set; } = "{Update.Version}";

        public override string Description { get; set; } =
            "Purpur is a drop-in replacement for Paper servers designed for configurability, new fun & exciting gameplay features, and high performanceng performance. | Added by TCAdminCrons";

        public override bool UseVersionAsViewOrder { get; set; } = true;
        public override int GetLastReleaseUpdates { get; set; } = 15;
        
        public override string FileName { get; set; } = "minecraft_server.jar";
    }

    public class SpigotSettings : GameUpdateSettings
    {
        public override bool Enabled { get; set; }
        public override string Group { get; set; } = "Spigot";
        public override string NameTemplate { get; set; } = "{Update.Version}";

        public override string Description { get; set; } =
            "Spigot is a modified version of CraftBukkit with hundreds of improvements and optimizations that can only make CraftBukkit shrink in shame! | Added by TCAdminCrons";

        public override bool UseVersionAsViewOrder { get; set; } = true;
        public override int GetLastReleaseUpdates { get; set; } = 15;
        
        public override string FileName { get; set; } = "minecraft_server.jar";
    }

    public class BukkitSettings : GameUpdateSettings
    {
        public override bool Enabled { get; set; }
        public override string Group { get; set; } = "Bukkit";
        public override string NameTemplate { get; set; } = "{Update.Version}";

        public override string Description { get; set; } =
            "CraftBukkit is lightly modified version of the Vanilla software allowing it to be able to run Bukkit plugins | Added by TCAdminCrons";

        public override bool UseVersionAsViewOrder { get; set; } = true;
        public override int GetLastReleaseUpdates { get; set; } = 15;

        public override string FileName { get; set; } = "minecraft_server.jar";
    }

    public abstract class GameUpdateSettings
    {
        [Display(Description = "Game ID to add game updates to.")]
        public virtual int GameId { get; set; } = 0;
        
        [Display(Description = "Enable this game update option.")]
        public virtual bool Enabled { get; set; } = false;

        [Display(Description = "Group to add the game update to.")]
        public virtual string Group { get; set; } = "GameUpdate";

        [Display(Name = "Name", Description = "The name the game update will be set to.")]
        public virtual string NameTemplate { get; set; } = "{Update.Version}";

        [Display(Description = "Description for the game update.")]
        public virtual string Description { get; set; } = "Added by TCAdminCrons";

        [Display(Name = "Use version as view order value",
            Description = "Use the version number as the view order value for the game update.")]
        public virtual bool UseVersionAsViewOrder { get; set; } = true;

        [Display(Name = "Get the last x updates.", Description = "Get the last x updates.")]
        public virtual int GetLastReleaseUpdates { get; set; } = 15;
        
        [Display(Name = "Image URL", Description = "Image that is shown in the grid.")]
        public virtual string ImageUrl { get; set; } = "";
        
        [Display(Name = "File Name", Description = "This is the file name that is saved.")]
        public virtual string FileName { get; set; } = "";
        
        [Display(Name = "Extract Path", Description = "This is the path relative to the game server where this image is installed.")]
        public virtual string ExtractPath { get; set; } = "/";
    }
}
