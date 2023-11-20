using System.ComponentModel.DataAnnotations.Schema;

namespace ManoExperta.API.Domain;

public class User
{
    public Guid Id { get; set; }
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    [Column(TypeName = "nvarchar(100)")]
    public UserType Type { get; set; }
}

public enum UserType
{
    Professional,
    Client
}