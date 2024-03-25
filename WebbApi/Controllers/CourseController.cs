﻿using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseController (CourseService courseService) : ControllerBase
{
	private readonly CourseService _courseService = courseService;

	#region Create
	[HttpPost("/api/course")]
	public async Task<IActionResult> Create(CourseRegistrationModel model)
	{
		try
		{
			if (model == null)
			{
				return BadRequest();
			}

			var result = await _courseService.AddCourseAsync(model);

			return result.StatusCode switch
			{
				Infrastructure.Models.StatusCode.Ok => Created("", result.ContentResult),
				Infrastructure.Models.StatusCode.Exists => Conflict("Course already exists."),
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
	[HttpGet("/api/courses")]
	public async Task<IActionResult> GetAll()
	{
		try
		{
			var result = await _courseService.GetAllCoursesAsync();

			return result.StatusCode switch
			{
				Infrastructure.Models.StatusCode.Ok => Ok(result.ContentResult),
				Infrastructure.Models.StatusCode.NotFound => NotFound("Not found any subscription."),
				_ => BadRequest("An unexpected error occurred."),
			};
		}
		catch (Exception ex)
		{
			return BadRequest("An unexpected error occurred." + ex.Message);
		}

	}

	[HttpGet("/api/course/{id}")]
	public async Task<IActionResult> Get(int id)
	{
		try
		{
			var result = await _courseService.GetCourseAsync(id);

			return result.StatusCode switch
			{
				Infrastructure.Models.StatusCode.Ok => Ok(result.ContentResult),
				Infrastructure.Models.StatusCode.NotFound => NotFound("Not found any subscription."),
				_ => BadRequest("An unexpected error occurred."),
			};
		}
		catch (Exception ex)
		{
			return BadRequest("An unexpected error occurred." + ex.Message);
		}

	}
	#endregion

	#region Update
	[HttpPut("/api/course {id}")]
	public async Task<IActionResult> UpdateOne(int id, CourseModel model)
	{
		try
		{
			if (model == null)
			{
				return BadRequest();
			}

			var result = await _courseService.UpdateCourseAsync(id, model);

			return result.StatusCode switch
			{
				Infrastructure.Models.StatusCode.Ok => Ok("Subscription updated successfully!"),
				Infrastructure.Models.StatusCode.NotFound => NotFound("Subscription does not exist."),
				_ => BadRequest("An unexpected error occurred."),
			};
		}
		catch (Exception ex)
		{
			return BadRequest("An unexpected error occurred." + ex.Message);
		}
	}
	#endregion

	#region Delete
	[HttpDelete("/api/course {id}")]
	public async Task<IActionResult> DeleteOne(int id)
	{
		try
		{
			if (id == 0)
			{
				return BadRequest();
			}

			var result = await _courseService.DeleteCourseAsync(id);

			return result.StatusCode switch
			{
				Infrastructure.Models.StatusCode.Ok => Ok("Subscription deleted successfully!"),
				Infrastructure.Models.StatusCode.NotFound => NotFound("Subscription does not exist."),
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