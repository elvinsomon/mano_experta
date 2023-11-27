using Microsoft.AspNetCore.Mvc;

namespace ManoExperta.API.Features.Category;


[ApiController]
[Route("[controller]/[action]")]
[Produces("application/json")]
[Consumes("application/json")]
public class CategoryController(
    Create.Handler create,
    GetAll.Handler getAll) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryRequest createCategoryRequest)
    {
        var command = new Create.Request
        {
            Code = createCategoryRequest.Code,
            Name = createCategoryRequest.Name,
            Description = createCategoryRequest.Description
        };

        var result = await create.Handle(command);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await getAll.Handle();
        return Ok(result);
    }
}

public class CreateCategoryRequest
{
    public string? Code { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
}