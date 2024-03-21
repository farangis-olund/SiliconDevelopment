using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Services;
using Newtonsoft.Json;
using Presentation.WebApp.ViewModels;

namespace Presentation.WebApp.Services;
public class ApiCourseService
{
	private readonly CategoryService _categoryService;
	private readonly HttpClient _http;

	public ApiCourseService(CategoryService categoryService, HttpClient httpClient)
	{
		_categoryService = categoryService;
		_http = httpClient;
	}

	public async Task<CoursesViewModel> PopulateAllCoursesAsync()
	{
		var viewModel = new CoursesViewModel();

		var categoryResponse = await _categoryService.GetAllCategoriesAsync();

		if (categoryResponse.StatusCode == StatusCode.Ok)
		{
			viewModel.Categories = ((List<CategoryEntity>)categoryResponse.ContentResult!).Select(c => new Category { Id = c.Id, Name = c.Name }).ToList();
		}
				
		var response = await _http.GetAsync("https://localhost:7267/api/courses");
		var json = await response.Content.ReadAsStringAsync();
		var data = JsonConvert.DeserializeObject<ResponseResult>(json);

		if (data!.StatusCode == StatusCode.Ok)
		{
			var courseEntities = JsonConvert.DeserializeObject<List<CourseEntity>>(data.ContentResult!.ToString()!);
			viewModel.Courses = courseEntities!.Select(MapToCourseModel).ToList();
		}
		return viewModel;
	}

	public async Task<CourseModel> PopulateOneCourseAsync(int id)
	{
		var viewModel = new CourseModel();
			
		var response = await _http.GetAsync($"https://localhost:7267/api/course/{id}");
		var json = await response.Content.ReadAsStringAsync();
		var data = JsonConvert.DeserializeObject<ResponseResult>(json);

		if (data!.StatusCode == StatusCode.Ok)
		{
			var courseEntity = JsonConvert.DeserializeObject<CourseEntity>(data.ContentResult!.ToString()!);
			viewModel = MapToCourseModel(courseEntity!);
		}

		return viewModel;
	}

	private CourseModel MapToCourseModel(CourseEntity entity)
	{
		return new CourseModel
		{
			Id = entity.Id,
			Name = entity.Name,
			Description = entity.Description,
			AuthorName = entity.Author.AuthorName,
			Price = entity.Price,
			Duration = (double)entity.Duration!,
			Ingress = entity.Ingress,
			ProgramDetails = entity.ProgramDetails,
			DownloadedResourses = entity.DownloadedResourses,
			ArticleCount = entity.ArticleCount,
			ReviewsCount = entity.ReviewsCount,
			LikeCount = entity.LikeCount,
			Digital = entity.Digital,
			BestSeller = entity.BestSeller,
			ImgUrl = entity.ImgUrl,
			CategoryName = entity.Category.Name,
			AuthorDescription = entity.Author.AuthorDescription,
			Subscribers = entity.Author.Subscribers,
			Followers = entity.Author.Followers
		};
	}
}
