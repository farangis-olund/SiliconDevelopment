
namespace Infrastructure.Entities;

public class ContactEntity
{
	public int Id { get; set; }
	public string FullName { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string Message { get; set; } = null!;
	public int ServiceId { get; set; }
	public ServiceEntity Service { get; set; } = null!;

}
