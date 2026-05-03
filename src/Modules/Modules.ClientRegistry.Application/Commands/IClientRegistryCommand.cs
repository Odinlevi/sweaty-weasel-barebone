using Shared.Application;

namespace Modules.ClientRegistry.Application.Commands;

public interface IClientRegistryCommand<TResponse> : ITransactionCommand<TResponse> where TResponse : notnull
{
}
