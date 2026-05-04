using Modules.ClientRegistry.Domain.Clients;

namespace Modules.ClientRegistry.Application.Commands.ClientCommands.UpdateClient;

public class UpdateClientResult
{
    public ClientId ClientId { get; set; }
    public bool     Success  { get; set; }
}
