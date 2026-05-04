using System.ComponentModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.ClientRegistry.Application.Commands;
using Modules.ClientRegistry.Application.Queries;
using Modules.ClientRegistry.Domain;
using Modules.ClientRegistry.Domain.SeedWork;
using Modules.ClientRegistry.Infrastructure.EfCore;
using Modules.ClientRegistry.Infrastructure.Repositories;
using Shared.Infrastructure.MediatR;

namespace Modules.ClientRegistry.Infrastructure;

public static class ClientRegistryInfrastructure
{
    public static WebApplicationBuilder AddClientRegistryInfrastructure(
        this WebApplicationBuilder builder,
        string                     nameOfConnectionString = "Default"
    )
    {
        var configuration    = builder.Configuration;
        var connectionString = configuration.GetConnectionString(nameOfConnectionString);

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException(
                $"Connection string '{nameOfConnectionString}' for '" +
                $"{nameof(ClientRegistryInfrastructure)}' is not found."
            );


        var commandAssembly = typeof(IClientRegistryCommand<>).Assembly;
        var queryAssembly   = typeof(IClientRegistryQueryRequest<>).Assembly;

        builder.AddCustomMediatR(commandAssembly, queryAssembly);

        var services = builder.Services;

        services.AddDbContextPool<DbContext, ClientRegistryDbContext>(options =>
            {
                options.UseNpgsql(connectionString);

                options.UseSnakeCaseNamingConvention();
            }
        );

        services.AddScoped(serviceType: typeof(IRepository<,>), implementationType: typeof(Repository<,>));

        StronglyTypedIdTypeDescriptor.AddStronglyTypedIdConverter(idType =>
            {
                var typeOfIdentity = typeof(StronglyTypedIdConverter<>).MakeGenericType(idType);
                TypeDescriptor.AddAttributes(type: idType, new TypeConverterAttribute(typeOfIdentity));
            }
        );

        return builder;
    }
}
