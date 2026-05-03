using Modules.ClientRegistry.Domain.Clients;

namespace Modules.ClientRegistry.Application.Queries.ClientQueries.GetClientDetailsById;

public class GetClientDetailsByIdRequest : IClientRegistryQueryRequest<GetClientDetailsByIdResult>
{
    public ClientId ClientId { get; set; }
}
