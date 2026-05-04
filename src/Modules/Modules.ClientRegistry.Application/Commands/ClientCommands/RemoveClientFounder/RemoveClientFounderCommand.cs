using Modules.ClientRegistry.Domain.Clients;

namespace Modules.ClientRegistry.Application.Commands.ClientCommands.RemoveClientFounder;

public class RemoveClientFounderCommand : IClientRegistryCommand<RemoveClientFounderResult>
{
    public ClientId  ClientId  { get; set; }
    public FounderId FounderId { get; set; }
}
