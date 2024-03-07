using Infrastructure.Models;

namespace Presentation.WebApp.ViewModels;

public class AccountDetailViewModel
{
	public string Title { get; set; } = "Account Details";

	public AccountProfileModel ProfileInfo { get; set; } = null!;
	public AccountDetailBasicInfoModel BasicInfo { get; set; } = null!;
	public AccountDetailAddressInfoModel AddressInfo { get; set; } = null!;
}
