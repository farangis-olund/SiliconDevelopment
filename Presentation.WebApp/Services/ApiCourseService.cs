using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Services;
using Newtonsoft.Json;
using Presentation.WebApp.ViewModels;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Presentation.WebApp.Services;
public class ApiCourseService(CategoryService categoryService, HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
{
	private readonly CategoryService _categoryService = categoryService;
	private readonly HttpClient _http = httpClient;
	private readonly IConfiguration _configuration = configuration;
	private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
	public async Task<CoursesViewModel> PopulateAllCoursesAsync()
	{
		var viewModel = new CoursesViewModel();
		var categoryResponse = await _categoryService.GetAllCategoriesAsync();

		if (categoryResponse.StatusCode == StatusCode.Ok)
		{
			viewModel.Categories = ((List<CategoryEntity>)categoryResponse.ContentResult!).Select(c => new Category { Id = c.Id, Name = c.Name }).ToList();
		}

		var response = await _http.GetAsync($"https://localhost:7267/api/courses?key={_configuration["ApiKey"]}");
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
		
		var response = await _http.GetAsync($"https://localhost:7267/api/course/{id}?key={_configuration["ApiKey"]}");
		var json = await response.Content.ReadAsStringAsync();
		var data = JsonConvert.DeserializeObject<ResponseResult>(json);

		if (data!.StatusCode == StatusCode.Ok)
		{
			var courseEntity = JsonConvert.DeserializeObject<CourseEntity>(data.ContentResult!.ToString()!);
			viewModel = MapToCourseModel(courseEntity!);
		}
			
		return viewModel;
	}

	public async Task<CourseModel> CreateOneCourseAsync(CourseModel model)
	{
		var responseResult = new ResponseResult();
		var httpContext = _httpContextAccessor.HttpContext;

		if (httpContext!.Request.Cookies.TryGetValue("AccessToken", out var token))
		{
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var jsonPayload = JsonConvert.SerializeObject(model);
			var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");


			var response = await _http.PostAsync($"https://localhost:7267/api/course?key={_configuration["ApiKey"]}", content);

			switch (response.StatusCode)
			{
				case HttpStatusCode.Created:
					var json = await response.Content.ReadAsStringAsync();
					responseResult = JsonConvert.DeserializeObject<ResponseResult>(json)!;
					break;
				case HttpStatusCode.Conflict:
					responseResult.StatusCode = StatusCode.Exists;
					break;
				case HttpStatusCode.Unauthorized:
					responseResult.StatusCode = StatusCode.Unauthorized;
					break;
				default:
					responseResult.StatusCode = StatusCode.Error;
					break;
			}
			
			return (CourseModel)responseResult.ContentResult!;
		}
		return model;
	}


	public async Task<CourseModel> DeleteOneCourseAsync(int id)
	{
		var viewModel = new CourseModel();
		var httpContext = _httpContextAccessor.HttpContext;

		if (httpContext!.Request.Cookies.TryGetValue("AccessToken", out var token))
		{
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var response = await _http.DeleteAsync($"https://localhost:7267/api/course/{id}?key={_configuration["ApiKey"]}");
			var json = await response.Content.ReadAsStringAsync();
			var data = JsonConvert.DeserializeObject<ResponseResult>(json);

			if (data!.StatusCode == StatusCode.Ok)
			{
				var courseEntity = JsonConvert.DeserializeObject<CourseEntity>(data.ContentResult!.ToString()!);
				viewModel = MapToCourseModel(courseEntity!);
			}
		}

		return viewModel;
	}

	public async Task<CourseModel> UpdateOneCourseAsync(CourseModel model)
	{
		var viewModel = new CourseModel();
		var httpContext = _httpContextAccessor.HttpContext;

		if (httpContext!.Request.Cookies.TryGetValue("AccessToken", out var token))
		{
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var jsonPayload = JsonConvert.SerializeObject(model);
			var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

			var response = await _http.PutAsync($"https://localhost:7267/api/course/{model.Id}?key={_configuration["ApiKey"]}", content);
			var json = await response.Content.ReadAsStringAsync();
			var data = JsonConvert.DeserializeObject<ResponseResult>(json);

			if (data!.StatusCode == StatusCode.Ok)
			{
				var courseEntity = JsonConvert.DeserializeObject<CourseEntity>(data.ContentResult!.ToString()!);
				viewModel = MapToCourseModel(courseEntity!);
			}
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
			DiscountPrice = entity.DiscountPrice,
			Duration = (double)entity.Duration!,
			Ingress = entity.Ingress,
			ProgramGoals = entity.ProgramGoals,
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
