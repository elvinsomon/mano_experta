using System.Text.Json;

namespace ManoExperta.Admin.Web.Data;

public interface IBaseApiClient
{
    Task<T> GetAsync<T>(string url);
}

public class BaseApiClient : IBaseApiClient
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IConfiguration _configuration;

    public BaseApiClient(IHttpClientFactory clientFactory, IConfiguration configuration)
    {
        _clientFactory = clientFactory;
        _configuration = configuration;
    }

    public async Task<T> GetAsync<T>(string url)
    {
        var client = _clientFactory.CreateClient();
        var baseUrl = _configuration.GetValue<string>("ApiUrl");
        var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/{url}");
        var response = await client.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            return default!;

        var content = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var result = JsonSerializer.Deserialize<T>(content, options);
        return result!;
    }
}