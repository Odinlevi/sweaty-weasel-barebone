using Modules.ClientRegistry.Domain.Clients;

namespace Modules.ClientRegistry.Application.Commands.ClientCommands.UpdateClientFounder;

public class UpdateClientFounderResult
{
    public ClientId  ClientId { get; set; }
    public FounderId FounderId  { get; set; }
    public bool      Success  { get; set; }
}
