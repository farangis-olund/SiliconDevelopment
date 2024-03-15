using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.ViewModels;

namespace Presentation.WebApp.Controllers;

public class CoursesController(CourseService courseService, CategoryService categoryService) : Controller
{
    private readonly CourseService _courseService = courseService;
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
		var coursesResponse = await _courseService.GetAllCoursesAsync();

		if (categoryResponse.StatusCode == Infrastructure.Models.StatusCode.Ok)
		{
			viewModel.Categories = ((List<CategoryEntity>)categoryResponse.ContentResult!).Select(c => new Category { Id = c.Id, Name = c.Name }).ToList();
		}
		if (coursesResponse.StatusCode == Infrastructure.Models.StatusCode.Ok)
		{
			viewModel.Courses = ((List<CourseEntity>)coursesResponse.ContentResult!).Select(c => new CourseModel
			{
				Id = c.Id,
				Name = c.Name,
				Description = c.Description,
				AuthorName = c.Author.AuthorName,
				Price = c.Price,
				Duration = c.Duration,
				Ingress = c.Ingress,
				ProgramDetails = c.ProgramDetails,
				DownloadedResourses = c.DownloadedResourses,
				ArticleCount = c.ArticleCount,
				ReviewsCount = c.ReviewsCount,
				LikeCount = c.LikeCount,
				Digital = c.Digital,
				BestSeller = c.BestSeller,
				ImgUrl = c.ImgUrl,
				CategoryName = c.Category.Name,
				AuthorDescription = c.Author.AuthorDescription,
				Subscribers = c.Author.Subscribers,
				Followers = c.Author.Followers
			}).ToList();
		}
		return viewModel;
	}

	private async Task<CourseModel> PopulateOneCourseAsync(int id)
	{
		var viewModel = new CourseModel();

		var courseResponse = await _courseService.GetCourseAsync(id);

		if (courseResponse.StatusCode == Infrastructure.Models.StatusCode.Ok)
		{
			var c = (CourseEntity)courseResponse.ContentResult!;

			viewModel = new CourseModel
			{
				Id = c.Id,
				Name = c.Name,
				Description = c.Description,
				AuthorName = c.Author.AuthorName,
				Price = c.Price,
				Duration = c.Duration,
				Ingress = c.Ingress,
				ProgramDetails = c.ProgramDetails,
				DownloadedResourses = c.DownloadedResourses,
				ArticleCount = c.ArticleCount,
				ReviewsCount = c.ReviewsCount,
				LikeCount = c.LikeCount,
				Digital = c.Digital,
				BestSeller = c.BestSeller,
				ImgUrl = c.ImgUrl,
				CategoryName = c.Category.Name,
				AuthorDescription = c.Author.AuthorDescription,
				Subscribers = c.Author.Subscribers,
				Followers = c.Author.Followers
			};
		}

		return viewModel;
	}
}
