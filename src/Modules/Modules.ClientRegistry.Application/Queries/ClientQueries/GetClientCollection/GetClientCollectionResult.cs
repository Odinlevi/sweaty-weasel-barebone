using Modules.ClientRegistry.Domain.Clients;
using Modules.ClientRegistry.Domain.ClientTypes;
using Modules.ClientRegistry.Domain.Inns;

namespace Modules.ClientRegistry.Application.Queries.ClientQueries.GetClientCollection;

public class GetClientCollectionResult
{
    public int                     TotalClients { get; set; }
    public IEnumerable<ClientItem> ClientItems  { get; set; }

    public class ClientItem
    {
        public ClientId   ClientId      { get; set; }
        public Inn        Inn           { get; set; }
        public string     Name          { get; set; }
        public ClientType Type          { get; set; }
        public int        TotalFounders { get; set; }
    }
}
