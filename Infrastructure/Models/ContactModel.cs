using Infrastructure.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class ContactModel
{
	[Display(Name = "Full name", Prompt = "Enter your full name", Order = 0)]
	[Required(ErrorMessage = "First name is required")]
	[DataType(DataType.Text)]
	public string FullName { get; set; } = null!;


	[Display(Name = "Email address", Prompt = "Enter your email address", Order = 1)]
	[Required(ErrorMessage = "Email address is required")]
	[EmailAddress]
	[RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
	public string Email { get; set; } = null!;

	[Display(Name = "Service", Prompt = "Choose the service you are interested in", Order = 2)]
	public int? SelectedServiceId { get; set; }

	[Display(Name = "Message", Prompt = "Enter your message here...", Order = 3)]
	[Required(ErrorMessage = "Message is required")]
	[DataType(DataType.MultilineText)]
	public string Message { get; set; } = null!;
}
