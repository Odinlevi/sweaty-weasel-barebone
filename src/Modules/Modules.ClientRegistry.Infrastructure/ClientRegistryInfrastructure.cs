using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.ClientRegistry.Infrastructure.EfCore;
using Modules.ClientRegistry.Infrastructure.Repositories;
using Modules.ClientRegistry.Infrastructure.Repositories.Implementations;

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

        var services = builder.Services;

        services.AddDbContextPool<DbContext, ClientRegistryDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            }
        );

        services.AddScoped(serviceType: typeof(IClientRepository), implementationType: typeof(ClientRepository));

        return builder;
    }
}
