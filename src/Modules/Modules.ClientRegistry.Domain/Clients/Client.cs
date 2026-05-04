using Modules.ClientRegistry.Domain.ClientTypes;
using Modules.ClientRegistry.Domain.Exceptions;
using Modules.ClientRegistry.Domain.Inns;
using Modules.ClientRegistry.Domain.SeedWork.Models;

namespace Modules.ClientRegistry.Domain.Clients;

public class Client : AggregateRoot<ClientId>
{
    private readonly List<Founder> _founders = new();

    public Inn        Inn       { get; private set; }
    public string     Name      { get; private set; }
    public ClientType Type      { get; }
    public DateTime   CreatedAt { get; private set; }
    public DateTime   UpdatedAt { get; private set; }

    public IReadOnlyCollection<Founder> Founders => _founders.AsReadOnly();

    #region Creation

    public static Client Create(Inn inn, string name, ClientType type)
    {
        return new Client(inn: inn, name: name, type: type);
    }

    #endregion

    #region Constructors

    private Client(ClientId id, Inn inn, string name, ClientType type) : base(id)
    {
        Inn  = inn ?? throw new ArgumentNullException(nameof(inn));
        Name = string.IsNullOrWhiteSpace(name) ? throw new DomainException("Client name cannot be empty.") : name;
        Type = type;

        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    private Client(Inn inn, string name, ClientType type) : this(
        id: ClientId.New, inn: inn, name: name, type: type
    )
    {
    }

    #endregion

    #region Behaviors

    public Founder AddFounder(Inn founderInn, string fullName)
    {
        // THE CORE BUSINESS RULE
        if (Type == ClientType.IndividualEntrepreneur)
            throw new DomainException("An Individual Entrepreneur (ИП) cannot have founders.");

        // Check if founder already exists
        if (_founders.Any(f => f.Inn == founderInn))
            throw new DomainException("A founder with this INN already exists for this client.");

        if (_founders.Count >= 10)
            throw new DomainException("A client cannot have more than 10 founders.");

        var founder = Founder.Create(inn: founderInn, fullName: fullName);
        _founders.Add(founder);

        UpdateTimestamp();

        return founder;
    }

    public void RemoveFounder(Guid founderId)
    {
        var founder = _founders.FirstOrDefault(f => f.Id.Id == founderId);
        if (founder is null)
            throw new DomainException("Founder not found.");

        _founders.Remove(founder);

        UpdateTimestamp();
    }

    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new DomainException("Client n ame cannot be empty.");

        Name = newName;
        UpdateTimestamp();
    }

    private void UpdateTimestamp()
    {
        UpdatedAt = DateTime.UtcNow;
    }

    #endregion
}
