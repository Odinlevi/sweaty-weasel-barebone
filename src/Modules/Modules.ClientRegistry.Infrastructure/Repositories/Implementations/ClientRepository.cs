using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Modules.ClientRegistry.Domain.Clients;

namespace Modules.ClientRegistry.Infrastructure.Repositories.Implementations;

public class ClientRepository(DbContext dbContext) : IClientRepository
{
    #region Implementation of IClientRepository

    public IQueryable<Client> AsQueryable()
    {
        return dbContext.Set<Client>();
    }

    public async Task<Client?> FindOneAsync(Expression<Func<Client, bool>> predicate)
    {
        return await dbContext.Set<Client>().FirstOrDefaultAsync(predicate);
    }

    public void Update(Client aggregate)
    {
        dbContext.Update(aggregate);
    }

    public void Remove(Client aggregate)
    {
        dbContext.Remove(aggregate);
    }

    public void Add(Client aggregate)
    {
        dbContext.Add(aggregate);
    }

    #endregion
}
