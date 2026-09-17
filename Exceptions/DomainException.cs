namespace GData.Exceptions
{
    public abstract class DomainException : Exception
    {
        protected DomainException(string message) : base(message) { }
    }

    public class NotFoundException : DomainException
    {
        public NotFoundException(string message) : base(message) { }
    }

    public class BadRequestException : DomainException
    {
        public BadRequestException(string message) : base(message) { }
    }

    public class ForbiddenException : DomainException
    {
        public ForbiddenException(string message) : base(message) { }
    }
}
