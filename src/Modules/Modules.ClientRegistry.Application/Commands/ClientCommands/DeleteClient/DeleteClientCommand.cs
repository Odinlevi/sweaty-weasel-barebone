using Modules.ClientRegistry.Domain.Clients;

namespace Modules.ClientRegistry.Application.Commands.ClientCommands.DeleteClient;

public class DeleteClientCommand : IClientRegistryCommand<DeleteClientResult>
{
    public ClientId ClientId { get; set; }
}
