namespace Modules.ClientRegistry.Domain.SeedWork.Models;

/// <summary>
/// https://enterprisecraftsmanship.com/posts/value-object-better-implementation/
/// </summary>
public abstract class ValueObjectBase
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(objA: this, objB: obj)) return true;

        if (ReferenceEquals(objA: null, objB: obj)) return false;

        if (GetType() != obj.GetType()) return false;

        var valueObject = (ValueObjectBase)obj;

        return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents().Aggregate(
            seed: 1, func: (current, obj) =>
            {
                unchecked
                {
                    return current * 23 + (obj?.GetHashCode() ?? 0);
                }
            }
        );
    }

    public static bool operator ==(ValueObjectBase object1, ValueObjectBase object2)
    {
        if (ReferenceEquals(objA: object1, objB: null) && ReferenceEquals(objA: object2, objB: null))
            return true;

        if (ReferenceEquals(objA: object1, objB: null) || ReferenceEquals(objA: object2, objB: null))
            return false;

        return object1.Equals(object2);
    }

    public static bool operator !=(ValueObjectBase object1, ValueObjectBase object2)
    {
        return !(object1 == object2);
    }
}
