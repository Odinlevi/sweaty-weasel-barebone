using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Modules.ClientRegistry.Domain.SeedWork;
using Modules.ClientRegistry.Domain.SeedWork.Models;

namespace Modules.ClientRegistry.Infrastructure.Repositories;

public class Repository<TAggregate, TIdentity>(DbContext dbContext) : IRepository<TAggregate, TIdentity>
    where TAggregate : AggregateRoot<TIdentity>
    where TIdentity : IdentityBase
{
    #region Implementation of IRepository<TAggregate>

    public IQueryable<TAggregate> AsQueryable()
    {
        return dbContext.Set<TAggregate>();
    }

    public async Task<TAggregate?> FindOneAsync(Expression<Func<TAggregate, bool>> predicate)
    {
        return await dbContext.Set<TAggregate>().FirstOrDefaultAsync(predicate);
    }

    public void Update(TAggregate aggregate)
    {
        dbContext.Update(aggregate);
    }

    public void Remove(TAggregate aggregate)
    {
        dbContext.Remove(aggregate);
    }

    public void Add(TAggregate aggregate)
    {
        dbContext.Add(aggregate);
    }

    #endregion
}
