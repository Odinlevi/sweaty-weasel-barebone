using Modules.ClientRegistry.Application.Commands.Exceptions;
using Modules.ClientRegistry.Domain.Clients;
using Modules.ClientRegistry.Domain.SeedWork;

namespace Modules.ClientRegistry.Application.Commands.ClientCommands.DeleteClient;

public class CommandHandler(IRepository<Client, ClientId> clientRepository)
{
    public async Task<DeleteClientResult> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        var client = await clientRepository.FindOneAsync(x => x.Id == request.ClientId);
        if (client is null)
            throw new NotFoundEntityException($"Client with ID {request.ClientId} not found.");

        clientRepository.Remove(client);

        return new DeleteClientResult
        {
            ClientId = request.ClientId
        };
    }
}
