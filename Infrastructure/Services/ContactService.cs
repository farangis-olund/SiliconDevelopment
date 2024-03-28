using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class ContactService(ContactRepository contactRepository, ServiceRepository serviceRepository)
{
	private readonly ContactRepository _contactRepository = contactRepository;
	private readonly ServiceRepository _serviceRepository = serviceRepository;
	public async Task<ResponseResult> AddContactAsync(ContactModel contact)
	{
		try
		{
			var serviceEntity = await _serviceRepository.GetOneAsync(c => c.Id == contact.SelectedServiceId);
			var serviceId= serviceEntity.StatusCode == StatusCode.Ok
				? ((ServiceEntity)serviceEntity.ContentResult!).Id
				: ((ServiceEntity)(await _serviceRepository.AddAsync(new ServiceEntity
				{
					Name = contact.ServiceName!

				})).ContentResult!).Id;

			var newContactEntity = new ContactEntity
			{
				FullName = contact.FullName,
				Email = contact.Email,
				Message = contact.Message,
				ServiceId = serviceId
			};

			var result = await _contactRepository.AddAsync(newContactEntity);

			return ResponseFactory.Ok(result);
		}
		catch (Exception ex)
		{
			return ResponseFactory.Error(ex.Message);
		}
	}

	public async Task<ResponseResult> GetContactAsync(int id)
	{
		try
		{
			var result = await _contactRepository.GetOneAsync(c => c.Id == id);
			if (result.StatusCode == StatusCode.Ok)
				return ResponseFactory.Ok(result.ContentResult!);
			return ResponseFactory.NotFound();
		}
		catch (Exception ex)
		{
			return ResponseFactory.Error(ex.Message);
		}
	}

	public async Task<ResponseResult> GetAllContactsAsync()
	{
		try
		{
			var contactEntities = await _contactRepository.GetAllAsync();

			if (contactEntities.StatusCode == StatusCode.Ok)
				return ResponseFactory.Ok(contactEntities.ContentResult!);
			return ResponseFactory.NotFound();
		}
		catch (Exception ex)
		{
			return ResponseFactory.Error(ex.Message);
		}
	}

	
	public async Task<ResponseResult> DeleteContactAsync(int id)
	{
		try
		{
			var existingcontact = await _contactRepository.GetOneAsync(x => x.Id == id);

			if (existingcontact.StatusCode == StatusCode.Ok)
			{
				await _contactRepository.RemoveAsync(c => c.Id == id);
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
