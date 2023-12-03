using System.Text.Json;
using ManoExperta.Admin.Web.Data;
using ManoExperta.Admin.Web.Pages.Professionals.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ManoExperta.Admin.Web.Pages.Professionals;

[BindProperties(SupportsGet = true)]
public class IndexModel : PageModel
{
    private readonly IBaseApiClient _baseApiClient;
    private readonly ILogger<IndexModel> _logger;
    public IEnumerable<Professional>? Professionals { get; set; }
    public List<CategoryDto>? Categories { get; set; }
    public string CategorySelected { get; set; } = "PLMR";

    public IndexModel(ILogger<IndexModel> logger, IBaseApiClient baseApiClient)
    {
        _logger = logger;
        _baseApiClient = baseApiClient;
    }

    public async Task<IActionResult> OnGet()
    {
        try
        {
            Categories = (await GetCategories())?.ToList();
            Professionals = await GetProfessionals();
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting professionals");
            return RedirectToPage("/Error");
        }
    }

    private async Task<IEnumerable<CategoryDto>?> GetCategories()
    {
        var categories = await _baseApiClient.GetAsync<IEnumerable<CategoryDto>>("Category/GetAll");
        return categories;
    }

    private async Task<IEnumerable<Professional>?> GetProfessionals()
    {
        var professionals = await _baseApiClient
            .GetAsync<IEnumerable<Professional>>($"Professional/GetByCategory?categoryCode={CategorySelected}");

        return professionals;
    }

    public record CategoryDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
}