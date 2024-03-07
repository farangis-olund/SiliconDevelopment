
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class DownloadAppService
{
    private readonly DownloadAppRepository _downloadAppRepository;
   
    public DownloadAppService(DownloadAppRepository downloadAppRepository)
    {
        _downloadAppRepository = downloadAppRepository;
      
    }

    public async Task<ResponseResult> AddDownloadAppAsync(DownloadAppEntity downloadApp)
    {
        try
        {
            var existingDownloadApp = await _downloadAppRepository.GetOneAsync(c => c.Id == downloadApp.Id);

            if (existingDownloadApp != null)
            {
                return null!;
            }
            var newDownloadApp = new DownloadAppEntity
            {
                Title = downloadApp.Title,
                Image =downloadApp.Image
                
            };

            return await _downloadAppRepository.AddAsync(newDownloadApp);
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    public async Task<ResponseResult> GetDownloadAppAsync(int id)
    {
        try
        {
            return await _downloadAppRepository.GetOneAsync(c => c.Id == id);

        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    public async Task<ResponseResult> GetAllDownloadAppsAsync()
    {
        try
        {
            var DownloadAppEntities = await _downloadAppRepository.GetAllAsync();

            if (DownloadAppEntities != null)
                return DownloadAppEntities;
            return ResponseFactory.NotFound();
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    public async Task<ResponseResult> UpdateDownloadAppAsync(DownloadAppEntity downloadApp)
    {
        try
        {
            var response = await _downloadAppRepository.GetOneAsync(c => c.Id == downloadApp.Id);

            if (response.StatusCode == StatusCode.Ok)
            {
                var existingDownloadApp = (DownloadAppEntity)response.ContentResult!;
                existingDownloadApp.Title = downloadApp.Title;
                existingDownloadApp.Image = downloadApp.Image;

                var updateResponse = await _downloadAppRepository.UpdateAsync(c => c.Id == downloadApp.Id, existingDownloadApp);

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

    public async Task<ResponseResult> DeleteDownloadAppAsync(int id)
    {
        try
        {
            var existingDownloadApp = await _downloadAppRepository.GetOneAsync(x => x.Id == id);

            if (existingDownloadApp != null)
            {
                await _downloadAppRepository.RemoveAsync(c => c.Id == id);
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
