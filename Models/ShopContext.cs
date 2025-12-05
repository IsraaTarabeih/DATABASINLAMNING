using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DATABASINLÄMNING.Models
{
    public class ShopContext : DbContext
    {
        // Mappar data mot tabellerna/relationerna i databasen.
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderRow> OrderRows => Set<OrderRow>();
        public DbSet<Product> Products => Set<Product>();

        // Talar om var DB-filen ska ligga + anger vilken databas-provider (SQLite) som ska användas.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Talar om vägen till vart filen sparas.
            var dbPath = Path.Combine(AppContext.BaseDirectory, "shop.db");

            optionsBuilder.UseSqlite($"Filename = {dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(e =>
            {
                e.HasKey(g => g.CategoryId);

                e.Property(g => g.CategoryName).IsRequired().HasMaxLength(100);
                e.Property(g => g.CategoryDescription).HasMaxLength(100);
            });

            modelBuilder.Entity<Customer>(e =>
            {
                e.HasKey(c => c.CustomerId);

                e.Property(c => c.CustomerName).IsRequired().HasMaxLength(100);
                e.Property(c => c.Email).IsRequired().HasMaxLength(100);
                e.Property(c => c.City).HasMaxLength(100);

                e.HasIndex(c => c.Email).IsUnique();
            });

            modelBuilder.Entity<Order>(e =>
            {
                e.HasKey(o => o.OrderId);

                e.Property(o => o.OrderDate).IsRequired();
                e.Property(o => o.Status).IsRequired().HasMaxLength(100);

                // Relation: en Customer kan ha flera Orders. Customer 1 - N Orders.
                e.HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<OrderRow>(e =>
            {
                e.HasKey(r => r.OrderRowId);

                e.Property(r => r.Quantity).IsRequired();
                e.Property(r => r.UnitPrice).IsRequired();

                // Relation: en Order kan ha flera OrderRows. Order 1 - N OrderRows.
                e.HasOne(r => r.Order)
                .WithMany(o => o.OrderRows)
                .HasForeignKey(r => r.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

                // Relation: en Product kan finnas i flera OrderRows. Product 1 - N OrderRows.
                e.HasOne(r => r.Product)
                .WithMany(p => p.OrderRows)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Product>(e =>
            {
                e.HasKey(o => o.ProductId);

                e.Property(p => p.ProductName).IsRequired().HasMaxLength(100);
                e.Property(p => p.Price).IsRequired();
                
                // Relation: en Product tillhör en Category, och en Category kan ha många Products. Category 1 - N Products.
                e.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}