namespace Modules.ClientRegistry.Application.Commands.Exceptions;

public class NotFoundEntityException : ApplicationServiceCommandsException
{
    public NotFoundEntityException(string message) : base(message)
    {
    }

    public NotFoundEntityException(string message, Exception innerException) :
        base(message: message, innerException: innerException)
    {
    }
}
