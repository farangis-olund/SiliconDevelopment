
using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Services;

namespace Infrastructure.Factories;

public class ApiUserFactory
{
	public static ApiUserEntity Create(ApiUserRegistrationModel model)
	{
		try
		{
			return new ApiUserEntity
			{
				Id = Guid.NewGuid().ToString(),
				FirstName = model.FirstName,
				LastName = model.LastName,
				Email = model.Email,
				Password = PasswordHasherService.GenerateSecurePassword(model.Password)
			};
		}
		catch { }
		return null!;
	}
}
