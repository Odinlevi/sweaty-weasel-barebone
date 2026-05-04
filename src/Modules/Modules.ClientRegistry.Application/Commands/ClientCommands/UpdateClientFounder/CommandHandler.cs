using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.ClientRegistry.Application.Commands.Exceptions;
using Modules.ClientRegistry.Domain.Clients;
using Modules.ClientRegistry.Domain.SeedWork;

namespace Modules.ClientRegistry.Application.Commands.ClientCommands.UpdateClientFounder;

public class CommandHandler(IRepository<Client, ClientId> repository)
    : IRequestHandler<UpdateClientFounderCommand, UpdateClientFounderResult>
{
    public async Task<UpdateClientFounderResult> Handle(UpdateClientFounderCommand request,
                                                        CancellationToken          cancellationToken)
    {
        var clients = repository.AsQueryable();

        var query =
            from c in clients
            from f in c.Founders.Where(f1 => f1.Id == request.FounderId)
            where c.Id == request.ClientId
            select new { Client = c, Founder = f };

        var result = await query.FirstOrDefaultAsync(cancellationToken);

        if (result is null)
            throw new NotFoundEntityException(
                $"Client with id {request.ClientId} and founder with id {request.FounderId} not found."
            );

        var client  = result.Client;
        var founder = result.Founder;

        founder.UpdateFullName(request.FounderFullName);

        return new UpdateClientFounderResult
        {
            ClientId  = client.Id,
            FounderId = founder.Id,
            Success   = true
        };
    }
}
