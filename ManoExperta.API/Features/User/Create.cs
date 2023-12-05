using ManoExperta.API.Data;
using ManoExperta.API.Domain;
using ManoExperta.API.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ManoExperta.API.Features.UserFeature
{
    public class Create
    {
        public record Command
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

        public record WorkingHoursDto
        {
            public int Day { get; init; }
            public string DayName => Day == 7 ? "Sunday" : ((DayOfWeek)Day).ToString();

            public int Start { get; init; }
            public int End { get; init; }
        }

        public record Result
        {
            public Guid Id { get; init; }
            public string? UserName { get; init; }
            public string? FirstName { get; init; }
            public string? LastName { get; init; }
            public bool IsProfessional { get; init; }
        }

        public class Handler(ApplicationDbContext context)
        {
            public async Task<Result> Handle(Command command)
            {

                var user = new User
                {
                    UserName = command.UserName,
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    Type = command.IsProfessional ? UserType.Professional : UserType.Client
                };

                var userWithSameUserName = await context.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName);
                if (userWithSameUserName is not null)
                    throw new UserAlreadyExistsException("User with same user name already exists");

                context.Users.Add(user);

                if (command.IsProfessional)
                {
                    var phoneNumber = GetPhoneNumbers(command);
                    var email = GetEmails(command);
                    var professionalCategory = await GetCategories(context, command);
                    var workingHours = GetWorkingHours(command);

                    var professionalProfile = new ProfessionalProfile(user, professionalCategory, workingHours, phoneNumber, email);
                    context.ProfessionalProfiles.Add(professionalProfile);
                }

                await context.SaveChangesAsync();

                return new Result
                {
                    Id = Guid.NewGuid(),
                    UserName = command.UserName,
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    IsProfessional = command.IsProfessional
                };
            }

            private static List<WorkingHours> GetWorkingHours(Command command)
            {
                if (command.WorkingHours.Length == 0)
                {
                    return
                    [
                        new WorkingHours(1, 8, 17),
                        new WorkingHours(2, 8, 17),
                        new WorkingHours(3, 8, 17),
                        new WorkingHours(4, 8, 17),
                        new WorkingHours(5, 8, 17)
                    ];
                }
                
                var workingHours = new List<WorkingHours>();

                foreach (var workingHoursDto in command.WorkingHours)
                {
                    workingHours.Add(new WorkingHours(workingHoursDto.Day, workingHoursDto.Start, workingHoursDto.End));
                }

                return workingHours;
            }

            private static List<PhoneNumber> GetPhoneNumbers(Command command)
            {
                var phoneNumber = new List<PhoneNumber>();

                for (int i = 0; i < command.PhoneNumbers.Length; i++)
                {
                    phoneNumber.Add(new PhoneNumber
                    {
                        Number = command.PhoneNumbers[i],
                        Type = PhoneNumberType.Mobile,
                        IsPrimary = i == 0
                    });
                }

                return phoneNumber;
            }

            private static List<Email> GetEmails(Command command)
            {
                var email = new List<Email>();

                for (int i = 0; i < command.Emails.Length; i++)
                {
                    email.Add(new Email
                    {
                        Address = command.Emails[i],
                        IsPrimary = i == 0
                    });
                }

                return email;
            }

            private static async Task<List<Domain.ProfessionalCategory>> GetCategories(ApplicationDbContext context, Command command)
            {
                var professionalCategory = new List<Domain.ProfessionalCategory>();

                foreach (var code in command.ProfessionalCategoryCodes)
                {
                    var category = await context.ProfessionalCategories.FirstOrDefaultAsync(c => c.Code == code)
                        ?? throw new Exception($"Category with code {code} does not exist");

                    professionalCategory.Add(category);
                }

                return professionalCategory;
            }
        }
    }
}