﻿namespace AngularApp5.Server.Models.OrderAggregate
{
    public class Order : BaseModel
    {
        public Order()
        {
            
        }
        public Order(IReadOnlyList<OrderItem> orderItems, string buyerEmail, 
            Address shipToAddress, DeliveryMethod deliveryMethod, decimal subtotal)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            Subtotal = subtotal;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public Address ShipToAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string? PaymentIntentId { get; set; }
        public decimal getTotal()
        {
            return Subtotal + DeliveryMethod.Price;
        }


    }
}
