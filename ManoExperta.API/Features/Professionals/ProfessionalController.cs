using Microsoft.AspNetCore.Mvc;

namespace ManoExperta.API.Features.Professionals;

[ApiController]
[Route("[controller]/[action]")]
public class ProfessionalController(GetByCategory.Handler getByCategory) : ControllerBase
{


    [HttpGet]
    public async Task<IActionResult> GetByCategory(string categoryCode)
    {
        var query = new GetByCategory.Query
        {
            CategoryCode = categoryCode
        };

        var result = await getByCategory.Handle(query);
        return Ok(result);
    }
}
