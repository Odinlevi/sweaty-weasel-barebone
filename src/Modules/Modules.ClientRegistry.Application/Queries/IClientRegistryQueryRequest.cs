using Shared.Application;

namespace Modules.ClientRegistry.Application.Queries;

public interface IClientRegistryQueryRequest<TResponse> : IQueryRequest<TResponse> where TResponse : notnull
{
}
