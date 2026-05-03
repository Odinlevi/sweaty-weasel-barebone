namespace Modules.ClientRegistry.Application.Queries.ClientQueries.GetClientCollection;

public class GetClientCollectionRequest : IClientRegistryQueryRequest<GetClientCollectionResult>
{
    public string? SearchTerm { get; set; }
    public int     PageIndex  { get; set; }
    public int     PageSize   { get; set; }
}
