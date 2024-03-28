
using Infrastructure.Models;

namespace Infrastructure.Entities;

public class ContactEntity
{
	public int Id { get; set; }
	public string FullName { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string Message { get; set; } = null!;
	public int ServiceId { get; set; }
	public ServiceEntity Service { get; set; } = null!;

	public static implicit operator ContactEntity(ContactModel model)
	{
		return new ContactEntity
		{
			FullName = model.FullName,
			Email = model.Email,
			Message = model.Message,
			ServiceId = (int)model.SelectedServiceId!
		};
	}
}
