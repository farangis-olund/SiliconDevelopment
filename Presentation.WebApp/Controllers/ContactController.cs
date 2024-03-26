using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.ViewModels;

namespace Presentation.WebApp.Controllers;

public class ContactController(ServiceRepository serviceRepository) : Controller
{
    private readonly ServiceRepository _serviceRepository = serviceRepository;

	[HttpGet]
	[Route("/contact")]
	public async Task<IActionResult> Index()
	{
		var viewModel = new ContactViewModel();
		var response = await _serviceRepository.GetAllAsync();

		if (response.StatusCode == Infrastructure.Models.StatusCode.Ok)
		{
			viewModel.Services = (List<ServiceEntity>)response.ContentResult!;
		}
		

		return View(viewModel);
	}
}
