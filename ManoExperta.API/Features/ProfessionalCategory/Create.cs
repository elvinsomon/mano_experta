using ManoExperta.API.Data;
using ManoExperta.API.Domain;

namespace ManoExperta.API.Features.Category;

public class Create
{
    public class Request
    {
        public string? Code { get; init; }
        public string? Name { get; init; }
        public string? Description { get; init; }
    }

    public class Result
    {
        public Guid Id { get; init; }
    }

    public class Handler(ApplicationDbContext context)
    {
        public async Task<Result> Handle(Request request)
        {
            var category = new ProfessionalCategory
            {
                Code = request.Code,
                Name = request.Name,
                Description = request.Description
            };

            context.ProfessionalCategories.Add(category);

            await context.SaveChangesAsync();

            return new Result
            {
                Id = category.Id
            };
        }
    }
}