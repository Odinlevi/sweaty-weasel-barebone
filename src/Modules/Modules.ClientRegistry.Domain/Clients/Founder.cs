using Modules.ClientRegistry.Domain.Exceptions;
using Modules.ClientRegistry.Domain.Inns;
using Modules.ClientRegistry.Domain.SeedWork.Models;

namespace Modules.ClientRegistry.Domain.Clients;

public class Founder : EntityBase<FounderId>
{
    #region Constructors

    private Founder(FounderId id, Inn inn, string fullName, DateTime createdAt, DateTime updatedAt) : base(id)
    {
        Inn       = inn;
        FullName  = fullName;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    #endregion

    public Inn      Inn       { get; private set; }
    public string   FullName  { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    #region Creations

    public static Founder Create(Inn inn, string fullName)
    {
        if (inn is null)
            throw new DomainException("Inn cannot be null.");

        if (string.IsNullOrWhiteSpace(fullName))
            throw new DomainException("Full name cannot be null or empty.");

        var now = DateTime.UtcNow;

        return new Founder(
            id: FounderId.New,
            inn: inn,
            fullName: fullName,
            createdAt: now,
            updatedAt: now
        );
    }

    #endregion
}
