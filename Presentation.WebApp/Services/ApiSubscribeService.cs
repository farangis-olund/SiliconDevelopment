using Infrastructure.Dtos;
using Infrastructure.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Presentation.WebApp.Services;

public class ApiSubscribeService(HttpClient httpClient, IConfiguration configuration)
{
	private readonly HttpClient _http = httpClient;
	private readonly IConfiguration _configuration = configuration;
	
	public async Task<ResponseResult> CreateSubscribeAsync(Subscription model)
	{
		var jsonPayload = JsonConvert.SerializeObject(model);
		var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
		var responseResult = new ResponseResult();

		var response = await _http.PostAsync($"https://localhost:7267/api/subscriber?key={_configuration["ApiKey"]}", content);

		if (response.StatusCode == HttpStatusCode.Created)
		{
			var json = await response.Content.ReadAsStringAsync();
			responseResult = JsonConvert.DeserializeObject<ResponseResult>(json)!;
		}
		else if (response.StatusCode == HttpStatusCode.Conflict)
		{
			responseResult = new ResponseResult
			{
				StatusCode = StatusCode.Exists
			};
		}
		else if (response.StatusCode == HttpStatusCode.Unauthorized)
		{
			responseResult = new ResponseResult
			{
				StatusCode = StatusCode.Unauthorized
			};
		}

		return responseResult;

	}

}
