using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopASP2.Domain.Entities;

namespace ShopASP2.Infrastructure.Data
{
    public class ShopASP2DBContext : IdentityDbContext<User>
    {
        public ShopASP2DBContext(DbContextOptions<ShopASP2DBContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderID)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<OrderItem>()
                .HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(pi => pi.ProductID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<CartItem>()
                .HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(pi => pi.ProductID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<CartItem>()
                .HasOne(ci => ci.User)
                .WithMany()
                .HasForeignKey(ci => ci.UserID)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
