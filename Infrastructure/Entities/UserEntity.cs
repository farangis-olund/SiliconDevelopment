﻿using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entities;

public class UserEntity : IdentityUser
{
    [ProtectedPersonalData]
    public string FirstName { get; set; } = null!;
    
    [ProtectedPersonalData]
    public string LastName { get; set; } = null!;

	public string? Bio { get; set; }

	[ProtectedPersonalData]
    public string? ProfileImgUrl { get; set; }
	
    [ProtectedPersonalData]
	public bool IsExternalAccount { get; set; } = false;

    
    public int? AddressId { get; set; }
    public AddressEntity? Address { get; set; }
}
