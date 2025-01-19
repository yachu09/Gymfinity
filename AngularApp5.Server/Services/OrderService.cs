using AngularApp5.Server.Interfaces;
using AngularApp5.Server.Models;
using AngularApp5.Server.Models.OrderAggregate;
using AngularApp5.Server.Specifications;

namespace AngularApp5.Server.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository basketRepo;
        private readonly IUnitOfWork unitOfWork;

        public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork)
        {
            this.basketRepo = basketRepo;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            var basket = await basketRepo.GetBasketAsync(basketId);

            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name,
                    productItem.ImageUrl);
                //cena przypisana z obiektu produktu a nie koszyka w celu zabezpieczenia 
                //przed zmianą ceny
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }

            var deliveryMethod = await unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            var subtotal = items.Sum(item => item.Price * item.Quantity);

            var order = new Order(items, buyerEmail, shippingAddress,
                deliveryMethod, subtotal);

            unitOfWork.Repository<Order>().Add(order);

            var result = await unitOfWork.Complete();

            if (result <= 0) return null;
            
            await basketRepo.DeleteBasketAsync(basketId);

            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(id, buyerEmail);

            return await unitOfWork.Repository<Order>().GetModelWithSpecAsync(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);

            return await unitOfWork.Repository<Order>().ListWithSpecAsync(spec);
        }
    }
}
