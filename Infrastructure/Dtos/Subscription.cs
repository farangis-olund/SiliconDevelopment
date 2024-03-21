using Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Dtos;

public class Subscription
{
	[Display(Name = "Email address", Prompt = "Enter your email address")]
	[DataType(DataType.EmailAddress)]
	[Required(ErrorMessage = "Email address is required")]
	public string Email { get; set; } = null!;
    public bool DailyNewsletter { get; set; } = false;
    public bool EventUpdates { get; set; } = false;
    public bool AdvertisingUpdates { get; set; } = false;
    public bool StartupsWeekly { get; set; } = false;
    public bool WeekInReview { get; set; } = false;
    public bool Podcasts { get; set; } = false;

	public static implicit operator Subscription(SubscriptionEntity entity)
	{
		return new Subscription
		{
			Email = entity.Email,
			DailyNewsletter = entity.DailyNewsletter,
			EventUpdates = entity.EventUpdates,
			AdvertisingUpdates = entity.AdvertisingUpdates,
			StartupsWeekly = entity.StartupsWeekly,
			WeekInReview = entity.WeekInReview,
			Podcasts = entity.Podcasts

		};
	}
}
