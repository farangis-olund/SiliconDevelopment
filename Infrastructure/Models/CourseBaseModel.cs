
using Infrastructure.Entities;

namespace Infrastructure.Models;

public class CourseBaseModel
{
	public string Name { get; set; } = null!;
	public string Ingress { get; set; } = null!;
	public string Description { get; set; } = null!;
	public string? ProgramGoals { get; set; }
	public string? ProgramDetails { get; set; }
	public double Price { get; set; }
	public double? DiscountPrice { get; set; }
	public double Duration { get; set; }
	public int? DownloadedResourses { get; set; }
	public int? ArticleCount { get; set; }
	public int? ReviewsCount { get; set; }
	public int? LikeCount { get; set; }
	public int? LikePercentage { get; set; }
	public bool Digital { get; set; } = false;
	public bool BestSeller { get; set; } = false;
	public string? ImgUrl { get; set; }
	public int CategoryId { get; set; }
	public string CategoryName { get; set; } = null!;

	public string AuthorName { get; set; } = null!;
	public string AuthorDescription { get; set; } = null!;
	public int Subscribers { get; set; }
	public int Followers { get; set; }

	public static implicit operator CourseBaseModel(CourseEntity entity)
	{
		return new CourseBaseModel
		{
			Name = entity.Name,
			Ingress = entity.Ingress,
			Description = entity.Description,
			ProgramGoals = entity.ProgramGoals,
			ProgramDetails = entity.ProgramDetails,
			Price = entity.Price,
			DiscountPrice = entity.DiscountPrice,
			Duration = (double)entity.Duration!,
			DownloadedResourses = entity.DownloadedResourses,
			ArticleCount = entity.ArticleCount,
			ReviewsCount = entity.ReviewsCount,
			LikeCount = entity.LikeCount,
			LikePercentage = entity.LikePercentage,
			Digital = entity.Digital,
			BestSeller = entity.BestSeller,
			ImgUrl = entity.ImgUrl,
			CategoryId = entity.CategoryId,
			CategoryName = entity.Category.Name,
			AuthorName = entity.Author.AuthorName,
			AuthorDescription = entity.Author.AuthorDescription,
			Subscribers = entity.Author.Subscribers,
			Followers = entity.Author.Followers
		};
	}
}
