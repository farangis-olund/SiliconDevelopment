using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.Services;
using Presentation.WebApp.ViewModels;
using Infrastructure.Models;

namespace Presentation.WebApp.Controllers;

[Authorize]
public class CoursesController(ApiCourseService apiCourseService, UserManager<UserEntity> userManager ,UserCourseService userCourseService) : Controller
{
    private readonly ApiCourseService _apiCourseService = apiCourseService;
    public readonly UserManager<UserEntity> _userManager = userManager; 
    private readonly UserCourseService _userCourseService = userCourseService;
	#region Index
	[HttpGet]
	[Route("/courses")]
	public async Task<IActionResult> Index(string? statusMessage)
	{
       
		if (!string.IsNullOrEmpty(statusMessage))
        {
            ViewData["StatusMessage"] = statusMessage;
        }

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
    [HttpGet("/course")]
    public async Task<IActionResult> SingleCourse(int id)
    {
		var	viewModel = await _apiCourseService.PopulateOneCourseAsync(id);
			
		return View(viewModel);
    }

    #endregion

    #region Join course
    [HttpPost("/join")]
    public async Task<IActionResult> Join(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        var response = await _userCourseService.AddUserCourseAsync(user!.Id, id);
        
        string statusMessage = response.StatusCode switch
        {
            Infrastructure.Models.StatusCode.Ok => "success|You have successfully joined the course!",
            Infrastructure.Models.StatusCode.Exists => "warning|You have already joined this course!",
            Infrastructure.Models.StatusCode.Error => "danger|Joining process failed. Something went wrong!",
            _ => "danger|Unknown error occurred!",
        };
        
        return RedirectToAction("Index" , new { statusMessage } );

    }

    #endregion
}
