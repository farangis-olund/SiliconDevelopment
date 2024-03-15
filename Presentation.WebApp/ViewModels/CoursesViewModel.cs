using Infrastructure.Dtos;
using Infrastructure.Models;

namespace Presentation.WebApp.ViewModels;

public class CoursesViewModel
{
	public string Title { get; set; } = "Courses";
	public string SearchQuery { get; set; } = null!;
    public int SelectedCategoryId { get; set; }
	public List<Category> Categories { get; set; } = [];
	public int SelectedCourseId { get; set; }
	public List<CourseModel> Courses { get; set; } = [];

}

