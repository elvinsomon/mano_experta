using ManoExperta.API.Domain;
using Microsoft.AspNetCore.Mvc;
using static ManoExperta.API.Features.UserFeature.Create;

namespace ManoExperta.API.Features.UserFeature;

[ApiController]
[Route("[controller]/[action]")]
[Produces("application/json")]
[Consumes("application/json")]
public class UserController : ControllerBase
{
    private readonly UserFeature.Create.Handler _createUserHandler;

    public UserController(Create.Handler createUserHandler)
    {
        _createUserHandler = createUserHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserRequest createUserRequest)
    {
        var command = new Command
        {
            UserName = createUserRequest.UserName,
            FirstName = createUserRequest.FirstName,
            LastName = createUserRequest.LastName,
            IsProfessional = createUserRequest.IsProfessional,
            PhoneNumbers = createUserRequest.PhoneNumbers,
            Emails = createUserRequest.Emails,
            ProfessionalCategoryCodes = createUserRequest.ProfessionalCategoryCodes,
            WorkingHours = createUserRequest.WorkingHours.Select(wh => new WorkingHoursDto
            {
                Day = wh.Day,
                Start = wh.Start,
                End = wh.End
            }).ToArray()
        };
       
        var result = await _createUserHandler.Handle(command);

        return Ok(result);
    }

    public class CreateUserRequest
    {
        public string? UserName { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public bool IsProfessional { get; init; }
        public string[] PhoneNumbers { get; init; } = Array.Empty<string>();
        public string[] Emails { get; init; } = Array.Empty<string>();
        public string[] ProfessionalCategoryCodes { get; init; } = Array.Empty<string>();
        public WorkingHoursDto[] WorkingHours { get; init; } = Array.Empty<WorkingHoursDto>();
    }


}