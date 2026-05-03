using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.ClientRegistry.Domain.Clients;
using Modules.ClientRegistry.Domain.SeedWork;

namespace Modules.ClientRegistry.Application.Queries.ClientQueries;

public class RequestHandler(IRepository<Client, ClientId> clientRepository)
    : IRequestHandler<GetClientCollectionRequest, GetClientCollectionResult>
{
    #region Implementation of IRequestHandler<in GetClientCollectionResult>

    public async Task<GetClientCollectionResult> Handle(
        GetClientCollectionRequest request,
        CancellationToken          cancellationToken)
    {
        var query = clientRepository.AsQueryable().AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var term = request.SearchTerm.ToLower();

            query = query.Where(c => c.Name.ToLower().Contains(term));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(c => c.Name)
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(c => new GetClientCollectionResult.ClientItem
                {
                    ClientId = c.Id,
                    Inn      = c.Inn,
                    Name     = c.Name,
                    Type     = c.Type,

                    TotalFounders = c.Founders.Count()
                }
            )
            .ToListAsync(cancellationToken);

        return new GetClientCollectionResult
        {
            TotalClients = totalCount,
            ClientItems  = items
        };
    }

    #endregion
}
