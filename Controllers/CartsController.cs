using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VtuberMerchHub.Models;
using VtuberMerchHub.Services;

namespace VtuberMerchHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetCartByCustomer(int customerId)
        {
            var cart = await _cartService.GetCartByCustomerIdAsync(customerId);
            return Ok(cart);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("{cartId}/items")]
        public async Task<IActionResult> GetCartItems(int cartId)
        {
            var items = await _cartService.GetCartItemsByCartIdAsync(cartId);
            return Ok(items);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("add")]
        public async Task<IActionResult> AddItemToCart([FromBody] AddCartItemRequest request)
        {
            var cartItem = await _cartService.AddItemToCartAsync(request.CustomerId, request.ProductId, request.Quantity);
            return Ok(cartItem);
        }

        [Authorize(Roles = "Customer")]
        [HttpPut("item/{cartItemId}")]
        public async Task<IActionResult> UpdateCartItem(int cartItemId, [FromBody] UpdateCartItemRequest request)
        {
            var updatedItem = await _cartService.UpdateCartItemAsync(cartItemId, request.Quantity);
            return Ok(updatedItem);
        }

        [Authorize(Roles = "Customer")]
        [HttpDelete("item/{cartItemId}")]
        public async Task<IActionResult> RemoveItemFromCart(int cartItemId)
        {
            var result = await _cartService.RemoveItemFromCartAsync(cartItemId);
            return Ok(new { Success = result });
        }

        [Authorize(Roles = "Customer")]
        [HttpDelete("clear/{cartId}")]
        public async Task<IActionResult> ClearCart(int cartId)
        {
            var result = await _cartService.ClearCartAsync(cartId);
            return Ok(new { Success = result });
        }
    }
}