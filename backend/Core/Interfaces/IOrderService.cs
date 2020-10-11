using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Core.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string email, int deliveryMethodId, string cartId, ShippingAddress shippingAddress);
        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string customerEmail);
        Task<Order> GetOrderAsync(int orderId, string customerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}