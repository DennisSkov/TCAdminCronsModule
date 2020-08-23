using System.ComponentModel.DataAnnotations;

namespace TCAdminCrons.Configuration
{
    public class MinecraftCronConfiguration
    {
        [Display(Name = "Game ID of the Minecraft Game Configuration.")]
        public int GameId { get; set; }

        [Display(Name = "Run every x Seconds.")]
        public int Seconds { get; set; } = 3600;

        public VanillaSettings VanillaSettings { get; set; } = new VanillaSettings();
        public PaperSettings PaperSettings { get; set; } = new PaperSettings();
        public SpigotSettings SpigotSettings { get; set; } = new SpigotSettings();
        public BukkitSettings BukkitSettings { get; set; } = new BukkitSettings();

        public static MinecraftCronConfiguration GetConfiguration()
        {
            return ConfigurationHelper.GetConfiguration<MinecraftCronConfiguration>("Crons.MinecraftUpdates");
        }

        public static void SetConfiguration(MinecraftCronConfiguration model)
        {
            ConfigurationHelper.SetConfiguration("Crons.MinecraftUpdates", model);
        }
    }

    public class VanillaSettings : GameUpdateSettings
    {
        public override bool Enabled { get; set; }
        public override string NameTemplate { get; set; } = "{Update.Id}";
        public override string Group { get; set; } = "Vanilla Release";
        [Display(Name = "Snapshot Group")] public string SnapshotGroup { get; set; } = "Vanilla Snapshot";

        public override string Description { get; set; } =
            "This is a vanilla server snapshot of Minecraft: Java Edition | Release Date: {Update.ReleaseTime} | Added by TCAdminCrons";

        public override bool UseVersionAsViewOrder { get; set; } = true;
        public override int GetLastReleaseUpdates { get; set; } = 15;

        [Display(Name = "Get the last x snapshots")]
        public int GetLastSnapshotUpdates { get; set; } = 15;
    }

    public class PaperSettings : GameUpdateSettings
    {
        public override bool Enabled { get; set; }
        public override string Group { get; set; } = "Paper";
        public override string NameTemplate { get; set; } = "{Version}";

        public override string Description { get; set; } =
            "Paper is the next generation of Minecraft server, compatible with Spigot plugins and offering uncompromising performance. | Added by TCAdminCrons";

        public override bool UseVersionAsViewOrder { get; set; } = true;
        public override int GetLastReleaseUpdates { get; set; } = 15;
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
    }

    public abstract class GameUpdateSettings
    {
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
    }
}