using Modules.ClientRegistry.Domain.ClientTypes;
using Modules.ClientRegistry.Domain.Exceptions;
using Modules.ClientRegistry.Domain.SeedWork.Models;

namespace Modules.ClientRegistry.Domain.Inns;

public class Inn : ValueObjectBase
{
    private Inn(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Inn Create(string value, ClientType clientType)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("INN cannot be empty.");

        if (!value.All(char.IsDigit))
            throw new DomainException("INN must contain only digits.");

        if (clientType == ClientType.LegalEntity && value.Length != 10)
            throw new DomainException("Legal Entity (ЮЛ) INN must be exactly 10 digits.");

        if (clientType == ClientType.IndividualEntrepreneur && value.Length != 12)
            throw new DomainException("Individual Entrepreneur (ИП) INN must be exactly 12 digits.");

        return new Inn(value);
    }

    // should be used by persistence only, subject to refactoring
    public static Inn Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("INN cannot be empty.");
        if (!value.All(char.IsDigit))
            throw new DomainException("INN must contain only digits.");
        return new Inn(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
