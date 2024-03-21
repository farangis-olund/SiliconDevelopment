using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.WebApp.ViewModels;

namespace Presentation.WebApp.Controllers;

[Authorize]
public class CoursesController(CategoryService categoryService) : Controller
{
    private readonly CategoryService _categoryService = categoryService;

	#region Index
	[HttpGet]
	[Route("/courses")]
	public async Task<IActionResult> Index()
	{
		var viewModel = await PopulateAllCoursesAsync();

		return View(viewModel);
	}

    [HttpPost]
    [Route("/courses")]
    public IActionResult Index(CoursesViewModel viewModel)
    {
       return View(viewModel);
    }
    #endregion

    #region Single course
    [HttpGet]
    public async Task<IActionResult> SingleCourse(int id)
    {
			var viewModel= await PopulateOneCourseAsync(id);
	
		return View(viewModel);
    }

	#endregion

	private async Task<CoursesViewModel> PopulateAllCoursesAsync()
	{
		var viewModel = new CoursesViewModel();

		var categoryResponse = await _categoryService.GetAllCategoriesAsync();
		
		if (categoryResponse.StatusCode == Infrastructure.Models.StatusCode.Ok)
		{
			viewModel.Categories = ((List<CategoryEntity>)categoryResponse.ContentResult!).Select(c => new Category { Id = c.Id, Name = c.Name }).ToList();
		}

		using var http = new HttpClient();
		var response = await http.GetAsync("https://localhost:7267/api/courses");
		var json = await response.Content.ReadAsStringAsync();
		var data = JsonConvert.DeserializeObject<ResponseResult>(json);

		if (data!.StatusCode == Infrastructure.Models.StatusCode.Ok)
		{
			var courseEntities = JsonConvert.DeserializeObject<List<CourseEntity>>(data.ContentResult!.ToString()!);
			viewModel.Courses = courseEntities!.Select(MapToCourseModel).ToList();
		}
		return viewModel;
	}

	private async Task<CourseModel> PopulateOneCourseAsync(int id)
	{
		var viewModel = new CourseModel();

		using var http = new HttpClient();
		var response = await http.GetAsync($"https://localhost:7267/api/course/{id}");
		var json = await response.Content.ReadAsStringAsync();
		var data = JsonConvert.DeserializeObject<ResponseResult>(json);

		if (data!.StatusCode == Infrastructure.Models.StatusCode.Ok)
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
