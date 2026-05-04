using MediatR;
using Modules.ClientRegistry.Application.Commands.Exceptions;
using Modules.ClientRegistry.Domain.Clients;
using Modules.ClientRegistry.Domain.SeedWork;

namespace Modules.ClientRegistry.Application.Commands.ClientCommands.UpdateClient;

public class CommandHandler(IRepository<Client, ClientId> repository)
    : IRequestHandler<UpdateClientCommand, UpdateClientResult>
{
    public async Task<UpdateClientResult> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var client = await repository.FindOneAsync(x => x.Id == request.ClientId);

        if (client is null)
            throw new NotFoundEntityException($"Client with ID {request.ClientId} not found.");

        client.UpdateName(request.ClientName);

        return new UpdateClientResult
        {
            ClientId = client.Id,
            Success  = true
        };
    }
}
