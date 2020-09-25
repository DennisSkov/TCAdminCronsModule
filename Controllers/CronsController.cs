using System.Linq;
using System.Web.Mvc;
using Alexr03.Common.Web.Helpers;
using Newtonsoft.Json.Linq;
using TCAdmin.GameHosting.SDK.Objects;
using TCAdmin.SDK.VirtualFileSystem;
using TCAdmin.SDK.Web.FileManager;
using TCAdmin.SDK.Web.MVC.Controllers;
using TCAdminCrons.Models.Objects;

namespace TCAdminCrons.Controllers
{
    public class CronsController : BaseController
    {
        public ActionResult Configuration()
        {
            return View();
        }
        
        [ParentAction("Configuration")]
        public ActionResult ConfigureCron(int id)
        {
            var cronJob = new CronJob(id);
            TempData["repeatEvery"] = cronJob.ExecuteEverySeconds;
            var configurationJObject = (JObject)cronJob.GetConfiguration<object>();
            var o = configurationJObject.ToObject(cronJob.Configuration.Type);
            ViewData.TemplateInfo = new TemplateInfo
            {
                HtmlFieldPrefix = cronJob.Configuration.Type.Name,
            };
            return PartialView($"{cronJob.Configuration.View}", o);
        }

        [HttpPost]
        [ParentAction("Configuration")]
        public ActionResult ConfigureCron(int id, FormCollection model)
        {
            var cronJob = new CronJob(id);
            cronJob.ExecuteEverySeconds = int.Parse(Request[$"{cronJob.Configuration.Type.Name}.repeatEvery"]);
            cronJob.Save();
            TempData["repeatEvery"] = cronJob.ExecuteEverySeconds;
            var bindModel = model.Parse(ControllerContext, cronJob.Configuration.Type);
            cronJob.SetConfiguration(bindModel);
            return PartialView($"{cronJob.Configuration.View}", bindModel);
        }

        [HttpPost]
        [ParentAction("Configuration")]
        public ActionResult RunCronNow(int id)
        {
            var cronJob = new CronJob(id);

            var master = TCAdmin.GameHosting.SDK.Objects.Server.GetEnabledServers().Cast<Server>()
                .FirstOrDefault(x => x.IsMaster);
            if (master == null)
            {
                return Json(new { });
            }

            var virtualDirectorySecurity = new VirtualDirectorySecurity();
            System.Threading.Tasks.Task.Run(() => cronJob.ExecuteCron());
            var consoleLog = cronJob.GetLogFile();
            var rt = new RemoteTail(master, virtualDirectorySecurity, consoleLog, "Console Log", string.Empty,
                string.Empty);
            return Json(new
            {
                url = rt.GetUrl()
            });
        }
    }
}