using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApp.Controllers;

public class ContactController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Contact us";
        return View();
    }
}
