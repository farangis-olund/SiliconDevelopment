using Infrastructure.Dtos.Sections;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Infrastructure.Services;

public class FeatureService
{
	private readonly FeatureRepository _featureRepository;
	private readonly ILogger<FeatureService> _logger;

	public FeatureService(FeatureRepository featureRepository, ILogger<FeatureService> logger)
	{
		_featureRepository = featureRepository;
		_logger = logger;
	}

	public async Task<ResponseResult> AddFeatureAsync(FeatureEntity feature)
	{
		try
		{
			var existingFeature = await _featureRepository.GetOneAsync(c => c.Id == feature.Id);

			if (existingFeature != null)
			{
				return null!;
			}
			var newFeature = new FeatureEntity
			{
				Title = feature.Title,
				Ingress = feature.Ingress				
			};

			return await _featureRepository.AddAsync(newFeature);
		}
		catch (Exception ex)
		{
            return ResponseFactory.Error(ex.Message);
        }
	}

	public async Task<ResponseResult> GetFeatureAsync(int id)
	{
		try
		{
			return await _featureRepository.GetOneAsync(c => c.Id == id);

		}
		catch (Exception ex)
		{
            return ResponseFactory.Error(ex.Message);
        }
	}

	public async Task<ResponseResult> GetAllFeaturesAsync()
	{
		try
		{
			var FeatureEntities = await _featureRepository.GetAllAsync();

			return FeatureEntities;

		}
		catch (Exception ex)
		{
            return ResponseFactory.Error(ex.Message);
        }
	}

	public async Task<ResponseResult> UpdateFeatureAsync(FeatureEntity feature)
	{
		try
		{
			var response = await _featureRepository.GetOneAsync(c => c.Id == feature.Id);

			if (response.StatusCode == StatusCode.Ok)
			{
                var existingFeature = (FeatureEntity)response.ContentResult!;
                existingFeature.Title = feature.Title;
				existingFeature.Ingress = feature.Ingress;
				return await _featureRepository.UpdateAsync(c => c.Id == feature.Id, existingFeature);
			}
			else
				return null!;
		}
		catch (Exception ex)
		{
            return ResponseFactory.Error(ex.Message);
        }
	}

	public async Task<ResponseResult> DeleteFeatureAsync(int id)
	{
		try
		{
			var existingFeature = await _featureRepository.GetOneAsync(x => x.Id == id);

			if (existingFeature != null)
			{
				await _featureRepository.RemoveAsync(c => c.Id == id);
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
