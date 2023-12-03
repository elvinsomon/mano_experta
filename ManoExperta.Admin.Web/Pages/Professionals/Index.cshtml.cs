using System.Text.Json;
using ManoExperta.Admin.Web.Pages.Professionals.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ManoExperta.Admin.Web.Pages.Professionals;

[BindProperties(SupportsGet = true)]
public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IConfiguration _configuration;
    private readonly ILogger<IndexModel> _logger;
    public IEnumerable<Professional>? Professionals { get; set; }
    public string CategorySelected { get; set; } = "PLMR";

    public IndexModel(IHttpClientFactory clientFactory, IConfiguration configuration, ILogger<IndexModel> logger)
    {
        _logger = logger;
        _clientFactory = clientFactory;
        _configuration = configuration;
    }

    public async Task<IActionResult> OnGet()
    {
        try
        {
            Professionals = await GetProfessionals();
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting professionals");
            return RedirectToPage("/Error");
        }
    }

    private async Task<IEnumerable<Professional>?> GetProfessionals()
    {
        var client = _clientFactory.CreateClient();
        var baseUrl = _configuration.GetValue<string>("ApiUrl");
        var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/Professional/GetByCategory?categoryCode={CategorySelected}");
        var response = await client.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            return null;

        var content = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var professionals = JsonSerializer.Deserialize<IEnumerable<Professional>>(content, options);
        return professionals;
    }
}