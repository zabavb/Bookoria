﻿using Library.OrderEntities;
using Microsoft.Identity.Client;

namespace OrderApi.Models.Extensions
{
    public class OrderFilter
    {
        public DateTime? OrderDateStart { get; set; }
        public DateTime? OrderDateEnd { get; set; }
        public DateTime? DeliveryDateStart { get; set; }
        public DateTime? DeliveryDateEnd { get; set; }
        public OrderStatus? Status { get; set; }
        public Guid? DeliveryId { get; set; }
    }
}