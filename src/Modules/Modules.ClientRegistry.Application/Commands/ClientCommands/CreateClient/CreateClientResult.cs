using Modules.ClientRegistry.Domain.Clients;

namespace Modules.ClientRegistry.Application.Commands.ClientCommands.CreateClient;

public class CreateClientResult
{
    public ClientId? ClientId { get; init; }
}
