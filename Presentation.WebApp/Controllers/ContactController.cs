using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.WebApp.ViewModels;
using System.Net;
using System.Text;

namespace Presentation.WebApp.Controllers;

public class ContactController(IConfiguration configuration, HttpClient http, ServiceRepository serviceRepository) : Controller
{
	private readonly IConfiguration _configuration = configuration;
	private readonly HttpClient _http = http;
	private readonly ServiceRepository _serviceRepository = serviceRepository;
	[HttpGet]
	[Route("/contact")]
	public async  Task<IActionResult> Index(string? statusMessage)
	{

		var viewModel = new ContactViewModel();

		if (!string.IsNullOrEmpty(statusMessage))
		{
			ViewData["StatusMessage"] = statusMessage;
		}

		var result = await _serviceRepository.GetAllAsync();
		if(result.StatusCode == Infrastructure.Models.StatusCode.Ok) 
		{
			viewModel.Services = (List<ServiceEntity>)result.ContentResult!;
		}

		return View(viewModel);
	}

	[HttpPost]
	[Route("/contact")]
	public async Task<IActionResult> Create(ContactViewModel model)
	{

		var jsonPayload = JsonConvert.SerializeObject(model.Form);
		var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
		string statusMessage = "";
		var response = await _http.PostAsync($"https://localhost:7267/api/contact?key={_configuration["ApiKey"]}", content);

		if (response.StatusCode == HttpStatusCode.Created)
		{
			statusMessage = "success|We have received your message and will be in touch with you shortly!";
		}
		
		else if (response.StatusCode == HttpStatusCode.Unauthorized)
		{
			statusMessage = "warning|You are unauthorized to send us message!";
		}

		return RedirectToAction("Index", new { statusMessage });
	}
}
