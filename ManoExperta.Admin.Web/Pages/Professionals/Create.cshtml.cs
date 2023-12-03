using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ManoExperta.Admin.Web.Pages.Professionals;

[BindProperties(SupportsGet = true)]
public class CreateModel : PageModel
{
    private readonly ILogger<CreateModel> _logger;
    private readonly IHttpClientFactory _clientFactory;
    private readonly IConfiguration _configuration;
    public CreateModel(ILogger<CreateModel> logger, IHttpClientFactory clientFactory, IConfiguration configuration)
    {
        _logger = logger;
        _clientFactory = clientFactory;
        _configuration = configuration;
    }

    public CreateDto ProfessionalToCreate { get; set; } = new();
    public IActionResult OnGet()
    {
        try
        {
            ProfessionalToCreate = new CreateDto();
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting professionals");
            return RedirectToPage("/Error");
        }
    }

    public async Task<IActionResult> OnPost()
    {
        try
        {
            var client = _clientFactory.CreateClient();
            var baseUrl = _configuration.GetValue<string>("ApiUrl");
            var json = JsonSerializer.Serialize(new CreateProfesionalRequest
            {
                UserName = ProfessionalToCreate.UserName,
                FirstName = ProfessionalToCreate.FirstName,
                LastName = ProfessionalToCreate.LastName,
                IsProfessional = ProfessionalToCreate.IsProfessional,
                PhoneNumbers = ProfessionalToCreate.PhoneNumbers?.Split(",")!,
                Emails = ProfessionalToCreate.Emails?.Split(",")!,
                ProfessionalCategoryCodes = ProfessionalToCreate.ProfessionalCategoryCodes?.Split(",")!,
                WorkingHours = Array.Empty<CreateProfesionalRequest.WorkingHoursDto>()
            });

            var response = await client.PostAsync($"{baseUrl}/User/Create", new StringContent(json, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
                return RedirectToPage("/Error");

            return RedirectToPage("/Professionals/Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting professionals");
            return RedirectToPage("/Error");
        }
    }


    public class CreateDto
    {
        public string? UserName { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public bool IsProfessional { get; init; }
        public string? PhoneNumbers { get; init; }
        public string? Emails { get; init; }
        public string? ProfessionalCategoryCodes { get; init; }

    }


    public class CreateProfesionalRequest
    {
        public string? UserName { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public bool IsProfessional { get; init; }
        public string[] PhoneNumbers { get; init; } = Array.Empty<string>();
        public string[] Emails { get; init; } = Array.Empty<string>();
        public string[] ProfessionalCategoryCodes { get; init; } = Array.Empty<string>();
        public WorkingHoursDto[] WorkingHours { get; init; } = Array.Empty<WorkingHoursDto>();

        public record WorkingHoursDto
        {
            public int Day { get; init; }
            public int Start { get; init; }
            public int End { get; init; }
        }
    }
}