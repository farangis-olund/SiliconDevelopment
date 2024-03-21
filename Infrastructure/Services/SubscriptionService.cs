using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class SubscriptionService(SubscriptionRepository subscriptionRepository)
{
	private readonly SubscriptionRepository _subscriptionRepository = subscriptionRepository;

	public async Task<ResponseResult> AddSubscriptionAsync(Subscription subscription)
	{
		try
		{
			var existingSubscription = await _subscriptionRepository.GetOneAsync(c => c.Email == subscription.Email);

			if (existingSubscription != null)
			{
				return ResponseFactory.Exists("Email already exists");
			}
			
			var result = await _subscriptionRepository.AddAsync(subscription);
			return ResponseFactory.Ok(result);
		}
		catch (Exception ex)
		{
			return ResponseFactory.Error(ex.Message);
		}
	}

	public async Task<ResponseResult> GetSubscriptionAsync(int id)
	{
		try
		{
			return await _subscriptionRepository.GetOneAsync(c => c.Id == id);

		}
		catch (Exception ex)
		{
			return ResponseFactory.Error(ex.Message);
		}
	}

	public async Task<ResponseResult> GetAllSubscriptionsAsync()
	{
		try
		{
			var subscriptionEntities = await _subscriptionRepository.GetAllAsync();

			if (subscriptionEntities != null)
				return subscriptionEntities;
			return ResponseFactory.NotFound();
		}
		catch (Exception ex)
		{
			return ResponseFactory.Error(ex.Message);
		}
	}

	public async Task<ResponseResult> UpdateSubscriptionAsync(int id, SubscriptionEntity subscription)
	{
		try
		{
			var response = await _subscriptionRepository.GetOneAsync(c => c.Id == id);

			if (response.StatusCode == StatusCode.Ok)
			{
				var existingsubscription = (SubscriptionEntity)response.ContentResult!;
				existingsubscription.Email = subscription.Email;
				existingsubscription.EventUpdates = subscription.EventUpdates;
				existingsubscription.AdvertisingUpdates = subscription.AdvertisingUpdates;
				existingsubscription.DailyNewsletter = subscription.DailyNewsletter;
				existingsubscription.WeekInReview = subscription.WeekInReview;
				existingsubscription.StartupsWeekly = subscription.StartupsWeekly;
				existingsubscription.Podcasts = subscription.Podcasts;

				var updateResponse = await _subscriptionRepository.UpdateAsync(c => c.Id == id, existingsubscription);

				return updateResponse;
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

	public async Task<ResponseResult> DeleteSubscriptionAsync(string email)
	{
		try
		{
			var existingsubscription = await _subscriptionRepository.GetOneAsync(x => x.Email == email);

			if (existingsubscription != null)
			{
				await _subscriptionRepository.RemoveAsync(c => c.Email == email);
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
