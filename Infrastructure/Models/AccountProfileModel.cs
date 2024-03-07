
namespace Infrastructure.Models;

public class AccountProfileModel
{
	public string? ProfileImg { get; set; } = "userImg.svg";
	public string FirstName { get; set; } = null!;
	public string LastName { get; set; } = null!;
	public string Email { get; set; } = null!;
}
