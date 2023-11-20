using System.ComponentModel.DataAnnotations.Schema;

namespace ManoExperta.API.Domain;

public class ContactInformation
{
    public Guid Id { get; set; }
}

public class Email
{
    public Guid Id { get; set; }
    public string? Address { get; set; }
    public bool IsPrimary { get; set; }
}

public class PhoneNumber
{
    public Guid Id { get; set; }
    [Column(TypeName = "nvarchar(100)")]
    public PhoneNumberType Type { get; set; }
    public string? Number { get; set; }
    public bool IsPrimary { get; set; }
}

// TODO: Verificar si es necesario colocar los tipos de formas de contancto una entidad aparte
public enum PhoneNumberType
{
    Home,
    Work,
    Mobile,
    WhatsApp,
    Other
}