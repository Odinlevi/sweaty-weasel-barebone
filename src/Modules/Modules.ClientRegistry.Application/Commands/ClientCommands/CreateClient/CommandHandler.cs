using MediatR;
using Modules.ClientRegistry.Domain.Clients;
using Modules.ClientRegistry.Domain.ClientTypes;
using Modules.ClientRegistry.Domain.Inns;
using Modules.ClientRegistry.Domain.SeedWork;

namespace Modules.ClientRegistry.Application.Commands.ClientCommands.CreateClient;

public class CommandHandler(IRepository<Client, ClientId> repository)
    : IRequestHandler<CreateClientCommand, CreateClientResult>
{
    public async Task<CreateClientResult> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var inn = Inn.Create(value: request.Inn, clientType: request.Type);

        var client = Client.Create(
            inn: inn,
            name: request.Name,
            type: request.Type
        );

        foreach (var founder in request.Founders)
            client.AddFounder(
                // actually, there can be both LE and IE founders, but the task kind of mentions only IE, so i roll with it.
                founderInn: Inn.Create(value: founder.Inn, clientType: ClientType.IndividualEntrepreneur),
                fullName: founder.FullName
            );

        repository.Add(client);

        await Task.Yield();

        return new CreateClientResult
        {
            ClientId = client.Id
        };
    }
}
