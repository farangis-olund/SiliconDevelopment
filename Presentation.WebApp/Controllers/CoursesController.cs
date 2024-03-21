using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.Services;
using Presentation.WebApp.ViewModels;

namespace Presentation.WebApp.Controllers;

[Authorize]
public class CoursesController(ApiCourseService apiCourseService) : Controller
{
    private readonly ApiCourseService _apiCourseService = apiCourseService;
	#region Index
	[HttpGet]
	[Route("/courses")]
	public async Task<IActionResult> Index()
	{
		var viewModel = await _apiCourseService.PopulateAllCoursesAsync();

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
    public async Task<IActionResult> SingleCourse(int id)
    {
			var viewModel= await _apiCourseService.PopulateOneCourseAsync(id);
	
		return View(viewModel);
    }

	#endregion

}
