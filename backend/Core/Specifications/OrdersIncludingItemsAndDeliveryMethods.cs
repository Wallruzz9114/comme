using Core.Services;
using Models;

namespace Core.Specifications
{
    public class OrdersIncludingItemsAndDeliveryMethods : SpecificationService<Order>
    {
        public OrdersIncludingItemsAndDeliveryMethods(string customerEmail) : base(order => order.CustomerEmail == customerEmail)
        {
            AddInclude(order => order.OrderItems);
            AddInclude(order => order.DeliveryMethod);
            AddOrderByDescending(order => order.OrderDate);
        }

        public OrdersIncludingItemsAndDeliveryMethods(int orderId, string customerEmail)
            : base(order => order.Id == orderId && order.CustomerEmail == customerEmail)
        {
            AddInclude(order => order.OrderItems);
            AddInclude(order => order.DeliveryMethod);
        }
    }
}