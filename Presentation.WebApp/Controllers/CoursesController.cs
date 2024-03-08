using Infrastructure.Dtos;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.ViewModels;

namespace Presentation.WebApp.Controllers;

public class CoursesController : Controller
{
	[HttpGet]
	[Route("/courses")]
	public IActionResult Index()
	{
        var viewModel = new CoursesViewModel();

        viewModel.Categories =
        [
            new Category { Id = 1, Name = "Category 1" },
            new Category { Id = 2, Name = "Category 2" },
            new Category { Id = 3, Name = "Category 3" },
        ];
        
        return View(viewModel);
	}
}
