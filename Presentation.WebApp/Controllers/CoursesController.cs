using Infrastructure.Dtos;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.ViewModels;

namespace Presentation.WebApp.Controllers;

public class CoursesController : Controller
{
    #region Index
    [HttpGet]
	[Route("/courses")]
	public IActionResult Index()
	{
		var viewModel = new CoursesViewModel
		{
			Categories =
		    [
			    new Category { Id = 1, Name = "Category 1" },
			    new Category { Id = 2, Name = "Category 2" },
			    new Category { Id = 3, Name = "Category 3" },
		    ],


			Course =
			[
				new CourseModel 
                { Id = 1, 
                  Name = "Fullstack Web Developer Course from Scratch",
                  Description = "Suspendisse natoque sagittis, consequat turpis. Sed tristique tellus morbi magna. At vel senectus accumsan, arcu mattis id tempor. Tellus sagittis, euismod porttitor sed tortor est id. Feugiat velit velit, tortor ut. Ut libero cursus nibh lorem urna amet tristique leo. Viverra lorem arcu nam nunc at ipsum quam. A proin id sagittis dignissim mauris condimentum ornare. Tempus mauris sed dictum ultrices.",
                  Author = "Albert Flores",
				  Price = 10.2,
                  Duration = "220 hours"

				}
			]
		};

		return View(viewModel);
	}

    [HttpPost]
    [Route("/courses")]
    public IActionResult Index(CoursesViewModel viewModel)
    {
        

        return View(viewModel);
    }
    #endregion

    #region Single course
    [HttpGet]
    public IActionResult SingleCourse(int id)
    {
		//var course = _courseService.GetCourseById(id);

		//if (course == null)
		//{
		//	return NotFound(); 
		//}
		var viewModel = new CourseModel
		{
			Id = 1,
			Name = "Fullstack Web Developer Course from Scratch",
			Ingress = "Egestas feugiat lorem eu neque suspendisse ullamcorper scelerisque aliquam mauris.",
			Description = "Suspendisse natoque sagittis, consequat turpis. Sed tristique tellus morbi magna. At vel senectus accumsan, arcu mattis id tempor. Tellus sagittis, euismod porttitor sed tortor est id. Feugiat velit velit, tortor ut. Ut libero cursus nibh lorem urna amet tristique leo. Viverra lorem arcu nam nunc at ipsum quam. A proin id sagittis dignissim mauris condimentum ornare. Tempus mauris sed dictum ultrices.",
			Author = "Albert Flores",
			Price = 10.2,
			Duration = "220 hours"
		};

		return View(viewModel);
    }

    #endregion
}
