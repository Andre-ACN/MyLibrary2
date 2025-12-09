namespace MyLibrary2.Domain.Errors
{
    public class NotFoundException : DomainException
    {
        public NotFoundException(string message) : base(message) { }
    }
}
