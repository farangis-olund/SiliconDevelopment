﻿
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class UserCourseService(UserCourseRepository userCourseRepository)
{
	private readonly UserCourseRepository _userCourseRepository = userCourseRepository;

	public async Task<ResponseResult> AddUserCourseAsync(string userId, int courseId)
	{
		try
		{
			var existinguserCourse = await _userCourseRepository.GetOneAsync(c => c.UserId == userId && c.CourseId == courseId);

			if (existinguserCourse.StatusCode == StatusCode.Ok)
			{
				return ResponseFactory.Exists();
			}
			var newuserCourse = new UserCourseEntity
			{
				UserId = userId, 
				CourseId = courseId
			};

			var result = await _userCourseRepository.AddAsync(newuserCourse);
			return ResponseFactory.Ok(result);
		}
		catch (Exception ex)
		{
			return ResponseFactory.Error(ex.Message);
		}
	}

	public async Task<ResponseResult> GetUserCourseAsync(string userId, int courseId)
	{
		try
		{
			var result = await _userCourseRepository.GetOneAsync(c => c.UserId == userId && c.CourseId == courseId);
			return ResponseFactory.Ok(result);
		}
		catch (Exception ex)
		{
			return ResponseFactory.Error(ex.Message);
		}
	}

	public async Task<ResponseResult> GetAllUserCoursesAsync(string userId)
	{
		try
		{
			var userCourseEntities = await _userCourseRepository.GetAllAsync(c => c.UserId == userId);

			if (userCourseEntities.StatusCode == StatusCode.Ok)
				return ResponseFactory.Ok(userCourseEntities.ContentResult!);
			return ResponseFactory.NotFound();
		}
		catch (Exception ex)
		{
			return ResponseFactory.Error(ex.Message);
		}
	}
	
	public async Task<ResponseResult> DeleteUserCourseAsync(string userId, int courseId)
	{
		try
		{
			var existinguserCourse = await _userCourseRepository.GetOneAsync(c => c.UserId == userId && c.CourseId == courseId);

			if (existinguserCourse.StatusCode == StatusCode.Ok)
			{
				await _userCourseRepository.RemoveAsync(c => c.UserId == userId && c.CourseId == courseId);
				return ResponseFactory.Ok("Successfully removed!");
			}

			return ResponseFactory.NotFound();
		}
		catch (Exception ex)
		{
			return ResponseFactory.Error(ex.Message);
		}
	}

	public async Task<ResponseResult> DeleteAllUserCoursesAsync(string userId)
	{
		try
		{
			var existinguserCourse = await _userCourseRepository.GetAllAsync(c => c.UserId == userId);

			if (existinguserCourse.StatusCode == StatusCode.Ok)
			{
				await _userCourseRepository.RemoveAsync(c => c.UserId == userId);
				return ResponseFactory.Ok("Successfully removed!");
			}

			return ResponseFactory.NotFound();
		}
		catch (Exception ex)
		{
			return ResponseFactory.Error(ex.Message);
		}
	}
}
