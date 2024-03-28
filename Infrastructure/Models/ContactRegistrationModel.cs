﻿namespace Infrastructure.Models;

public class ContactRegistrationModel
{
	public string FullName { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string? ServiceName { get; set; }
	public string Message { get; set; } = null!;


}