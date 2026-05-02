using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.ClientRegistry.Domain.Clients;
using Modules.ClientRegistry.Domain.Inns;

namespace Modules.ClientRegistry.Infrastructure.EfCore.Configurations;

public class ClientTypeConfiguration : IEntityTypeConfiguration<Client>
{
    #region Implementation of IEntityTypeConfiguration<Client>

    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasConversion(
                convertToProviderExpression: c => c.Id, convertFromProviderExpression: id => ClientId.Of(id)
            );

        // PostgreSQL Optimistic Concurrency.
        // Npgsql automatically maps this to the hidden 'xmin' column.
        builder.Property<uint>("Version")
            .IsRowVersion();

        builder.Property(c => c.Inn)
            .HasConversion(
                convertToProviderExpression: inn => inn.Value,
                convertFromProviderExpression: dbString => Inn.Of(dbString)
            )
            .HasMaxLength(12)
            .IsRequired();

        // Save the Enum as a readable string
        builder.Property(c => c.Type)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.HasMany(c => c.Founders)
            .WithOne()
            // .HasForeignKey("ClientId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Metadata.FindNavigation(nameof(Client.Founders))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    #endregion
}
