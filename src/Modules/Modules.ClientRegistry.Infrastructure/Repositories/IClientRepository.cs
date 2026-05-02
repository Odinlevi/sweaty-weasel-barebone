using Modules.ClientRegistry.Domain.Clients;
using Modules.ClientRegistry.Domain.SeedWork;

namespace Modules.ClientRegistry.Infrastructure.Repositories;

public interface IClientRepository : IRepository<Client, ClientId>
{
}
