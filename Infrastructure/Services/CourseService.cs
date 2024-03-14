
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class CourseService
{
	private readonly CourseRepository _courseRepository;

	public CourseService(CourseRepository courseRepository)
	{
		_courseRepository = courseRepository;

	}

	public async Task<ResponseResult> AddCourseAsync(CourseEntity course)
	{
		try
		{
			var existingcourse = await _courseRepository.GetOneAsync(c => c.Id == course.Id);

			if (existingcourse != null)
			{
				return null!;
			}
			
			return await _courseRepository.AddAsync(course);
		}
		catch (Exception ex)
		{
			return ResponseFactory.Error(ex.Message);
		}
	}

	public async Task<ResponseResult> GetCourseAsync(int id)
	{
		try
		{
			return await _courseRepository.GetOneAsync(c => c.Id == id);

		}
		catch (Exception ex)
		{
			return ResponseFactory.Error(ex.Message);
		}
	}

	public async Task<ResponseResult> GetAllCoursesAsync()
	{
		try
		{
			var courseEntities = await _courseRepository.GetAllAsync();

			if (courseEntities != null)
				return courseEntities;
			return ResponseFactory.NotFound();
		}
		catch (Exception ex)
		{
			return ResponseFactory.Error(ex.Message);
		}
	}

	public async Task<ResponseResult> UpdateCourseAsync(CourseEntity course)
	{
		try
		{
			var response = await _courseRepository.GetOneAsync(c => c.Id == course.Id);

			if (response.StatusCode == StatusCode.Ok)
			{
				var existingcourse = (CourseEntity)response.ContentResult!;
				existingcourse.Name = course.Name;
				existingcourse.Description = course.Description;
				existingcourse.CreatedDate = course.CreatedDate;
				existingcourse.Duration = course.Duration;
				existingcourse.ArticleCount	= course.ArticleCount;
				existingcourse.BestSeller = course.BestSeller;
				existingcourse.Digital = course.Digital;
				existingcourse.ProgramDetails = course.ProgramDetails;
				existingcourse.Price = course.Price;
				existingcourse.DiscountPrice = course.DiscountPrice;
				existingcourse.Ingress = course.Ingress;
				existingcourse.DownloadedResourses = course.DownloadedResourses;
				existingcourse.ImgUrl = course.ImgUrl;

				var updateResponse = await _courseRepository.UpdateAsync(c => c.Id == course.Id, existingcourse);

				return updateResponse;
			}
			else
			{
				return response;
			}
		}
		catch (Exception ex)
		{
			return ResponseFactory.Error(ex.Message);
		}
	}

	public async Task<ResponseResult> DeleteCourseAsync(int id)
	{
		try
		{
			var existingcourse = await _courseRepository.GetOneAsync(x => x.Id == id);

			if (existingcourse != null)
			{
				await _courseRepository.RemoveAsync(c => c.Id == id);
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
