
using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[UseApiKey]
public class ContactController(ContactService contactService) : ControllerBase
{
	private readonly ContactService _contactService = contactService;

	#region Create
	[HttpPost("/api/contact")]
	public async Task<IActionResult> Create(ContactModel model)
	{
		try
		{
			if (model == null)
			{
				return BadRequest();
			}

			var result = await _contactService.AddContactAsync(model);

			return result.StatusCode switch
			{
				Infrastructure.Models.StatusCode.Ok => Created(result.Message, result.ContentResult),
				Infrastructure.Models.StatusCode.Exists => Conflict(result.Message),
				_ => BadRequest("An unexpected error occurred."),
			};
		}
		catch (Exception ex)
		{
			return BadRequest("An unexpected error occurred." + ex.Message);
		}

	}
	#endregion

	#region Get
	[HttpGet("/api/contacts")]
	public async Task<IActionResult> GetAll()
	{
		try
		{
			var result = await _contactService.GetAllContactsAsync();

			return result.StatusCode switch
			{
				Infrastructure.Models.StatusCode.Ok => Ok(result.ContentResult),
				Infrastructure.Models.StatusCode.NotFound => NotFound("Not found any courses."),
				Infrastructure.Models.StatusCode.Unauthorized => Unauthorized("You are unauthorized to get courses!"),
				_ => BadRequest("An unexpected error occurred."),
			};
		}
		catch (Exception ex)
		{
			return BadRequest("An unexpected error occurred." + ex.Message);
		}

	}

	[HttpGet("/api/contact/{id}")]
	public async Task<IActionResult> Get(int id)
	{
		try
		{
			var result = await _contactService.GetContactAsync(id);

			return result.StatusCode switch
			{
				Infrastructure.Models.StatusCode.Ok => Ok(result.ContentResult),
				Infrastructure.Models.StatusCode.NotFound => NotFound("Not found any course."),
				Infrastructure.Models.StatusCode.Unauthorized => Unauthorized("You are unauthorized to get course!"),
				_ => BadRequest("An unexpected error occurred.")
			};
		}
		catch (Exception ex)
		{
			return BadRequest("An unexpected error occurred." + ex.Message);
		}

	}
	#endregion
	
		
	#region Delete
	[HttpDelete("/api/contact/{id}")]
	public async Task<IActionResult> DeleteOne(int id)
	{
		try
		{
			if (id == 0)
			{
				return BadRequest();
			}

			var result = await _contactService.DeleteContactAsync(id);

			return result.StatusCode switch
			{
				Infrastructure.Models.StatusCode.Ok => Ok("Course deleted successfully!"),
				Infrastructure.Models.StatusCode.NotFound => NotFound("Course does not exist."),
				Infrastructure.Models.StatusCode.Unauthorized => Unauthorized("You are unauthorized to delete!"),
				_ => BadRequest("An unexpected error occurred."),
			};
		}
		catch (Exception ex)
		{
			return BadRequest("An unexpected error occurred." + ex.Message);
		}
	}
	#endregion
}
