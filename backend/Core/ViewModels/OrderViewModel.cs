using System;
using System.Collections.Generic;
using Models;

namespace Core.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string CustomerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public decimal DeliveryMethodPrice { get; set; }
        public string DeliveryMethod { get; set; }
        public IReadOnlyList<OrderItemViewModel> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public string OrderStatus { get; set; }
        public decimal Total { get; set; }
        public string PaymentMethodId { get; set; }
    }
}