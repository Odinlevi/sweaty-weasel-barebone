using Modules.ClientRegistry.Domain.Clients;

namespace Modules.ClientRegistry.Application.Commands.ClientCommands.AddClientFounder;

public class AddClientFounderCommand : IClientRegistryCommand<AddClientFounderResult>
{
    public ClientId ClientId        { get; set; }
    public string   FounderFullName { get; set; }
    public string   FounderInn      { get; set; }
}
