using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.Services;
using Presentation.WebApp.ViewModels;

namespace Presentation.WebApp.Controllers;

[Authorize]
public class AccountController(UserManager<UserEntity> userManager, 
							   SignInManager<UserEntity> signInManager, 
							   AccountService accountService,
							   UserCourseService userCourseService) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;
	private readonly SignInManager<UserEntity> _signInManager = signInManager;
	private readonly AccountService _accountService = accountService;
	private readonly UserCourseService _userCourseService = userCourseService;

	#region Index - Details
	[HttpGet]
    [Route("/account")]
    public async Task<IActionResult> Index()
    {
		var user = await _userManager.GetUserAsync(User);
		if (user == null)
			return NotFound();

		var viewModel = await _accountService.GetAccountDetailsAsync(user);
		return View(viewModel);

	}
	
	[HttpPost]
	[Route("/account")]
	public async Task<IActionResult> Index(AccountDetailViewModel viewModel)
	{
		var user = await _userManager.GetUserAsync(User);
		if (user == null)
			return NotFound();
		
			var success = await _accountService.UpdateAccountAsync(user, viewModel);
			if (!success)
			{
				ViewData["StatusMessage"] = "danger|Failed to update account details!";
				return View(viewModel);
			}
		
		return RedirectToAction("Index");

	}
	#endregion

	#region Security
	[HttpGet]
	[Route("/account/security")]
	public async Task<IActionResult> AccountSecurity()
	{
		var user = await _userManager.GetUserAsync(User);
		var viewModel = await _accountService.GetAccountDetailsAsync(user!);
		return View(viewModel);
	}

	[HttpPost]
	[Route("/account/security")]
	public async Task<IActionResult> AccountSecurity(AccountDetailViewModel viewModel)
	{
		var user = await _userManager.GetUserAsync(User);
		if (user != null)
		{
			if (viewModel.SecurityInfo != null)
			{
				if (viewModel.SecurityInfo.CurrentPassword != null && viewModel.SecurityInfo.Password != null && (viewModel.SecurityInfo.ConfirmPassword != null && viewModel.SecurityInfo.Password  == viewModel.SecurityInfo.ConfirmPassword))
				{
					var result = await _userManager.ChangePasswordAsync(user, viewModel.SecurityInfo.CurrentPassword, viewModel.SecurityInfo.Password);

					if (result.Succeeded)
						ViewData["SatusMessage"] = " success|Password is saved successfully!";
					else
						ViewData["SatusMessage"] = " danger|Password saving process failed!";
				}
				else
				{
					ViewData["StatusMessage"] = "danger|Validation is failed, you should enter correct password!";
				}

				if (viewModel.SecurityInfo.DeleteAccount == true)
				{
					var result = await _userManager.DeleteAsync(user);
					if (result.Succeeded)
						ViewData["SatusMessage"] = "success|User is successfully deleted!";
					else
						ViewData["StatusMessage"] = "danger|Deleting process is failed!";
					await _signInManager.RefreshSignInAsync(user);
					return RedirectToAction("SignIn", "Auth");
					
				}

				
			}
			viewModel = await _accountService.GetAccountDetailsAsync(user);
		}

		ViewData["Title"] = viewModel.Title;
		return View(viewModel);
	}
	#endregion

	#region User Courses
	[HttpGet]
	[Route("/account/savedCourses")]
	public async Task<IActionResult> SavedCourses(string? statusMessage)
	{
		if (!string.IsNullOrEmpty(statusMessage))
		{
			ViewData["StatusMessage"] = statusMessage;
		}
		var user = await _userManager.GetUserAsync(User);
		
		var viewModel = await _accountService.GetAccountDetailsAsync(user!);
		
		return View(viewModel);
	}
	
	[HttpPost]
	[Route("/account/DeleteCourse")]
	public async Task<IActionResult> DeleteCourse(int id)
	{
		var user = await _userManager.GetUserAsync(User);
		
		var response = await _userCourseService.DeleteUserCourseAsync(user!.Id, id);

		string statusMessage = response.StatusCode switch
		{
			Infrastructure.Models.StatusCode.Ok => "success|You have successfully signed out the course!",
			Infrastructure.Models.StatusCode.Error => "danger|Course sign out process failed. Something went wrong!",
			_ => "danger|Unknown error occurred!",
		};

		return RedirectToAction("SavedCourses", new { statusMessage });
	}

	[HttpPost]
	[Route("/account/DeleteAllCourse")]
	public async Task<IActionResult> DeleteAllCourses()
	{
		var user = await _userManager.GetUserAsync(User);

		var response = await _userCourseService.DeleteAllUserCoursesAsync(user!.Id);

		string statusMessage = response.StatusCode switch
		{
			Infrastructure.Models.StatusCode.Ok => "success|You have successfully signed out from all courses!",
			Infrastructure.Models.StatusCode.Error => "danger|Course sign out process failed. Something went wrong!",
			_ => "danger|Unknown error occurred!",
		};

		return RedirectToAction("SavedCourses", new { statusMessage });
	}
	#endregion

}
