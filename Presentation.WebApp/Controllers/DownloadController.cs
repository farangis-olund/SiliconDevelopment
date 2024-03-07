using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApp.Controllers
{
    public class DownloadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
