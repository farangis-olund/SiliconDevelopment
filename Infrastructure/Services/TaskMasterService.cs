
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Infrastructure.Services;

public class TaskMasterService
{
    private readonly TaskMasterRepository _taskMasterRepository;
    private readonly ILogger<TaskMasterService> _logger;

    public TaskMasterService(TaskMasterRepository taskMasterRepository, ILogger<TaskMasterService> logger)
    {
        _taskMasterRepository = taskMasterRepository;
        _logger = logger;
    }

    public async Task<ResponseResult> AddTaskMasterAsync(TaskMasterEntity taskMasterEntity)
    {
        try
        {
            var existingTaskMaster = await _taskMasterRepository.GetOneAsync(c => c.Id == taskMasterEntity.Id);

            if (existingTaskMaster != null)
            {
                return null!;
            }
            return await _taskMasterRepository.AddAsync(taskMasterEntity);
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    public async Task<ResponseResult> GetTaskMasterAsync(int id)
    {
        try
        {
            return await _taskMasterRepository.GetOneAsync(c => c.Id == id);

        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    public async Task<ResponseResult> GetAllTaskMastersAsync()
    {
        try
        {
            var TaskMasterEntities = await _taskMasterRepository.GetAllAsync();

            return TaskMasterEntities;

        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    public async Task<ResponseResult> UpdateTaskMasterAsync(TaskMasterEntity taskMasterEntity)
    {
        try
        {
            var response = await _taskMasterRepository.GetOneAsync(c => c.Id == taskMasterEntity.Id);

            if (response.StatusCode == StatusCode.Ok)
            {
                var existingTaskMaster = (TaskMasterEntity)response.ContentResult!;
                existingTaskMaster.Title = taskMasterEntity.Title;
                existingTaskMaster.ImageUrl = taskMasterEntity.ImageUrl;
                existingTaskMaster.CheckListItems = taskMasterEntity.CheckListItems;
                return await _taskMasterRepository.UpdateAsync(c => c.Id == taskMasterEntity.Id, existingTaskMaster);
            }
            else
                return response;
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    public async Task<ResponseResult> DeleteTaskMasterAsync(int id)
    {
        try
        {
            var existingTaskMaster = await _taskMasterRepository.GetOneAsync(x => x.Id == id);

            if (existingTaskMaster != null)
            {
                await _taskMasterRepository.RemoveAsync(c => c.Id == id);
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
