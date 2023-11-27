namespace ManoExperta.API.Domain.Exceptions
{
    public class UserAlreadyExistsException(string message) : DomainException
    {
        public string Message {get; set;}
    }
}