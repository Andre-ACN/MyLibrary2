namespace MyLibrary2.Domain.Errors
{
    public class ValidationException : DomainException
    {
        public ValidationException(string message) : base(message) { }
    }
}
