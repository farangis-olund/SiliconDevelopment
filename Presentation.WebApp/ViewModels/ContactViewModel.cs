using Infrastructure.Entities;
using Infrastructure.Models;

namespace Presentation.WebApp.ViewModels;

public class ContactViewModel
{
	public string Title { get; set; } = "Contact Us";
	public ContactModel Form { get; set; } = new ContactModel();
	public List<ServiceEntity> Services { get; set; } = [];
}
