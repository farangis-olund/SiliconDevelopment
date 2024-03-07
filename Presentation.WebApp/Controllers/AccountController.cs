using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.ViewModels;

namespace Presentation.WebApp.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly AddressService _addressService;

    public AccountController(UserManager<UserEntity> userManager, AddressService addressService)
    {
        _userManager = userManager;
        _addressService = addressService;
    }
    #region HttpGet Index
    [HttpGet]
    [Route("/account")]
    public async Task<IActionResult> Index()
    {
		var viewModel = new AccountDetailViewModel();
		var accountDetailsTask = PopulateAccountDetailsAsync();
		viewModel.ProfileInfo = (await accountDetailsTask).ProfileInfo;
		viewModel.BasicInfo ??= (await accountDetailsTask).BasicInfo;
		viewModel.AddressInfo ??= (await accountDetailsTask).AddressInfo;

		return View(viewModel);
    }
	#endregion

	#region HttpPost Index
	[HttpPost]
	[Route("/account")]
	public async Task<IActionResult> Index(AccountDetailViewModel viewModel)
	{
		var user = await _userManager.GetUserAsync(User);
		if (user != null) {
			if (viewModel.BasicInfo != null) 
			{
				
				if (viewModel.BasicInfo.FirstName !=null && viewModel.BasicInfo.LastName != null && viewModel.BasicInfo.Email != null)
				{
					user.FirstName = viewModel.BasicInfo.FirstName;
					user.LastName = viewModel.BasicInfo.LastName;
					user.Email = viewModel.BasicInfo.Email;
					user.UserName = viewModel.BasicInfo.Email;
					user.PhoneNumber = viewModel.BasicInfo.Phone;
					user.Bio = viewModel.BasicInfo.Biography;
					
					var result = await _userManager.UpdateAsync(user);
					
					if (!result.Succeeded) 
					{
						ViewData["ErrorMessage"] = "Update is failed!";
					}
				}
			}
			if (viewModel.AddressInfo != null)
			{
				if (viewModel.AddressInfo.Addressline_1 != null && viewModel.AddressInfo.City != null && viewModel.AddressInfo.PostalCode != null)
				{
					if (user.AddressId !=null)
					{
						var response = await _addressService.GetAddressAsync((int)user.AddressId);
						if (response.ContentResult != null)
						{
							AddressEntity addressEntity = (AddressEntity)response.ContentResult;
							addressEntity.Addressline_1 = viewModel.AddressInfo.Addressline_1;
							addressEntity.Addressline_2 = viewModel.AddressInfo.Addressline_2;
							addressEntity.City = viewModel.AddressInfo.City;
							addressEntity.PostalCode = viewModel.AddressInfo.PostalCode;
						}
						
					}
					else
					{
						var newAddress = new AddressEntity
						{
							Addressline_1 = viewModel.AddressInfo.Addressline_1,
							Addressline_2 = viewModel.AddressInfo.Addressline_2,
							City = viewModel.AddressInfo.City,
							PostalCode = viewModel.AddressInfo.PostalCode
						};
						var response = await _addressService.AddAddressAsync(newAddress);
						if (response.ContentResult != null)
						{
							AddressEntity address = (AddressEntity)response.ContentResult;
							user.AddressId = address.Id;
						}
					}
					
					var result = await _userManager.UpdateAsync(user);

					if (!result.Succeeded)
					{
						ViewData["ErrorMessage"] = "Update is failed!";
					}
				}
			}	
		}
		
		var accountDetailsTask = PopulateAccountDetailsAsync();
		viewModel.ProfileInfo = (await accountDetailsTask).ProfileInfo;
		viewModel.BasicInfo ??= (await accountDetailsTask).BasicInfo;
		viewModel.AddressInfo ??= (await accountDetailsTask).AddressInfo;

		ViewData["Title"] = viewModel.Title;
        return View(viewModel);
	}
	#endregion

	private async Task<AccountDetailViewModel> PopulateAccountDetailsAsync()
	{
		var user = await _userManager.GetUserAsync(User);
		if (user == null)
			return null!;

		var accountDetails = new AccountDetailViewModel
		{
			ProfileInfo = new AccountProfileModel
			{
				ProfileImgUrl = user.ProfileImgUrl,
				FirstName = user.FirstName,
				LastName = user.LastName,
				UserName = user.Email!,
				Email = user.Email!,
				
				IsExternalAccount = user.IsExternalAccount
			},
			BasicInfo = new AccountDetailBasicInfoModel
			{
				UserId = user.Id,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email!,
				Phone = user.PhoneNumber!,
				Biography = user.Bio
			}
		};

		if (user.AddressId.HasValue)
		{
			var result = await _addressService.GetAddressAsync(user.AddressId.Value);
			if (result.ContentResult is AddressEntity address)
			{
				accountDetails.AddressInfo = new AccountDetailAddressInfoModel
				{
					Addressline_1 = address.Addressline_1,
					Addressline_2 = address.Addressline_2,
					City = address.City,
					PostalCode = address.PostalCode
				};
			}
		}

		return accountDetails;
	}

}
