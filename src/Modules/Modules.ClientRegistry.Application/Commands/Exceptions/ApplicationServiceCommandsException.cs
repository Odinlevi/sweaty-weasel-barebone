namespace Modules.ClientRegistry.Application.Commands.Exceptions;

public abstract class ApplicationServiceCommandsException : Exception
{
    protected ApplicationServiceCommandsException(string message) : base(message)
    {
    }

    protected ApplicationServiceCommandsException(string message, Exception innerException) :
        base(message: message, innerException: innerException)
    {
    }
}
