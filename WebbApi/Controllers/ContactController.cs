using Infrastructure.Dtos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly SubscriptionService _subscriptionService = subscriptionService;

    #region Create
    [HttpPost("/api/subscriber")]
    public async Task<IActionResult> Create(Subscription model)
    {
        try
        {
            if (model == null)
            {
                return BadRequest();
            }

            var result = await _subscriptionService.AddSubscriptionAsync(model);

            return result.StatusCode switch
            {
                Infrastructure.Models.StatusCode.Ok => Created("", result.ContentResult),
                Infrastructure.Models.StatusCode.Exists => Conflict("Subscription already exists."),
                _ => BadRequest("An unexpected error occurred."),
            };
        }
        catch (Exception ex)
        {
            return BadRequest("An unexpected error occurred." + ex.Message);
        }

    }
    #endregion

    #region Get
    [HttpGet("/api/subscribers")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var result = await _subscriptionService.GetAllSubscriptionsAsync();

            return result.StatusCode switch
            {
                Infrastructure.Models.StatusCode.Ok => Ok(result.ContentResult),
                Infrastructure.Models.StatusCode.NotFound => NotFound("Not found any subscription."),
                _ => BadRequest("An unexpected error occurred."),
            };
        }
        catch (Exception ex)
        {
            return BadRequest("An unexpected error occurred." + ex.Message);
        }

    }

    [HttpGet("/api/subscriber/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var result = await _subscriptionService.GetSubscriptionAsync(id);

            return result.StatusCode switch
            {
                Infrastructure.Models.StatusCode.Ok => Ok(result.ContentResult),
                Infrastructure.Models.StatusCode.NotFound => NotFound("Not found any subscription."),
                _ => BadRequest("An unexpected error occurred."),
            };
        }
        catch (Exception ex)
        {
            return BadRequest("An unexpected error occurred." + ex.Message);
        }

    }
    #endregion

    #region Update
    [HttpPut("/api/subscriber/{id}")]
    public async Task<IActionResult> UpdateOne(int id, Subscription model)
    {
        try
        {
            if (model == null)
            {
                return BadRequest();
            }

            var result = await _subscriptionService.UpdateSubscriptionAsync(id, model);

            return result.StatusCode switch
            {
                Infrastructure.Models.StatusCode.Ok => Ok("Subscription updated successfully!"),
                Infrastructure.Models.StatusCode.NotFound => NotFound("Subscription does not exist."),
                _ => BadRequest("An unexpected error occurred."),
            };
        }
        catch (Exception ex)
        {
            return BadRequest("An unexpected error occurred." + ex.Message);
        }
    }
    #endregion

    #region Delete
    [HttpDelete("/api/subscriber/{email}")]
    public async Task<IActionResult> DeleteOne(string email)
    {
        try
        {
            if (email == null)
            {
                return BadRequest();
            }

            var result = await _subscriptionService.DeleteSubscriptionAsync(email);

            return result.StatusCode switch
            {
                Infrastructure.Models.StatusCode.Ok => Ok("Subscription deleted successfully!"),
                Infrastructure.Models.StatusCode.NotFound => NotFound("Subscription does not exist."),
                _ => BadRequest("An unexpected error occurred."),
            };
        }
        catch (Exception ex)
        {
            return BadRequest("An unexpected error occurred." + ex.Message);
        }
    }
    #endregion
}
