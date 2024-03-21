
using Infrastructure.Dtos;

namespace Infrastructure.Entities;

public class SubscriptionEntity
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public bool DailyNewsletter { get; set; } = false;
    public bool EventUpdates { get; set; } = false;
    public bool AdvertisingUpdates { get; set; } = false;
    public bool StartupsWeekly { get; set; } = false;
    public bool WeekInReview { get; set; } = false;
    public bool Podcasts { get; set; } = false;

	public static implicit operator SubscriptionEntity(Subscription dto)
	{
		return new SubscriptionEntity
		{
			Email = dto.Email,
            DailyNewsletter = dto.DailyNewsletter,
            EventUpdates = dto.EventUpdates,
            AdvertisingUpdates = dto.AdvertisingUpdates,
            StartupsWeekly = dto.StartupsWeekly,
            WeekInReview = dto.WeekInReview,
            Podcasts = dto.Podcasts,

		};
	}
}
