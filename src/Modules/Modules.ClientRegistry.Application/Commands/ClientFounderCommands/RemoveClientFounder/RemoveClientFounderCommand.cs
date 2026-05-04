using Modules.ClientRegistry.Domain.Clients;

namespace Modules.ClientRegistry.Application.Commands.ClientFounderCommands.RemoveClientFounder;

public class RemoveClientFounderCommand : IClientRegistryCommand<RemoveClientFounderResult>
{
    public ClientId  ClientId  { get; set; }
    public FounderId FounderId { get; set; }
}
