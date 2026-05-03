using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.ClientRegistry.Application.Commands;
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

        builder.AddCustomMediatR(commandAssembly);

        var services = builder.Services;

        services.AddDbContextPool<DbContext, ClientRegistryDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            }
        );

        services.AddScoped(serviceType: typeof(IRepository<,>), implementationType: typeof(Repository<,>));

        return builder;
    }
}
