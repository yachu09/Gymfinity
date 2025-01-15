using AngularApp5.Server.Models;
using Microsoft.EntityFrameworkCore;

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
    }
}
