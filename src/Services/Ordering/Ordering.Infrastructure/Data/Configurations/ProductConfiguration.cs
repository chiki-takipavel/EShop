﻿namespace Ordering.Infrastructure.Data.Configurations;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasConversion(productId => productId.Value,
            databaseId => ProductId.Of(databaseId));

        builder.Property(p => p.Name).HasMaxLength(100).IsRequired();

        builder.Property(p => p.Price).IsRequired();
    }
}
