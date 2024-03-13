using Infrastructure.Dtos;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.ViewModels;

namespace Presentation.WebApp.Controllers;

public class ContactController : Controller
{
    [HttpGet]
    [Route("/contact")]
    public IActionResult Index()
    {
        var viewModel = new ContactViewModel
		{
			Services =
		[
			new Service { Id = 1, Name = "Service 1" },
			new Service { Id = 2, Name = "Service 2" },
			new Service { Id = 3, Name = "Service 3" },
		]
		};

		return View(viewModel);
    }
}
