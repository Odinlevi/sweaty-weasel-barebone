using Modules.ClientRegistry.Domain.ClientTypes;

namespace Modules.ClientRegistry.Application.Commands.ClientCommands.CreateClient;

public class CreateClientCommand : IClientRegistryCommand<CreateClientResult>
{
    public string     Inn  { get; set; }
    public string     Name { get; set; }
    public ClientType Type { get; set; }

    public List<FounderInClient> Founders { get; set; } = [];

    public CreateClientCommand AddFounder(string inn, string fullName)
    {
        Founders.Add(
            new FounderInClient
            {
                Inn      = inn,
                FullName = fullName
            }
        );

        return this;
    }

    public class FounderInClient
    {
        public string Inn      { get; set; }
        public string FullName { get; set; }
    }
}
