using ManoExperta.API.Data;
using ManoExperta.API.Domain;
using Microsoft.EntityFrameworkCore;
using static ManoExperta.API.Features.UserFeature.Create;

namespace ManoExperta.API.Features.Professionals;

public class GetByCategory
{
    public class Query
    {
        public string CategoryCode { get; set; } = string.Empty;
    }

    public class Result 
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public UserType Type { get; set; }
        public CategoryDto[] Category { get; set; } = Array.Empty<CategoryDto>();
        public PhoneNumberDto[] PhoneNumbers { get; set; } = Array.Empty<PhoneNumberDto>();
        public EmailDto[] Emails { get; set; } = Array.Empty<EmailDto>();
        public WorkingHoursDto[] WorkingHours { get; set; } = Array.Empty<WorkingHoursDto>();

        public class CategoryDto
        {
            public string? Code { get; set; }
            public string? Name { get; set; }
            public string? Description { get; set; }
        }
        public class PhoneNumberDto
        {
            public string? Number { get; set; }
        }

        public class EmailDto
        {
            public string? Address { get; set; }
        }
    }

    public class Handler(ApplicationDbContext context)
    {
        public async Task<IEnumerable<Result>> Handle(Query query)
        {
            
            var professionals = await context.ProfessionalCategories
                .Where(c => c.Code == query.CategoryCode)
                .SelectMany(c => c.Professionals!)
                .Include(p => p.User)
                .Include(p => p.Categories)
                .Include(p => p.PhoneNumber)
                .Include(p => p.Email)
                .Include(p => p.WorkingHours)
                .ToListAsync();


            return professionals.Select(p => new Result
            {
                Id = p.Id,
                UserName = p.User!.UserName,
                FirstName = p.User.FirstName,
                LastName = p.User.LastName,
                Type = p.User.Type,
                Category = p.Categories!.Select(c => new Result.CategoryDto
                {
                    Code = c.Code,
                    Name = c.Name,
                    Description = c.Description
                }).ToArray(),
                PhoneNumbers = p.PhoneNumber!.Select(pn => new Result.PhoneNumberDto
                {
                    Number = pn.Number
                }).ToArray(),
                Emails = p.Email!.Select(e => new Result.EmailDto
                {
                    Address = e.Address
                }).ToArray(),
                WorkingHours = p.WorkingHours!.Select(wh => new WorkingHoursDto
                {
                    Day = wh.Day,
                    Start = wh.Start,
                    End = wh.End
                }).ToArray()
            });
        }
    }

}