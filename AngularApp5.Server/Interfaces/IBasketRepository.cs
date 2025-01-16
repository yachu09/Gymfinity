using AngularApp5.Server.Models;

namespace AngularApp5.Server.Interfaces
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string Id);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string id);
        Task PostBasket(CustomerBasket newBasket);
    }
}
