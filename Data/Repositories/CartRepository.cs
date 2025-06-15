using Microsoft.EntityFrameworkCore;
using VtuberMerchHub.DTOs;
using VtuberMerchHub.Models;

namespace VtuberMerchHub.Data
{
    public interface ICartRepository
    {
        Task<CartDTO?> GetCartByCustomerIdAsync(int customerId);
        Task<CartDTO> CreateCartAsync(Cart cart);
        Task<CartItemDTO> AddItemToCartAsync(CartItem cartItem);
        Task<CartItemDTO> UpdateCartItemAsync(CartItem cartItem);
        Task<bool> RemoveItemFromCartAsync(int cartItemId);
        Task<bool> ClearCartAsync(int cartId);
    }

    public class CartRepository : ICartRepository
{
    private readonly VtuberMerchHubDbContext _context;

    public CartRepository(VtuberMerchHubDbContext context)
    {
        _context = context;
    }

    public async Task<CartDTO?> GetCartByCustomerIdAsync(int customerId)
    {
        var cart = await _context.Carts
            .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                    .ThenInclude(p => p.Category)
            .FirstOrDefaultAsync(c => c.CustomerId == customerId);

        return cart == null ? null : MapCartToDTO(cart);
    }

    public async Task<CartDTO> CreateCartAsync(Cart cart)
    {
        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();
        return MapCartToDTO(cart);
    }

    public async Task<CartItemDTO> AddItemToCartAsync(CartItem cartItem)
    {
        _context.CartItems.Add(cartItem);
        await _context.SaveChangesAsync();

        var itemWithProduct = await _context.CartItems
            .Include(ci => ci.Product)
                .ThenInclude(p => p.Category)
            .FirstOrDefaultAsync(ci => ci.CartItemId == cartItem.CartItemId);

        return MapCartItemToDTO(itemWithProduct!);
    }

    public async Task<CartItemDTO> UpdateCartItemAsync(CartItem cartItem)
    {
        _context.CartItems.Update(cartItem);
        await _context.SaveChangesAsync();

        var updatedItem = await _context.CartItems
            .Include(ci => ci.Product)
                .ThenInclude(p => p.Category)
            .FirstOrDefaultAsync(ci => ci.CartItemId == cartItem.CartItemId);

        return MapCartItemToDTO(updatedItem!);
    }

    public async Task<bool> RemoveItemFromCartAsync(int cartItemId)
    {
        var cartItem = await _context.CartItems.FindAsync(cartItemId);
        if (cartItem == null) return false;

        _context.CartItems.Remove(cartItem);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ClearCartAsync(int cartId)
    {
        var cartItems = await _context.CartItems
            .Where(ci => ci.CartId == cartId)
            .ToListAsync();

        _context.CartItems.RemoveRange(cartItems);
        await _context.SaveChangesAsync();
        return true;
    }

    // Mapping functions
    private static CartDTO MapCartToDTO(Cart cart)
    {
        return new CartDTO
        {
            CartId = cart.CartId,
            CustomerId = cart.CustomerId,
            CreatedAt = cart.CreatedAt,
            CartItems = cart.CartItems?.Select(MapCartItemToDTO).ToList() ?? new List<CartItemDTO>()
        };
    }

    private static CartItemDTO MapCartItemToDTO(CartItem cartItem)
    {
        return new CartItemDTO
        {
            CartItemId = cartItem.CartItemId,
            CartId = cartItem.CartId,
            ProductId = cartItem.ProductId,
            Quantity = cartItem.Quantity,
            Product = new ProductDTO
            {
                ProductId = cartItem.Product.ProductId,
                ProductName = cartItem.Product.ProductName,
                Description = cartItem.Product.Description,
                Price = cartItem.Product.Price,
                ImageUrl = cartItem.Product.ImageUrl,
                CategoryId = cartItem.Product.CategoryId,
                Category = cartItem.Product.Category == null ? null : new CategoryDTO
                {
                    CategoryId = cartItem.Product.Category.CategoryId,
                    CategoryName = cartItem.Product.Category.CategoryName
                }
            }
        };
    }
}

}
