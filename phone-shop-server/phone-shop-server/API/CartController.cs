using Humanizer;
using Microsoft.AspNetCore.Mvc;
using phone_shop_server.Business.APIResponse;
using phone_shop_server.Business.DTO.Brand;
using phone_shop_server.Business.DTO.Cart;
using phone_shop_server.Business.DTO.Phone;
using phone_shop_server.Business.Service;

namespace phone_shop_server.API
{
    [ApiController]
    [Route("api/v{version:apiVersion}/cart")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {   
             return Ok(await _cartService.GetAllAsync(HttpContext));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CartCRUDDto dto)
        {
            return Ok(await _cartService.AddToCartAsync(HttpContext, dto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _cartService.RemoveFromCartAsync(id, HttpContext));
        }
    }
}
