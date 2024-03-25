
using Infrastructure.Models;

namespace Infrastructure.Entities;

public class CourseEntity
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public string Ingress { get; set; } = null!;
	public string Description { get; set; } = null!;
	public string? ProgramGoals { get; set; }
	public string? ProgramDetails { get; set; }
	public DateTime? CreatedDate { get; set; }
	public double Price { get; set; }
	public double? DiscountPrice { get; set; }
	public double? Duration { get; set; } 
	public int? DownloadedResourses { get; set; }
	public int? ArticleCount { get; set; }
	public int? ReviewsCount { get; set; }
	public int? LikeCount { get; set; }
	public int? LikePercentage { get; set; }
	public bool Digital { get; set; } = false;
	public bool BestSeller { get; set; } = false;
	public string? ImgUrl { get; set; } 
	public int CategoryId { get; set; }
		
	public CategoryEntity Category { get; set; } = null!;
	public int AuthorId { get; set; }
	
	public AuthorEntity Author { get; set; } = null!;

	public static implicit operator CourseEntity(CourseModel model)
	{
		return ToCourseEntity(model);
	}

	public static implicit operator CourseEntity(CourseRegistrationModel model)
	{
		return ToCourseEntity(model);
	}

	public static CourseEntity ToCourseEntity(CourseBaseModel model)
	{
		return new CourseEntity
		{
			Name = model.Name,
			Ingress = model.Ingress,
			Description = model.Description,
			ProgramGoals = model.ProgramGoals,
			ProgramDetails = model.ProgramDetails,
			Price = model.Price,
			DiscountPrice = model.DiscountPrice,
			Duration = (double)model.Duration!,
			DownloadedResourses = model.DownloadedResourses,
			ArticleCount = model.ArticleCount,
			ReviewsCount = model.ReviewsCount,
			LikeCount = model.LikeCount,
			LikePercentage = model.LikePercentage,
			Digital = model.Digital,
			BestSeller = model.BestSeller,
			ImgUrl = model.ImgUrl
		};
	}

}
