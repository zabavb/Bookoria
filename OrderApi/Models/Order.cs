﻿using Library.OrderEntities;

namespace OrderApi.Models
{
    internal class Order
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public List<Guid> BookIds { get; set; } = new List<Guid>();

        public string Region { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Address { get; set; } = null!;

        public float Price { get; set; }
        public DeliveryType Delivery { get; set; }
        public float DeliveryPrice { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime DeliveryDate { get; set; }  // + 2 days, in case of Nova Post

        public OrderStatus Status { get; set; } = OrderStatus.RECEIVED;
    }
}
