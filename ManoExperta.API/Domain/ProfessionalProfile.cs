namespace ManoExperta.API.Domain;

public class ProfessionalProfile
{
    public ProfessionalProfile() { }

    public ProfessionalProfile(
        User user, IEnumerable<ProfessionalCategory> categories, 
        IEnumerable<WorkingHours> workingHours, IEnumerable<PhoneNumber> phoneNumber, 
        IEnumerable<Email> email)
    {
        ArgumentNullException.ThrowIfNull(user);

        if(user.Type is not UserType.Professional)
            throw new ArgumentException("User must be a professional", nameof(user));

        User = user;
        Categories = categories;
        WorkingHours = workingHours;
        PhoneNumber = phoneNumber;
        Email = email;        
    }

    public Guid Id { get; set; }
    public User? User { get; private set; }
    public IEnumerable<ProfessionalCategory>? Categories { get; set; }
    public IEnumerable<WorkingHours>? WorkingHours { get; set; }    
    public IEnumerable<PhoneNumber>? PhoneNumber { get; set; }
    public IEnumerable<Email>? Email { get; set; }
}