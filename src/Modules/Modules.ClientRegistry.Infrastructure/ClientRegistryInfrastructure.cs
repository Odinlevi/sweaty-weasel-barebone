using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.ClientRegistry.Infrastructure.EfCore;

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

        var services = builder.Services;

        services.AddDbContextPool<DbContext, ClientRegistryDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            }
        );

        //todo: repo

        return builder;
    }
}
