namespace Modules.ClientRegistry.Domain.SeedWork.Models;

public abstract class IdentityBase : ValueObjectBase
{
    #region Constructors

    protected IdentityBase(Guid id)
    {
        Id = id;
    }

    #endregion

    public Guid Id { get; }

    #region Overrides of ValueObjectBase

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }

    #endregion

    #region Overrides of Object

    public override string ToString()
    {
        return $"{GetType().Name}:{Id}";
    }

    #endregion

    public static implicit operator Guid(IdentityBase id)
    {
        return id.Id;
    }
}
