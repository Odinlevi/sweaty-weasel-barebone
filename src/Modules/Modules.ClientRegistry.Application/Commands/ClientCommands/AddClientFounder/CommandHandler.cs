using MediatR;
using Modules.ClientRegistry.Domain.Clients;
using Modules.ClientRegistry.Domain.ClientTypes;
using Modules.ClientRegistry.Domain.Inns;
using Modules.ClientRegistry.Domain.SeedWork;

namespace Modules.ClientRegistry.Application.Commands.ClientCommands.AddClientFounder;

public class CommandHandler(IRepository<Client, ClientId> repository)
    : IRequestHandler<AddClientFounderCommand, AddClientFounderResult>
{
    public async Task<AddClientFounderResult> Handle(AddClientFounderCommand request,
                                                     CancellationToken       cancellationToken)
    {
        var client = await repository.FindOneAsync(x => x.Id == request.ClientId);

        if (client is null)
            throw new InvalidOperationException($"Client with id {request.ClientId} not found.");

        var founder = client.AddFounder(
            // actually, there can be both LE and IE founders, but the task kind of mentions only IE, so i roll with it.
            founderInn: Inn.Create(value: request.FounderInn, clientType: ClientType.IndividualEntrepreneur),
            fullName: request.FounderFullName
        );

        return new AddClientFounderResult
        {
            ClientId  = client.Id,
            FounderId = founder.Id
        };
    }
}
