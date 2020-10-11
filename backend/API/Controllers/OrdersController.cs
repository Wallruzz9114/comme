using System.Collections.Generic;
using System.Threading.Tasks;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Interfaces;
using Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    [Authorize]
    public class OrdersController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(NewOrderViewModel newOrderViewModel)
        {
            var customerEmail = HttpContext.User.GetCustomerEmail();
            var shippingAddress = _mapper.Map<AddressViewModel, ShippingAddress>(newOrderViewModel.ShippingAddress);
            var order = await _orderService.CreateOrderAsync(customerEmail, newOrderViewModel.DeliveryMethodId, newOrderViewModel.CartId, shippingAddress);

            if (order == null) return BadRequest(new APIResponse(StatusCodes.Status400BadRequest, "Problem while creating order"));

            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderViewModel>>> GetOrdersForCustomer()
        {
            var customerEmail = HttpContext.User.GetCustomerEmail();
            var orders = await _orderService.GetOrdersForUserAsync(customerEmail);

            return Ok(_mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderViewModel>>(orders));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderViewModel>> GetOrder(int id)
        {
            var customerEmail = HttpContext.User.GetCustomerEmail();
            var order = await _orderService.GetOrderAsync(id, customerEmail);

            if (order == null) return NotFound(new APIResponse(StatusCodes.Status404NotFound));

            return Ok(_mapper.Map<Order, OrderViewModel>(order));
        }

        [HttpGet("deliverymethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods() =>
            Ok(await _orderService.GetDeliveryMethodsAsync());
    }
}