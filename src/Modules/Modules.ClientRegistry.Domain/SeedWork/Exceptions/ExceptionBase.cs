namespace Modules.ClientRegistry.Domain.SeedWork.Exceptions;

public abstract class ExceptionBase : Exception
{
    protected ExceptionBase(string message) : base(message)
    {
    }

    protected ExceptionBase(string message, Exception innerException) : base(
        message: message, innerException: innerException
    )
    {
    }
}
