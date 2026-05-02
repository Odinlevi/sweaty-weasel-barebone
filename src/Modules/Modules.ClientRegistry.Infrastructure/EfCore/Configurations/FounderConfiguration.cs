using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.ClientRegistry.Domain.Clients;
using Modules.ClientRegistry.Domain.Inns;

namespace Modules.ClientRegistry.Infrastructure.EfCore.Configurations;

public class FounderConfiguration : IEntityTypeConfiguration<Founder>
{
    #region Implementation of IEntityTypeConfiguration<Founder>

    public void Configure(EntityTypeBuilder<Founder> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(c => c.Id)
            .HasConversion(
                convertToProviderExpression: c => c.Id, convertFromProviderExpression: id => FounderId.Of(id)
            );

        // PostgreSQL Optimistic Concurrency.
        // Npgsql automatically maps this to the hidden 'xmin' column.
        builder.Property<uint>("Version")
            .IsRowVersion();

        builder.Property(f => f.FullName)
            .HasMaxLength(200)
            .IsRequired();

        // Founder also uses the INN Value Object
        // builder.OwnsOne(f => f.Inn, innBuilder =>
        // {
        //     innBuilder.Property(i => i.Value)
        //         .HasMaxLength(12)
        //         .IsRequired();
        // });

        builder.Property(c => c.Inn)
            .HasConversion(
                inn => inn.Value,
                dbString => Inn.Of(dbString)
            )
            .HasMaxLength(12)
            .IsRequired();
    }

    #endregion
}
