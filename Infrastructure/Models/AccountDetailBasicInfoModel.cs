﻿
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class AccountDetailBasicInfoModel
{
	public string UserId { get; set; } = null!;
	[DataType(DataType.ImageUrl)]
	public string? ProfileImgUrl { get; set; }
	
	[Display(Name = "First name", Prompt = "Enter your first name", Order = 0)]
	[Required(ErrorMessage = "First name is required")]
	[DataType(DataType.Text)]
	public string FirstName { get; set; } = null!;


	[Display(Name = "Last name", Prompt = "Enter your last name", Order = 1)]
	[Required(ErrorMessage = "Last name is required")]
	[DataType(DataType.Text)]
	public string LastName { get; set; } = null!;


	[Display(Name = "Email address", Prompt = "Enter your email address", Order = 2)]
	[Required(ErrorMessage = "Email address is required")]
	[EmailAddress]
	[RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
	public string Email { get; set; } = null!;

	[Display(Name = "Phone", Prompt = "Enter your phone", Order = 3)]
	[Required(ErrorMessage = "Phone is required")]
	[DataType(DataType.PhoneNumber)]
	public string Phone { get; set; } = null!;

	[Display(Name = "Bio", Prompt = "Add a short bio...", Order = 4)]
	[DataType(DataType.MultilineText)]
	public string? Biography { get; set; }

}
