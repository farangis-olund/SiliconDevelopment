
using Infrastructure.Dtos;
using Infrastructure.Models;
using Newtonsoft.Json;
using System.Text;

namespace Presentation.WebApp.Services;

public class ApiSubscribeService (HttpClient httpClient)
{
    private readonly HttpClient _http = httpClient;

    public async Task<ResponseResult> CreateSubscribeAsync(Subscription model)
    {
        var jsonPayload = JsonConvert.SerializeObject(model);
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        var response = await _http.PostAsync("https://localhost:7267/api/subscriber", content);
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ResponseResult>(json)!;
       
    }

}
