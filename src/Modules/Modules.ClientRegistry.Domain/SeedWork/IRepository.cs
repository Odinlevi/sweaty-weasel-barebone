using System.Linq.Expressions;
using Modules.ClientRegistry.Domain.SeedWork.Models;

namespace Modules.ClientRegistry.Domain.SeedWork;

public interface IRepository<TAggregate, TIdentity>
    where TAggregate : AggregateRoot<TIdentity>
    where TIdentity : IdentityBase
{
    IQueryable<TAggregate> AsQueryable();

    Task<TAggregate?> FindOneAsync(Expression<Func<TAggregate, bool>> predicate);

    void Update(TAggregate aggregate);
    void Remove(TAggregate aggregate);
    void Add(TAggregate    aggregate);
}
