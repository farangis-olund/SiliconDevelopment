
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class SignInModel
{
	[Display(Name ="Email address", Prompt ="Enter your email address")]
    [DataType(DataType.EmailAddress)]
	[Required(ErrorMessage = "Email address is required")]
	public string Email { get; set; } = null!;
    
    [Display(Name = "Password", Prompt = "Enter your password")]
    [DataType(DataType.Password)]
	[Required(ErrorMessage = "Password is required")]
	public string Password { get; set; } = null!;

	[Display(Name = "Remember me")]
	
	public bool RememberMe { get; set; } = false;
}
