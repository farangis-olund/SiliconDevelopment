﻿
namespace Infrastructure.Entities;

public class ApiUserEntity
{
	public string Id { get; set; } = null!;
	public string FirstName { get; set; } = null!;
	public string LastName { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string Password { get; set; } = null!;
}
