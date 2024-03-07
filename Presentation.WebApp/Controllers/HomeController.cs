using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApp.Controllers;

public class HomeController : Controller
{
    [Route("/")]
    public IActionResult Index()
    {
		ViewData["Title"] = "Task Management Asisstent";
		return View();
    }

	[Route("/error")]
    public IActionResult Error404(int statusCode) => View();
}
