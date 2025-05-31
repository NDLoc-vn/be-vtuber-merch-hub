using VtuberMerchHub.Data;
using VtuberMerchHub.DTOs;
using VtuberMerchHub.Models;
using Microsoft.EntityFrameworkCore;

namespace VtuberMerchHub.Services
{
    public interface ICartService
    {
        Task<CartDTO> GetCartByCustomerIdAsync(int customerId);
        Task<CartItemDTO> AddItemToCartAsync(int customerId, int productId, int quantity);
        Task<CartItemDTO> UpdateCartItemAsync(int cartItemId, int quantity);
        Task<bool> RemoveItemFromCartAsync(int cartItemId);
        Task<bool> ClearCartAsync(int cartId);
    }

    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly VtuberMerchHubDbContext _context;

        public CartService(ICartRepository cartRepository, VtuberMerchHubDbContext context)
        {
            _cartRepository = cartRepository;
            _context = context;
        }

        public async Task<CartDTO> GetCartByCustomerIdAsync(int customerId)
        {
            var cartDto = await _cartRepository.GetCartByCustomerIdAsync(customerId);
            if (cartDto == null)
            {
                var newCart = new Cart
                {
                    CustomerId = customerId,
                    CreatedAt = DateTime.UtcNow
                };
                await _cartRepository.CreateCartAsync(newCart);
                return await _cartRepository.GetCartByCustomerIdAsync(customerId);
            }
            return cartDto;
        }

        public async Task<CartItemDTO> AddItemToCartAsync(int customerId, int productId, int quantity)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (cart == null)
            {
                cart = new Cart
                {
                    CustomerId = customerId,
                    CreatedAt = DateTime.UtcNow
                };
                await _context.Carts.AddAsync(cart);
                await _context.SaveChangesAsync();
            }

            var existingItem = cart.CartItems?.FirstOrDefault(ci => ci.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                return await _cartRepository.UpdateCartItemAsync(existingItem);
            }

            var newItem = new CartItem
            {
                CartId = cart.CartId,
                ProductId = productId,
                Quantity = quantity
            };
            return await _cartRepository.AddItemToCartAsync(newItem);
        }

        public async Task<CartItemDTO> UpdateCartItemAsync(int cartItemId, int quantity)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem == null)
                throw new Exception("Cart item not found.");

            cartItem.Quantity = quantity;
            return await _cartRepository.UpdateCartItemAsync(cartItem);
        }

        public async Task<bool> RemoveItemFromCartAsync(int cartItemId)
        {
            return await _cartRepository.RemoveItemFromCartAsync(cartItemId);
        }

        public async Task<bool> ClearCartAsync(int cartId)
        {
            return await _cartRepository.ClearCartAsync(cartId);
        }
    }
}