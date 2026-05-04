using System.Reflection;
using Microsoft.OpenApi;
using Modules.ClientRegistry.Domain;

namespace Modules.ClientRegistry.WebApi.Infrastructures;

public static class SwaggerRegistration
{
    public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint(url: "/swagger/V1/swagger.json", name: "ClientRegistry API"));

        return app;
    }

    public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    name: "V1", info: new OpenApiInfo
                    {
                        Title       = "ClientRegistry API",
                        Version     = "V1",
                        Description = "Web API for technical task."
                    }
                );

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(path1: AppContext.BaseDirectory, path2: xmlFile);
                c.IncludeXmlComments(xmlPath);

                StronglyTypedIdTypeDescriptor.AddStronglyTypedIdConverter(idType =>
                    {
                        c.MapType(
                            type: idType, schemaFactory: () => new OpenApiSchema
                            {
                                Type   = JsonSchemaType.String,
                                Format = "uuid"
                            }
                        );
                    }
                );
            }
        );
        return services;
    }
}
