using AngularApp5.Server.Models;
using AngularApp5.Server.Models.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AngularApp5.Server.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if(!context.ProductsBrands.Any())
                {
                    var brandsData = File.ReadAllText("Data/SeedData/brands.json");

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach(var item in brands)
                    {
                        context.ProductsBrands.Add(item);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.ProductsTypes.Any())
                {
                    var typesData = File.ReadAllText("Data/SeedData/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (var item in types)
                    {
                        context.ProductsTypes.Add(item);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText("Data/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var item in products)
                    {
                        context.Products.Add(item);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.DeliveryMethods.Any())
                {
                    var deliveryMethodsData = File.ReadAllText("Data/SeedData/delivery.json");

                    var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);

                    foreach (var item in deliveryMethods)
                    {
                        context.DeliveryMethods.Add(item);
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
