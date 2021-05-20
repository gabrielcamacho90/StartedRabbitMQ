using Microsoft.AspNetCore.Mvc;
using Started.Project.RabbitMq.Shared;

namespace Started.Project.RabbitMq.Api.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("Configuration")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Config()
        {
            return Json(Settings.ConnectionString);
        }
    }
}
