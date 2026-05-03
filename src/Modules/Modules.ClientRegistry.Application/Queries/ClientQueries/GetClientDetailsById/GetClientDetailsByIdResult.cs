using Modules.ClientRegistry.Domain.Clients;
using Modules.ClientRegistry.Domain.ClientTypes;
using Modules.ClientRegistry.Domain.Inns;

namespace Modules.ClientRegistry.Application.Queries.ClientQueries.GetClientDetailsById;

public class GetClientDetailsByIdResult
{
    public ClientId   ClientId      { get; set; }
    public Inn        Inn           { get; set; }
    public string     Name          { get; set; }
    public ClientType Type          { get; set; }
    public int        TotalFounders { get; set; }

    public IEnumerable<ClientFounderResult> ClientFounderResults { get; set; }

    public class ClientFounderResult
    {
        public FounderId FounderId { get; set; }
        public Inn       Inn       { get; set; }
        public string    FullName  { get; set; }
    }
}
