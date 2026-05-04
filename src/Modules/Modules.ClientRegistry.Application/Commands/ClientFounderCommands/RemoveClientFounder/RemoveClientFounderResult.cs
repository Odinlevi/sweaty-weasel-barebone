using Modules.ClientRegistry.Domain.Clients;

namespace Modules.ClientRegistry.Application.Commands.ClientFounderCommands.RemoveClientFounder;

public class RemoveClientFounderResult
{
    public ClientId  ClientId  { get; set; }
    public FounderId FounderId { get; set; }
}
