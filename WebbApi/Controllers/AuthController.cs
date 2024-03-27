using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Filters;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(ApiUserRepository apiUserRepository, IConfiguration configuration) : ControllerBase
{
	private readonly ApiUserRepository _apiUserRepository = apiUserRepository;
	private readonly IConfiguration _configuration = configuration;


	[HttpPost]
	[Route("register")]
	public async Task<ActionResult> Register (ApiUserRegistrationModel model)
	{
		if (ModelState.IsValid)
		{
			var response = await _apiUserRepository.GetOneAsync(x => x.Email == model.Email);
			if (response.StatusCode == Infrastructure.Models.StatusCode.NotFound)
			{
				await _apiUserRepository.AddAsync(ApiUserFactory.Create(model));
				return Created("", null);
			}

			return Conflict();
		}

		return BadRequest();
	}

	[HttpPost]
	[Route("login")]
	public async Task<ActionResult> Login(ApiUserRegistrationModel model)
	{
		if (ModelState.IsValid)
		{
			var response = await _apiUserRepository.GetOneAsync(x => x.Email == model.Email);
			var apiUserEntity = response.ContentResult as ApiUserEntity;
			if (response.StatusCode == Infrastructure.Models.StatusCode.Ok && PasswordHasherService.ValidateSecurePassword(model.Password, apiUserEntity!.Password))
			{
				var tokenHandler = new JwtSecurityTokenHandler();
				var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!);
				var tokenDescriptor = new SecurityTokenDescriptor
				{
					Subject = new ClaimsIdentity(new Claim[]
					{
						new(ClaimTypes.NameIdentifier, apiUserEntity.Id.ToString()),
						new(ClaimTypes.Email, apiUserEntity.Email),
						new(ClaimTypes.Name, apiUserEntity.Email)

					}),
					Expires = DateTime.UtcNow.AddDays(7),
					Issuer = _configuration["Jwt:Issuer"],
					Audience = _configuration["Jwt:Audience"],
					SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)

				};

				var token = tokenHandler.CreateToken(tokenDescriptor);
				var tokenString = tokenHandler.WriteToken(token);

				return Ok(tokenString);
			}
			return NotFound();
		}

		return Unauthorized();
	}

	[UseApiKey]
	[HttpPost]
	[Route("token")]
	public ActionResult GetToken(ApiFormModel model)
	{
		if (ModelState.IsValid)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new(ClaimTypes.Email, model.Email),
					new(ClaimTypes.Name, model.Email)

				}),
				Expires = DateTime.UtcNow.AddDays(1),
				Issuer = _configuration["Jwt:Issuer"],
				Audience = _configuration["Jwt:Audience"],
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			var tokenString = tokenHandler.WriteToken(token);

			return Ok(tokenString);
		}
		
		return Unauthorized();
	}


}
