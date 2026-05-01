using Modules.ClientRegistry.Domain.SeedWork.Exceptions;

namespace Modules.ClientRegistry.Domain.Exceptions;

public class DomainException : ExceptionBase
{
    public DomainException(string message) : base(message)
    {
    }

    public DomainException(string message, Exception innerException) :
        base(message: message, innerException: innerException)
    {
    }
}
