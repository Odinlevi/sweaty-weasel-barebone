using Modules.ClientRegistry.Domain.SeedWork.Models;

namespace Modules.ClientRegistry.Domain.Clients;

public class FounderId : IdentityBase
{
    #region Constructors

    private FounderId(Guid id) : base(id)
    {
    }

    #endregion

    public static FounderId New   => new(Guid.NewGuid());
    public static FounderId Empty => new(Guid.Empty);

    public static FounderId Of(Guid id)
    {
        return new FounderId(id);
    }
}
