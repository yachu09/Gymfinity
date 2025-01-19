using AngularApp5.Server.Models;
using AngularApp5.Server.Models.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AngularApp5.Server.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductsTypes { get; set; }
        public DbSet<ProductBrand> ProductsBrands { get; set; }
        public DbSet<CustomerBasket> Baskets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerBasket>()
                .Property(b => b.Items)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }), // Serializacja
                    v => JsonSerializer.Deserialize<List<BasketItem>>(v, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) // Deserializacja
                );

            modelBuilder.Entity<CustomerBasket>()
                .HasKey(b => b.Id); // Ustaw klucz główny dla CustomerBasket

            //modelBuilder.Entity<Address>().HasNoKey();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //nullable PaymentIntent
            modelBuilder.Entity<Order>()
                .Property(o => o.PaymentIntentId)
                .IsRequired(false); // Ustaw jako opcjonalną kolumnę

        }
    }
}
