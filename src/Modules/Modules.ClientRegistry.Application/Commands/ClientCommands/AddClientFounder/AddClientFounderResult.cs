using Modules.ClientRegistry.Domain.Clients;

namespace Modules.ClientRegistry.Application.Commands.ClientCommands.AddClientFounder;

public class AddClientFounderResult
{
    public ClientId  ClientId  { get; set; }
    public FounderId FounderId { get; set; }
}
