using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.ClientRegistry.Application.Commands.Exceptions;
using Modules.ClientRegistry.Domain.Clients;
using Modules.ClientRegistry.Domain.SeedWork;

namespace Modules.ClientRegistry.Application.Commands.ClientCommands.RemoveClientFounder;

public class CommandHandler(IRepository<Client, ClientId> repository)
    : IRequestHandler<RemoveClientFounderCommand, RemoveClientFounderResult>
{
    public async Task<RemoveClientFounderResult> Handle(RemoveClientFounderCommand request,
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

        client.RemoveFounder(founder.Id);

        return new RemoveClientFounderResult
        {
            ClientId  = client.Id,
            FounderId = founder.Id
        };
    }
}
