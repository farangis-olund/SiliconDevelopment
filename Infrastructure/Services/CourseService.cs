using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class CourseService(CourseRepository courseRepository, AuthorRepository authorRepository, CategoryRepository categoryRepository)
{
	private readonly CourseRepository _courseRepository = courseRepository;
	private readonly AuthorRepository _authorRepository = authorRepository;
	private readonly CategoryRepository _categoryRepository = categoryRepository;
	public async Task<ResponseResult> AddCourseAsync(CourseRegistrationModel course)
	{
		try
		{
			var existingcourse = await _courseRepository.GetOneAsync(c => c.Name == course.Name);

			if (existingcourse.StatusCode == StatusCode.Ok)
			{
				return ResponseFactory.Exists();
			}

			var authorEntity = await _authorRepository.GetOneAsync(c => c.AuthorName == course.AuthorName);
			var authorId = authorEntity.StatusCode == StatusCode.Ok
				? ((AuthorEntity)authorEntity.ContentResult!).Id
				: ((AuthorEntity)(await _authorRepository.AddAsync(new AuthorEntity
				{
					AuthorName = course.AuthorName,
					AuthorDescription = course.AuthorDescription,
					Subscribers = course.Subscribers,
					Followers = course.Followers
				})).ContentResult!).Id;

			var categoryEntity = await _categoryRepository.GetOneAsync(c => c.Name == course.CategoryName);
			var categoryId = categoryEntity.ContentResult != null
				? ((CategoryEntity)categoryEntity.ContentResult).Id
				: ((CategoryEntity)(await _categoryRepository.AddAsync(new CategoryEntity
				{
					Name = course.CategoryName,
				})).ContentResult!).Id;

			var newCourseEntity = CourseEntity.ToCourseEntity(course);
			newCourseEntity.AuthorId = authorId;
			newCourseEntity.CategoryId = categoryId;
			
			var result = await _courseRepository.AddAsync(newCourseEntity);
			
			return ResponseFactory.Ok(result);
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
