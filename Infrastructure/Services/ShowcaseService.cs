
using Infrastructure.Dtos.Sections;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Infrastructure.Services;

public class ShowcaseService
{
    private readonly ShowcaseRepository _showcaseRepository;
    private readonly ILogger<ShowcaseService> _logger;

    public ShowcaseService(ShowcaseRepository showcaseRepository, ILogger<ShowcaseService> logger)
    {
        _showcaseRepository = showcaseRepository;
        _logger = logger;
    }

    public async Task<ResponseResult> AddShowcaseAsync(ShowcaseEntity Showcase)
    {
        try
        {
            var existingShowcase = await _showcaseRepository.GetOneAsync(c => c.Id == Showcase.Id);

            if (existingShowcase != null)
            {
                return null!;
            }
            var newShowcase = new ShowcaseEntity
            {
                Title = Showcase.Title,
                Ingress = Showcase.Ingress
            };

            return await _showcaseRepository.AddAsync(newShowcase);
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    public async Task<ResponseResult> GetShowcaseAsync(int id)
    {
        try
        {
            return await _showcaseRepository.GetOneAsync(c => c.Id == id);

        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    public async Task<ResponseResult> GetAllShowcasesAsync()
    {
        try
        {
            var ShowcaseEntities = await _showcaseRepository.GetAllAsync();

            return ShowcaseEntities;

        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    public async Task<ResponseResult> UpdateShowcaseAsync(ShowcaseEntity Showcase)
    {
        try
        {
            var response = await _showcaseRepository.GetOneAsync(c => c.Id == Showcase.Id);

            if (response.StatusCode == StatusCode.Ok)
            {
                var existingShowcase = (ShowcaseEntity)response.ContentResult!;
                existingShowcase.Title = Showcase.Title;
                existingShowcase.Ingress = Showcase.Ingress;
                return await _showcaseRepository.UpdateAsync(c => c.Id == Showcase.Id, existingShowcase);
            }
            else
                return response;
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    public async Task<ResponseResult> DeleteShowcaseAsync(int id)
    {
        try
        {
            var existingShowcase = await _showcaseRepository.GetOneAsync(x => x.Id == id);

            if (existingShowcase != null)
            {
                await _showcaseRepository.RemoveAsync(c => c.Id == id);
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
