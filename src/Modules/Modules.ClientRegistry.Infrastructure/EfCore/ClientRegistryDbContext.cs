using Microsoft.EntityFrameworkCore;
using Modules.ClientRegistry.Domain.Clients;

namespace Modules.ClientRegistry.Infrastructure.EfCore;

public class ClientRegistryDbContext(DbContextOptions<ClientRegistryDbContext> options) : DbContext(options)
{
    public DbSet<Client> Clients => Set<Client>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // IEntityTypeConfiguration<T> lookup.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientRegistryDbContext).Assembly);
    }
}
