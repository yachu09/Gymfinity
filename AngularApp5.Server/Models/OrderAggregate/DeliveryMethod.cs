﻿namespace AngularApp5.Server.Models.OrderAggregate
{
    public class DeliveryMethod : BaseModel
    {
        public string ShortName { get; set; }
        public string DeliveryTime { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
