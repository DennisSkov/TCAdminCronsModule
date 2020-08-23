using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TCAdmin.GameHosting.SDK.Objects;
using TCAdmin.SDK.VirtualFileSystem;
using TCAdmin.SDK.Web.FileManager;
using TCAdmin.SDK.Web.MVC.Controllers;
using TCAdmin.Web.MVC;
using TCAdminCrons.Configuration;
using TCAdminCrons.Models;

namespace TCAdminCrons.Controllers
{
    public class CronsController : BaseController
    {
        public ActionResult Minecraft()
        {
            var model = MinecraftCronConfiguration.GetConfiguration();
            return View(model);
        }

        [HttpPost]
        public ActionResult Minecraft(MinecraftCronConfiguration model)
        {
            MinecraftCronConfiguration.SetConfiguration(model);
            return View(model);
        }

        [HttpPost]
        [ParentAction("Minecraft")]
        public ActionResult RunGameUpdatesNow()
        {
            var master = TCAdmin.GameHosting.SDK.Objects.Server.GetEnabledServers().Cast<Server>()
                .FirstOrDefault(x => x.IsMaster);
            if (master == null)
            {
                return Json(new { });
            }

            var fileSystem = master.FileSystemService;
            var virtualDirectorySecurity = new VirtualDirectorySecurity();
            var consoleLog = TCAdmin.SDK.Misc.FileSystem.CombinePath(
                master.ServerUtilitiesService.GetMonitorLogsDirectory(), "console.log", master.OperatingSystem);
            var rt = new RemoteTail(master, virtualDirectorySecurity, consoleLog, "Console Log", string.Empty,
                string.Empty);
            fileSystem.CreateTextFile(
                TCAdmin.SDK.Misc.FileSystem.CombinePath(master.ServerUtilitiesService.GetMonitorDirectory(),
                    "command.do", master.OperatingSystem), Encoding.Default.GetBytes("service tcacronsGU restart"));
            return Json(new
            {
                url = rt.GetUrl()
            });
        }
    }
}