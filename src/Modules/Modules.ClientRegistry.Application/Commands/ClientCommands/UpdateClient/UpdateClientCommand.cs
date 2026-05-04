using Modules.ClientRegistry.Domain.Clients;

namespace Modules.ClientRegistry.Application.Commands.ClientCommands.UpdateClient;

public class UpdateClientCommand : IClientRegistryCommand<UpdateClientResult>
{
    public ClientId ClientId   { get; set; }
    public string   ClientName { get; set; }
}
