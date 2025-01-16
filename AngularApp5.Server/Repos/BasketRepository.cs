using AngularApp5.Server.Data;
using AngularApp5.Server.Interfaces;
using AngularApp5.Server.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace AngularApp5.Server.Repos
{
    public class BasketRepository : IBasketRepository
    {
        private readonly StoreContext basketContext;

        public BasketRepository(StoreContext basketContext)
        {
            this.basketContext = basketContext;
        }
        public async Task<bool> DeleteBasketAsync(string id)
        {
            var basket = await basketContext.Baskets
                .FirstOrDefaultAsync(b => b.Id == id);

            if (basket == null)
            {
                return false;
            }

            basketContext.Baskets.Remove(basket);

            await basketContext.SaveChangesAsync();

            return true;
        }


        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            var data = await basketContext.Baskets.FindAsync(basketId);

            return data;
        }

        public async Task PostBasket(CustomerBasket newBasket)
        {
            basketContext.Baskets.Add(newBasket);
            await basketContext.SaveChangesAsync();
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var existingBasket = await basketContext.Baskets
                .FirstOrDefaultAsync(b => b.Id == basket.Id);

            if (existingBasket == null)
            {
                basketContext.Baskets.Add(basket);
            }
            else
            {
                existingBasket.Items = basket.Items;
            }

            await basketContext.SaveChangesAsync();

            return basket;
        }
    }
}
