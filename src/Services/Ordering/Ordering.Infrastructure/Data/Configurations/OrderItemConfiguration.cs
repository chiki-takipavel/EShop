using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations;

internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id).HasConversion(orderItemId => orderItemId.Value,
            databaseId => OrderItemId.Of(databaseId));

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(i => i.ProductId);

        builder.Property(i => i.Quantity).IsRequired();

        builder.Property(i => i.Price).IsRequired();
    }
}
