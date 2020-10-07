using System.Threading.Tasks;
using AutoMapper;
using Core.Interfaces;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public CartController(ICartService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Cart>> GetCartById(string id) =>
            Ok(await _cartService.GetCartAsync(id) ?? new Cart(id));

        [HttpPost]
        public async Task<ActionResult<Cart>> UpdateOrCreateCart(CartViewModel cartViewModel)
        {
            var cart = _mapper.Map<CartViewModel, Cart>(cartViewModel);
            return Ok(await _cartService.UpdateOrCreateCartAsync(cart));
        }

        [HttpDelete]
        public async Task EmptyCart(string id) => await _cartService.EmptyCartAsync(id);
    }
}