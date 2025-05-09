﻿namespace Discount.Grpc.Data;

public class DiscountContext(DbContextOptions<DiscountContext> options) : DbContext(options)
{
    public DbSet<Coupon> Coupones { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon { Id = 1, ProductName = "IPhone X", Description = "IPhone X Discount", Percentage = 2 },
            new Coupon { Id = 2, ProductName = "Samsung 10", Description = "Samsung 10 Discount", Percentage = 4 }
        );

        modelBuilder.Entity<Coupon>().Property(c => c.ProductName).UseCollation("NOCASE");
    }
}
