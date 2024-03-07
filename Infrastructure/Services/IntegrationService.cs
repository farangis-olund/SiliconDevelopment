
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Infrastructure.Services;

public class IntegrationService
{
    private readonly IntegrationRepository _integrationRepository;
    
    public IntegrationService(IntegrationRepository integrationRepository)
    {
        _integrationRepository = integrationRepository;
       
    }

    public async Task<ResponseResult> AddIntegrationAsync(IntegrationEntity integration)
    {
        try
        {
            var existingIntegration = await _integrationRepository.GetOneAsync(c => c.Id == integration.Id);

            if (existingIntegration != null)
            {
                return null!;
            }
            var newIntegration = new IntegrationEntity
            {
                Title = integration.Title,
                Ingress = integration.Ingress
            };

            return await _integrationRepository.AddAsync(newIntegration);
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    public async Task<ResponseResult> GetIntegrationAsync(int id)
    {
        try
        {
            return await _integrationRepository.GetOneAsync(c => c.Id == id);

        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    public async Task<ResponseResult> GetAllIntegrationsAsync()
    {
        try
        {
            var IntegrationEntities = await _integrationRepository.GetAllAsync();

            return IntegrationEntities;

        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    public async Task<ResponseResult> UpdateIntegrationAsync(IntegrationEntity integration)
    {
        try
        {
            var response = await _integrationRepository.GetOneAsync(c => c.Id == integration.Id);

            if (response.StatusCode == StatusCode.Ok)
            {
                var existingIntegration = (IntegrationEntity)response.ContentResult!;
                existingIntegration.Title = integration.Title;
                existingIntegration.Ingress = integration.Ingress;
                return await _integrationRepository.UpdateAsync(c => c.Id == integration.Id, existingIntegration);
            }
            else
                return response;
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    public async Task<ResponseResult> DeleteIntegrationAsync(int id)
    {
        try
        {
            var existingIntegration = await _integrationRepository.GetOneAsync(x => x.Id == id);

            if (existingIntegration != null)
            {
                await _integrationRepository.RemoveAsync(c => c.Id == id);
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
