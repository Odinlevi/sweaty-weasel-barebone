using System.Reflection;
using Modules.ClientRegistry.Domain.SeedWork.Models;

namespace Modules.ClientRegistry.Domain;

public static class StronglyTypedIdTypeDescriptor
{
    public static void AddStronglyTypedIdConverter(Action<Type> additionalAction)
    {
        Assembly.GetExecutingAssembly()
            .ExportedTypes
            .Where(x => x is { IsGenericTypeDefinition: false, IsAbstract: false }
                        && x.BaseType == typeof(IdentityBase)
            )
            .ToList().ForEach(idType => { additionalAction?.Invoke(idType); });
    }
}
