
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class CategoryService
{
	private readonly CategoryRepository _categoryRepository;

	public CategoryService(CategoryRepository categoryRepository)
	{
		_categoryRepository = categoryRepository;

	}

	public async Task<ResponseResult> AddCategoryAsync(CategoryEntity category)
	{
		try
		{
			var existingcategory = await _categoryRepository.GetOneAsync(c => c.Id == category.Id);

			if (existingcategory != null)
			{
				return null!;
			}
			var newcategory = new CategoryEntity
			{
				Name = category.Name
			};

			return await _categoryRepository.AddAsync(newcategory);
		}
		catch (Exception ex)
		{
			return ResponseFactory.Error(ex.Message);
		}
	}

	public async Task<ResponseResult> GetCategoryAsync(int id)
	{
		try
		{
			return await _categoryRepository.GetOneAsync(c => c.Id == id);

		}
		catch (Exception ex)
		{
			return ResponseFactory.Error(ex.Message);
		}
	}

	public async Task<ResponseResult> GetAllCategoriesAsync()
	{
		try
		{
			var categoryEntities = await _categoryRepository.GetAllAsync();

			if (categoryEntities != null)
				return categoryEntities;
			return ResponseFactory.NotFound();
		}
		catch (Exception ex)
		{
			return ResponseFactory.Error(ex.Message);
		}
	}

	public async Task<ResponseResult> UpdatecategoryAsync(CategoryEntity category)
	{
		try
		{
			var response = await _categoryRepository.GetOneAsync(c => c.Id == category.Id);

			if (response.StatusCode == StatusCode.Ok)
			{
				var existingcategory = (CategoryEntity)response.ContentResult!;
				existingcategory.Name = category.Name;
				
				var updateResponse = await _categoryRepository.UpdateAsync(c => c.Id == category.Id, existingcategory);

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

	public async Task<ResponseResult> DeleteCategoryAsync(int id)
	{
		try
		{
			var existingcategory = await _categoryRepository.GetOneAsync(x => x.Id == id);

			if (existingcategory != null)
			{
				await _categoryRepository.RemoveAsync(c => c.Id == id);
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
