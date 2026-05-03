using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.ClientRegistry.Domain.Clients;
using Modules.ClientRegistry.Domain.SeedWork;

namespace Modules.ClientRegistry.Application.Queries.ClientQueries.GetClientDetailsById;

public class RequestHandler(IRepository<Client, ClientId> clientRepository)
    : IRequestHandler<GetClientDetailsByIdRequest, GetClientDetailsByIdResult>
{
    #region IRequestHandler<GetClientDetailsByIdRequest, GetClientDetailsByIdResult>

    public async Task<GetClientDetailsByIdResult> Handle(
        GetClientDetailsByIdRequest request,
        CancellationToken           cancellationToken)
    {
        var result = await clientRepository.AsQueryable()
            .AsNoTracking()
            .Where(c => c.Id == request.ClientId)
            .Select(c => new GetClientDetailsByIdResult
                {
                    ClientId      = c.Id,
                    Name          = c.Name,
                    Inn           = c.Inn,
                    Type          = c.Type,
                    TotalFounders = c.Founders.Count(),
                    ClientFounderResults = c.Founders.Select(f => new GetClientDetailsByIdResult.ClientFounderResult
                        {
                            FounderId = f.Id,
                            FullName  = f.FullName,
                            Inn       = f.Inn
                        }
                    ).ToList()
                }
            )
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }

    #endregion
}
