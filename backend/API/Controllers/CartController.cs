using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService) => _cartService = cartService;

        [HttpGet]
        public async Task<ActionResult<Cart>> GetCartById(string id) =>
            Ok(await _cartService.GetCartAsync(id) ?? new Cart(id));

        [HttpPost]
        public async Task<ActionResult<Cart>> UpdateOrCreateCart(Cart cart) =>
            Ok(await _cartService.UpdateOrCreateCartAsync(cart));

        [HttpDelete]
        public async Task EmptyCart(string id) => await _cartService.EmptyCartAsync(id);
    }
}