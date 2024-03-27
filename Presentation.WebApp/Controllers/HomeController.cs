using Infrastructure.Dtos;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.Services;

namespace Presentation.WebApp.Controllers;

public class HomeController(ApiSubscribeService apiSubscribeService) : Controller
{
    private readonly ApiSubscribeService _apiSubscribeService = apiSubscribeService;

    [Route("/")]
    public IActionResult Index(string? statusMessage)
    {
		ViewData["Title"] = "Task Management Asisstent";
        
        if (!string.IsNullOrEmpty(statusMessage))
        {
            ViewData["StatusMessage"] = statusMessage;
        }

        return View();
    }

	[Route("/error")]
    public IActionResult Error404(int statusCode) => View();

	[HttpPost]
	public async Task<IActionResult> Subscribe(IFormCollection form)
	{
        string email = form["email"]!;
        List<string> checkboxValues = form["checkboxValues"].ToList()!;
        
        var subscrier = new Subscription
        {
            Email = email,
            DailyNewsletter = checkboxValues.Contains("Daily Newsletter"),
            EventUpdates = checkboxValues.Contains("Event Updates"),
            AdvertisingUpdates = checkboxValues.Contains("Advertising Updates"),
            StartupsWeekly = checkboxValues.Contains("Startups Weekly"),
            WeekInReview = checkboxValues.Contains("Week in Review"),
            Podcasts = checkboxValues.Contains("Podcasts")
        };
       
        var response = await _apiSubscribeService.CreateSubscribeAsync(subscrier);
        
        var statusMessage = response.StatusCode switch
        {
            Infrastructure.Models.StatusCode.Ok => "success|You are successfully subscribed!",
            Infrastructure.Models.StatusCode.Exists => "warning|You are already subscribed!",
			Infrastructure.Models.StatusCode.Unauthorized => "You are unauthorized to subscribe!",
			_ => "danger|Unknown error occurred!"
        };
        
        return RedirectToAction("Index", new { statusMessage, scrollToSubscription = true });
    }
}
