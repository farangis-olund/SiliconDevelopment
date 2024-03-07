using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using System.Diagnostics;


namespace Infrastructure.Services
{
    public class SubscriptionSectionService
    {
        private readonly SubscriptionSectionRepository _subscriptionSectionRepository;
        private readonly ILogger<SubscriptionSectionService> _logger;

        public SubscriptionSectionService(SubscriptionSectionRepository subscriptionSectionRepository, ILogger<SubscriptionSectionService> logger)
        {
            _subscriptionSectionRepository = subscriptionSectionRepository;
            _logger = logger;
        }

        public async Task<ResponseResult> AddSubscriptionSectionAsync(SubscriptionSectionEntity subscriptionSection)
        {
            try
            {
                var existingSubscriptionSection = await _subscriptionSectionRepository.GetOneAsync(c => c.Id == subscriptionSection.Id);

                if (existingSubscriptionSection != null)
                {
                    return null!;
                }
                var newSubscriptionSection = new SubscriptionSectionEntity
                {
                    Title = subscriptionSection.Title,
                    Description = subscriptionSection.Description
                };

                return await _subscriptionSectionRepository.AddAsync(newSubscriptionSection);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in adding Subscription Section: {ex.Message}");
                Debug.WriteLine(ex.Message);
                return null!;
            }
        }

        public async Task<ResponseResult> GetSubscriptionSectionAsync(int id)
        {
            try
            {
                return await _subscriptionSectionRepository.GetOneAsync(c => c.Id == id);

            }
            catch (Exception ex)
            {
                return ResponseFactory.Error(ex.Message);
            }
        }

        public async Task<ResponseResult> GetAllSubscriptionSectionsAsync()
        {
            try
            {
                var subscriptionSectionEntities = await _subscriptionSectionRepository.GetAllAsync();

                return subscriptionSectionEntities;

            }
            catch (Exception ex)
            {
                return ResponseFactory.Error(ex.Message);
            }
        }

        public async Task<ResponseResult> UpdateSubscriptionSectionAsync(SubscriptionSectionEntity subscriptionSection)
        {
            try
            {
                var response = await _subscriptionSectionRepository.GetOneAsync(c => c.Id == subscriptionSection.Id);

                if (response.StatusCode == StatusCode.Ok)
                {
                    var existingSubscriptionSection = (SubscriptionSectionEntity)response.ContentResult!;
                    existingSubscriptionSection.Title = subscriptionSection.Title;
                    existingSubscriptionSection.Description = subscriptionSection.Description;
                    existingSubscriptionSection.CheckBoxs = subscriptionSection.CheckBoxs;
                    return await _subscriptionSectionRepository.UpdateAsync(c => c.Id == subscriptionSection.Id, existingSubscriptionSection);
                }
                else
                    return response;
            }
            catch (Exception ex)
            {
                return ResponseFactory.Error(ex.Message);
            }
        }

        public async Task<ResponseResult> DeleteSubscriptionSectionAsync(int id)
        {
            try
            {
                var existingSubscriptionSection = await _subscriptionSectionRepository.GetOneAsync(x => x.Id == id);

                if (existingSubscriptionSection != null)
                {
                    await _subscriptionSectionRepository.RemoveAsync(c => c.Id == id);
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
}
