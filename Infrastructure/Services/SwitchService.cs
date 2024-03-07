
using Infrastructure.Dtos.Sections;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Infrastructure.Services;

public class SwitchService
{
    private readonly SwitchRepository _switchRepository;
    private readonly ILogger<SwitchService> _logger;

    public SwitchService(SwitchRepository switchRepository, ILogger<SwitchService> logger)
    {
        _switchRepository = switchRepository;
        _logger = logger;
    }

    public async Task<ResponseResult> AddSwitchAsync(SwitchEntity switchEntity)
    {
        try
        {
            var existingSwitch = await _switchRepository.GetOneAsync(c => c.Id == switchEntity.Id);

            if (existingSwitch != null)
            {
                return null!;
            }
            return await _switchRepository.AddAsync(switchEntity);
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    public async Task<ResponseResult> GetSwitchAsync(int id)
    {
        try
        {
            return await _switchRepository.GetOneAsync(c => c.Id == id);

        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    public async Task<ResponseResult> GetAllSwitchsAsync()
    {
        try
        {
            var SwitchEntities = await _switchRepository.GetAllAsync();

            return SwitchEntities;

        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    public async Task<ResponseResult> UpdateSwitchAsync(SwitchEntity switchEntity)
    {
        try
        {
            var response = await _switchRepository.GetOneAsync(c => c.Id == switchEntity.Id);

            if (response.StatusCode == StatusCode.Ok)
            {
                var existingSwitch = (SwitchEntity)response.ContentResult!;
                existingSwitch.DarkTitle = switchEntity.DarkTitle;
                existingSwitch.LightTitle = switchEntity.LightTitle;
                existingSwitch.DarkImage = switchEntity.DarkImage;
                existingSwitch.LightImage = switchEntity.LightImage;
                return await _switchRepository.UpdateAsync(c => c.Id == switchEntity.Id, existingSwitch);
            }
            else
                return response;
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    public async Task<ResponseResult> DeleteSwitchAsync(int id)
    {
        try
        {
            var existingSwitch = await _switchRepository.GetOneAsync(x => x.Id == id);

            if (existingSwitch != null)
            {
                await _switchRepository.RemoveAsync(c => c.Id == id);
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
