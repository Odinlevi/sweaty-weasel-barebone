#nullable disable

using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using Modules.ClientRegistry.Domain.SeedWork.Models;

namespace Modules.ClientRegistry.Infrastructure;

public class StronglyTypedIdConverter<TIdentity> : TypeConverter where TIdentity : IdentityBase
{
    #region Overrides of TypeConverter

    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        return sourceType == typeof(string) || base.CanConvertFrom(context: context, sourceType: sourceType);
    }

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
        var stringValue = value as string;

        if (!string.IsNullOrEmpty(stringValue) && Guid.TryParse(input: stringValue, result: out var guidValue))
            return (TIdentity)Activator.CreateInstance(
                type: typeof(TIdentity),
                bindingAttr: BindingFlags.NonPublic | BindingFlags.Instance,
                binder: null,
                args: new object[] { guidValue },
                culture: null
            );

        return base.ConvertFrom(context: context, culture: culture, value: value);
    }

    #endregion
}
