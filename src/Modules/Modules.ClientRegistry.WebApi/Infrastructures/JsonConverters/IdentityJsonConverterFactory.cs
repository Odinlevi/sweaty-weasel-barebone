using System.Text.Json;
using System.Text.Json.Serialization;
using Modules.ClientRegistry.Domain.SeedWork.Models;

namespace Modules.ClientRegistry.WebApi.Infrastructures.JsonConverters;

public class IdentityJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert is { IsGenericType: false, BaseType: not null } &&
               typeToConvert.BaseType == typeof(IdentityBase);
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var converterType = typeof(IdentityJsonConverter<>).MakeGenericType(typeToConvert);

        return Activator.CreateInstance(converterType) as JsonConverter
               ?? throw new JsonException(
                   $"Failed to create JSON converter for identity type '{typeToConvert.FullName}'."
               );
    }
}
