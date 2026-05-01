namespace Modules.ClientRegistry.Domain.SeedWork.Models;

public abstract class EntityBase<TIdentity> : IEquatable<EntityBase<TIdentity>> where TIdentity : IdentityBase
{
    #region Constructors

    protected EntityBase(TIdentity id)
    {
        Id = id;
    }

    #endregion

    public TIdentity Id { get; }

    #region Overrides of Object

    public override string ToString()
    {
        return $"{GetType().Name}#[Identity={Id}]";
    }

    #endregion

    public static bool operator ==(EntityBase<TIdentity> a, EntityBase<TIdentity> b)
    {
        if (ReferenceEquals(objA: a, objB: null) && ReferenceEquals(objA: b, objB: null))
            return true;

        if (ReferenceEquals(objA: a, objB: null) || ReferenceEquals(objA: b, objB: null))
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(EntityBase<TIdentity> a, EntityBase<TIdentity> b)
    {
        return !(a == b);
    }

    #region Implementation of IEquatable<Entity>

    public bool Equals(EntityBase<TIdentity>? other)
    {
        if (ReferenceEquals(objA: null, objB: other)) return false;

        if (ReferenceEquals(objA: this, objB: other)) return true;

        return Equals(objA: Id, objB: other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(objA: null, objB: obj)) return false;

        if (ReferenceEquals(objA: this, objB: obj)) return true;

        if (obj.GetType() != GetType()) return false;

        return Equals((EntityBase<TIdentity>)obj);
    }

    public override int GetHashCode()
    {
        return GetType().GetHashCode() * 907 + Id.GetHashCode();
    }

    #endregion
}
