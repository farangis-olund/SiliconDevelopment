using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class FeatureItemService
{
	private readonly FeatureItemRepository _featureItemRepository;
	private readonly ILogger<FeatureService> _logger;

	public FeatureItemService(FeatureItemRepository FeatureItemRepository, ILogger<FeatureService> logger)
	{
		_featureItemRepository = FeatureItemRepository;
		_logger = logger;
	}

	public async Task<ResponseResult> AddFeatureItemAsync(FeatureItemEntity featureItem)
	{
		try
		{
			var existingFeatureItem = await _featureItemRepository.GetOneAsync(c => c.Id == featureItem.Id);

			if (existingFeatureItem != null)
			{
				return null!;
			}
			var newFeatureItem = new FeatureItemEntity
			{
				Title = featureItem.Title,
				Text = featureItem.Text
			};

			return await _featureItemRepository.AddAsync(newFeatureItem);
		}
		catch (Exception ex)
		{
            return ResponseFactory.Error(ex.Message);
        }
	}

	public async Task<ResponseResult> GetFeatureItemAsync(int id)
	{
		try
		{
			return await _featureItemRepository.GetOneAsync(c => c.Id == id);

		}
		catch (Exception ex)
		{
            return ResponseFactory.Error(ex.Message);
        }
	}

	public async Task<ResponseResult> GetAllFeatureItemsAsync()
	{
		try
		{
			var FeatureItemEntities = await _featureItemRepository.GetAllAsync();

			return FeatureItemEntities;

		}
		catch (Exception ex)
		{
            return ResponseFactory.Error(ex.Message);
        }
	}

	public async Task<ResponseResult> UpdateFeatureItemAsync(FeatureItemEntity featureItem)
	{
		try
		{
			var response = await _featureItemRepository.GetOneAsync(c => c.Id == featureItem.Id);

			if (response.StatusCode == StatusCode.Ok)
			{
                var existingFeatureItem = (FeatureItemEntity)response.ContentResult!;
                existingFeatureItem.Title = featureItem.Title;
				existingFeatureItem.ImgUrl = featureItem.ImgUrl;
				existingFeatureItem.Text = featureItem.Text;
				return await _featureItemRepository.UpdateAsync(c => c.Id == featureItem.Id, existingFeatureItem);
			}
			else
				return null!;
		}
		catch (Exception ex)
		{
            return ResponseFactory.Error(ex.Message);
        }
	}

	public async Task<ResponseResult> DeleteFeatureItemAsync(int id)
	{
		try
		{
			var existingFeature = await _featureItemRepository.GetOneAsync(x => x.Id == id);

			if (existingFeature != null)
			{
				await _featureItemRepository.RemoveAsync(c => c.Id == id);
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
