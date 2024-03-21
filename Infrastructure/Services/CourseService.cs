using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class CourseService(CourseRepository courseRepository)
{
	private readonly CourseRepository _courseRepository = courseRepository;

	public async Task<ResponseResult> AddCourseAsync(CourseEntity course)
	{
		try
		{
			var existingcourse = await _courseRepository.GetOneAsync(c => c.Name == course.Name);

			if (existingcourse != null)
			{
				return ResponseFactory.Exists();
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
			var result = await _courseRepository.GetOneAsync(c => c.Id == id);
			return ResponseFactory.Ok(result);

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
				return ResponseFactory.Ok(courseEntities);
			return ResponseFactory.NotFound();
		}
		catch (Exception ex)
		{
			return ResponseFactory.Error(ex.Message);
		}
	}

	public async Task<ResponseResult> UpdateCourseAsync(int id, CourseEntity course)
	{
		try
		{
			var response = await _courseRepository.GetOneAsync(c => c.Id == id);

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

				return ResponseFactory.Ok(updateResponse);
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
