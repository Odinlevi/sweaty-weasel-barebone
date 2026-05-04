using Modules.ClientRegistry.Domain.Clients;

namespace Modules.ClientRegistry.Application.Commands.ClientCommands.UpdateClientFounder;

public class UpdateClientFounderCommand : IClientRegistryCommand<UpdateClientFounderResult>
{
    public ClientId  ClientId        { get; set; }
    public FounderId FounderId       { get; set; }
    public string    FounderFullName { get; set; }
}
