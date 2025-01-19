using AngularApp5.Server.Interfaces;
using AngularApp5.Server.Models;
using AngularApp5.Server.Models.OrderAggregate;

namespace AngularApp5.Server.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> orderRepo;
        private readonly IGenericRepository<DeliveryMethod> dmRepo;
        private readonly IGenericRepository<Product> productRepo;
        private readonly IBasketRepository basketRepo;

        public OrderService(IGenericRepository<Order> orderRepo, 
            IGenericRepository<DeliveryMethod> dmRepo, 
            IGenericRepository<Product> productRepo,
            IBasketRepository basketRepo)
        {
            this.orderRepo = orderRepo;
            this.dmRepo = dmRepo;
            this.productRepo = productRepo;
            this.basketRepo = basketRepo;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            var basket = await basketRepo.GetBasketAsync(basketId);

            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await productRepo.GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name,
                    productItem.ImageUrl);
                //cena przypisana z obiektu produktu a nie koszyka w celu zabezpieczenia 
                //przed zmianą ceny
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }

            var deliveryMethod = await dmRepo.GetByIdAsync(deliveryMethodId);

            var subtotal = items.Sum(item => item.Price * item.Quantity);

            var order = new Order(items, buyerEmail, shippingAddress,
                deliveryMethod, subtotal);

            //save to db
            
            return order;
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
