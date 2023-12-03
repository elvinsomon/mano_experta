namespace ManoExperta.Admin.Web.Pages.Professionals.DTOs
{
    public class Professional 
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public int Type { get; set; }
        public string? TypeName => Type == 0 ? "Profesional" : "Usuario";
        public CategoryDto[] Category { get; set; } = Array.Empty<CategoryDto>();
        public string? Categories => string.Join(", ", Category.Select(c => c.Name));
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

        public record WorkingHoursDto
        {
            public int Day { get; init; }
            public int Start { get; init; }
            public int End { get; init; }
        }
    }
}