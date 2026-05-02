using Modules.ClientRegistry.Domain.SeedWork.Models;

namespace Modules.ClientRegistry.Domain.Clients;

public class ClientId : IdentityBase
{
    #region Constructors

    private ClientId(Guid id) : base(id)
    {
    }

    #endregion

    public static ClientId New   => new(Guid.NewGuid());
    public static ClientId Empty => new(Guid.Empty);

    public static ClientId Of(Guid id)
    {
        return new ClientId(id);
    }
}
