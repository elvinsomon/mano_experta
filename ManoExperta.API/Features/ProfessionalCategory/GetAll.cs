using ManoExperta.API.Data;
using Microsoft.EntityFrameworkCore;

namespace ManoExperta.API.Features.Category;

public class GetAll
{
    public class Handler(ApplicationDbContext context){
        
        public async Task<IEnumerable<Domain.ProfessionalCategory>> Handle()
        {
            var result = await context.ProfessionalCategories.ToListAsync();
            return result;
        }
    }
}