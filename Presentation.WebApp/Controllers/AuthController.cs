using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.WebApp.ViewModels;
using System.Security.Claims;


namespace Presentation.WebApp.Controllers;

public class AuthController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager) : Controller
{

	private readonly UserManager<UserEntity> _userManager = userManager;
	private readonly SignInManager<UserEntity> _signInManager = signInManager;

	//Individual account
	#region Individual Account |SignIn
	[Route("/signin")]
	[HttpGet]
	public IActionResult SignIn(string returnUrl)
    {
		if (_signInManager.IsSignedIn(User))
			return RedirectToAction("Index", "Account");

		ViewData["Title"] = "Sign in";
		ViewData["ErrorMessage"] = "";
		ViewData["ReturnUrl"] = returnUrl ?? Url.Content("~/");
		var viewModel = new SignInViewModel();
		return View(viewModel);
	}
	
	[Route("/signin")]
	[HttpPost]
	public async Task<IActionResult> SignIn(SignInViewModel model, string returnUrl)
    {
		if (ModelState.IsValid)
		{
			var result = await _signInManager.PasswordSignInAsync(model.Form.Email, model.Form.Password, model.Form.RememberMe, false); 
			
			if (result.Succeeded)
			{				
				if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
					return Redirect(returnUrl);
				return RedirectToAction("Index", "Account");
			};

			
				
		}
		ModelState.AddModelError("Inccorect", "Incorrect email or password");
		ViewData["ErrorMessage"] = "Incorrect email or password";
		return View(model);

	}
	#endregion

	#region Individual Account |SignUp
	[Route("/signup")]
    [HttpGet]
    public IActionResult SignUp()
    {
		if (_signInManager.IsSignedIn(User))
			return RedirectToAction("Index", "Account");

		ViewData["Title"] = "Sign Up";
		ViewData["ErrorMessage"] = "";
		var viewModel = new SignUpViewModel();
        return View(viewModel);
    }
	
	[Route("/signup")]
    [HttpPost]
	public async Task<IActionResult> SignUp(SignUpViewModel model)
	{
		if (ModelState.IsValid)
		{
			var exists = await _userManager.Users.AnyAsync(x => x.Email == model.Form.Email);
			if (exists)
			{
				ModelState.AddModelError("AlreadyExists", "User with the same email already exists");
				ViewData["ErrorMessage"] = "User with the same email already exists";
				return View(model);
			}

			var userEntity = new UserEntity
			{
				FirstName = model.Form.FirstName,
				LastName = model.Form.LastName,
				Email = model.Form.Email,
				UserName = model.Form.Email
			};
			
			
			var result = await _userManager.CreateAsync(userEntity, model.Form.Password);
			if ( result.Succeeded)
			return RedirectToAction("SignIn", "Auth");
		}
		ViewData["ErrorMessage"] = "The required fields must be filled.";
		return View(model);
	}
	#endregion

	#region Individual Account |SignOut
	[HttpGet]
	[Route("/signout")]
	public new async Task<IActionResult> SignOut()
    {
		await _signInManager.SignOutAsync();
		return RedirectToAction("Index", "Home");
    }
	#endregion

	#region External Account |Facebook

	[HttpGet]
	public IActionResult Facebook()
	{
		var authProps = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", Url.Action("FacebookCallback"));
		return new ChallengeResult("Facebook", authProps);
	}

	[HttpGet]
	public async Task<IActionResult> FacebookCallback()
	{
		var info = await _signInManager.GetExternalLoginInfoAsync();
		
		if (info != null)
		{
			var userEntity = new UserEntity
			{
				FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)!,
				LastName = info.Principal.FindFirstValue(ClaimTypes.Surname)!,
				Email = info.Principal.FindFirstValue(ClaimTypes.Email)!,
				UserName = info.Principal.FindFirstValue(ClaimTypes.Email)!,
			};

			var user = await _userManager.FindByEmailAsync(userEntity.Email);
			if (user == null)
			{
				var result = await _userManager.CreateAsync(userEntity);
				if (result.Succeeded)
				{
					user = await _userManager.FindByEmailAsync(userEntity.Email);

				}
			}
			if (user != null) 
			{
				if (user.FirstName != userEntity.FirstName || user.LastName != userEntity.LastName || user.Email != userEntity.Email)
				{
					user.FirstName = userEntity.FirstName; 
					user.LastName = userEntity.LastName;
					user.Email = userEntity.Email;

					await _userManager.UpdateAsync(user);
				}

				await _signInManager.SignInAsync(user, isPersistent: false);

				if (HttpContext.User != null)
					return RedirectToAction("Index", "Account");	
			}
		}
		ModelState.AddModelError("InvalidFacebookAuth" , "danger|Authantication via facebook failed");
		ViewData["StatusMessage"] = "danger|Authantication via facebook failed";
		return RedirectToAction("Index", "Account");
	}


	#endregion

}
