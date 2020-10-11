using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Specifications;
using Models;

namespace Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartService _cartService;

        public OrderService(IUnitOfWork unitOfWork, ICartService cartService)
        {
            _unitOfWork = unitOfWork;
            _cartService = cartService;
        }

        public async Task<Order> CreateOrderAsync(string customerEmail, int deliveryMethodId, string cartId, ShippingAddress shippingAddress)
        {
            var cart = await _cartService.GetCartAsync(cartId);
            var orderItems = new List<OrderItem>();

            foreach (var cartItem in cart.CartItems)
            {
                var productItem = await _unitOfWork.Service<Product>().GetByIdAsync(cartItem.Id);
                var productItemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureURL);
                var orderItem = new OrderItem(productItemOrdered, productItem.Price, cartItem.Quantity);

                orderItems.Add(orderItem);
            }

            var deliveryMethod = await _unitOfWork.Service<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
            var subTotal = orderItems.Sum(orderItem => orderItem.Price * orderItem.Quantity);
            var order = new Order(customerEmail, shippingAddress, deliveryMethod, orderItems, subTotal);

            _unitOfWork.Service<Order>().Create(order);

            var orderSuccessfullySaved = await _unitOfWork.Save() > 0;

            if (orderSuccessfullySaved)
            {
                await _cartService.EmptyCartAsync(cartId);
                return order;
            }

            return null;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync() =>
            await _unitOfWork.Service<DeliveryMethod>().ListAllAsync();

        public async Task<Order> GetOrderAsync(int orderId, string customerEmail)
        {
            var includeItemsAndDeliveryMethodQuery = new OrdersIncludingItemsAndDeliveryMethods(orderId, customerEmail);
            return await _unitOfWork.Service<Order>().GetOneWithSpecification(includeItemsAndDeliveryMethodQuery);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string customerEmail)
        {
            var includeItemsAndDeliveryMethodQuery = new OrdersIncludingItemsAndDeliveryMethods(customerEmail);
            return await _unitOfWork.Service<Order>().ListAllWithSpecificationAsync(includeItemsAndDeliveryMethodQuery);
        }
    }
}