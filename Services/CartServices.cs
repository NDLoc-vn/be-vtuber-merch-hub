using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VtuberMerchHub.Data;
using VtuberMerchHub.Models;
using Microsoft.AspNetCore.Http;
using VtuberMerchHub.DTOs;

namespace VtuberMerchHub.Services
{
    public interface ICartService
    {
        Task<Cart> GetCartByCustomerIdAsync(int customerId);
        Task<List<CartItem>> GetCartItemsByCartIdAsync(int cartId);
        Task<Cart> CreateCartAsync(int customerId);
        Task<CartItem> AddItemToCartAsync(int customerId, int productId, int quantity);
        Task<CartItem> UpdateCartItemAsync(int cartItemId, int quantity);
        Task<bool> RemoveItemFromCartAsync(int cartItemId);
        Task<bool> ClearCartAsync(int cartId);
    }

    // CartService
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly VtuberMerchHubDbContext _context;

        public CartService(ICartRepository cartRepository, VtuberMerchHubDbContext context)
        {
            _cartRepository = cartRepository;
            _context = context;
        }

        public async Task<Cart> GetCartByCustomerIdAsync(int customerId)
        {
            var cart = await _cartRepository.GetCartByCustomerIdAsync(customerId);
            if (cart == null)
            {
                cart = await CreateCartAsync(customerId);
            }
            return cart;
        }

        public async Task<List<CartItem>> GetCartItemsByCartIdAsync(int cartId)
        {
            return await _cartRepository.GetCartItemsByCartIdAsync(cartId);
        }

        public async Task<Cart> CreateCartAsync(int customerId)
        {
            var cart = new Cart
            {
                CustomerId = customerId,
                CreatedAt = DateTime.UtcNow
            };
            return await _cartRepository.CreateCartAsync(cart);
        }

        public async Task<CartItem> AddItemToCartAsync(int customerId, int productId, int quantity)
        {
            var cart = await GetCartByCustomerIdAsync(customerId);
            var existingItem = cart.CartItems?.FirstOrDefault(ci => ci.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                return await _cartRepository.UpdateCartItemAsync(existingItem);
            }

            var cartItem = new CartItem
            {
                CartId = cart.CartId,
                ProductId = productId,
                Quantity = quantity
            };
            return await _cartRepository.AddItemToCartAsync(cartItem);
        }

        public async Task<CartItem> UpdateCartItemAsync(int cartItemId, int quantity)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem == null)
                throw new Exception("Sản phẩm trong giỏ hàng không tìm thấy");

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