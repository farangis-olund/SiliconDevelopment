
namespace Infrastructure.Entities;

public class ServiceEntity
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public ICollection<ContactEntity> Contacts { get; set; } = [];
}
