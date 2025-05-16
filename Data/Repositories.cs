using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using VtuberMerchHub.DTOs;
using VtuberMerchHub.Models;

namespace VtuberMerchHub.Data
{
    // IUserRepository
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByIdAsync(int id);
        Task<List<User>> GetAllUsersAsync();
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
    }

    // UserRepository
    public class UserRepository : IUserRepository
    {
        private readonly VtuberMerchHubDbContext _context;

        public UserRepository(VtuberMerchHubDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }

    // IVtuberRepository
    public interface IVtuberRepository
    {
        Task<VtuberDTO> GetVtuberByIdAsync(int id);
        Task<List<VtuberDTO>> GetAllVtubersAsync();
        Task<Vtuber> CreateVtuberAsync(Vtuber vtuber);
        Task<Vtuber> UpdateVtuberAsync(Vtuber vtuber);
        Task<bool> DeleteVtuberAsync(int id);
    }

    // VtuberRepository
    public class VtuberRepository : IVtuberRepository
    {
        private readonly VtuberMerchHubDbContext _context;

        public VtuberRepository(VtuberMerchHubDbContext context)
        {
            _context = context;
        }

        public async Task<VtuberDTO> GetVtuberByIdAsync(int id)
        {
            var merchandise = await _context.Vtubers
                .Include(v => v.User)
                .Include(v => v.Gender)
                .Include(v => v.Species)
                .Include(v => v.Company)
                .Include(v => v.Merchandises)
                    .ThenInclude(m => m.Products)
                        .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(v => v.VtuberId == id);
            if (merchandise == null) return null;

            var dto = new VtuberDTO
            {
                VtuberId = merchandise.VtuberId,
                UserId = merchandise.UserId,
                VtuberName = merchandise.VtuberName,
                RealName = merchandise.RealName,
                DebutDate = merchandise.DebutDate,
                Channel = merchandise.Channel,
                Description = merchandise.Description,
                VtuberGender = merchandise.VtuberGender,
                SpeciesId = merchandise.SpeciesId,
                CompanyId = merchandise.CompanyId,
                ModelUrl = merchandise.ModelUrl,
                User = merchandise.User == null ? null : new UserDTO
                {
                    UserId = merchandise.User.UserId,
                    Email = merchandise.User.Email,
                    Role = merchandise.User.Role,
                    CreatedAt = merchandise.User.CreatedAt,
                    UpdatedAt = merchandise.User.UpdatedAt,
                    AvatarUrl = merchandise.User.AvatarUrl
                },
                Gender = merchandise.Gender == null ? null : new GenderDTO
                {
                    GenderId = merchandise.Gender.GenderId,
                    GenderType = merchandise.Gender.GenderType
                },
                Species = merchandise.Species == null ? null : new SpeciesDTO
                {
                    SpeciesId = merchandise.Species.SpeciesId,
                    SpeciesName = merchandise.Species.SpeciesName,
                    Description = merchandise.Species.Description
                },
                Company = merchandise.Company == null ? null : new CompanyDTO
                {
                    CompanyId = merchandise.Company.CompanyId,
                    CompanyName = merchandise.Company.CompanyName,
                    Address = merchandise.Company.Address,
                    ContactEmail = merchandise.Company.ContactEmail
                },
                Merchandises = merchandise.Merchandises?.Select(m => new MerchandiseDTO
                {
                    MerchandiseId = m.MerchandiseId,
                    VtuberId = m.VtuberId,
                    MerchandiseName = m.MerchandiseName,
                    Description = m.Description,
                    ImageUrl = m.ImageUrl,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    Products = m.Products?.Select(p => new ProductDTO
                    {
                        ProductId = p.ProductId,
                        MerchandiseId = p.MerchandiseId,
                        ProductName = p.ProductName,
                        Price = p.Price,
                        Stock = p.Stock,
                        ImageUrl = p.ImageUrl,
                        Description = p.Description,
                        CategoryId = p.CategoryId,
                        Category = p.Category == null ? null : new CategoryDTO
                        {
                            CategoryId = p.Category.CategoryId,
                            CategoryName = p.Category.CategoryName,
                            Description = p.Category.Description
                        }
                    }).ToList() ?? new List<ProductDTO>()
                }).ToList() ?? new List<MerchandiseDTO>()
            };
            return dto;
        }

        public async Task<List<VtuberDTO>> GetAllVtubersAsync()
        {
            var vtuberEntities = await _context.Vtubers
                .Include(v => v.User)
                .Include(v => v.Gender)
                .Include(v => v.Species)
                .Include(v => v.Company)
                .Include(v => v.Merchandises)
                    .ThenInclude(m => m.Products)
                        .ThenInclude(p => p.Category)
                .ToListAsync();

            return vtuberEntities.Select(v => new VtuberDTO
            {
                VtuberId = v.VtuberId,
                UserId = v.UserId,
                VtuberName = v.VtuberName,
                RealName = v.RealName,
                DebutDate = v.DebutDate,
                Channel = v.Channel,
                Description = v.Description,
                VtuberGender = v.VtuberGender,
                SpeciesId = v.SpeciesId,
                CompanyId = v.CompanyId,
                ModelUrl = v.ModelUrl,
                User = v.User == null ? null : new UserDTO
                {
                    UserId = v.User.UserId,
                    Email = v.User.Email,
                    Role = v.User.Role,
                    CreatedAt = v.User.CreatedAt,
                    UpdatedAt = v.User.UpdatedAt,
                    AvatarUrl = v.User.AvatarUrl
                },
                Gender = v.Gender == null ? null : new GenderDTO
                {
                    GenderId = v.Gender.GenderId,
                    GenderType = v.Gender.GenderType
                },
                Species = v.Species == null ? null : new SpeciesDTO
                {
                    SpeciesId = v.Species.SpeciesId,
                    SpeciesName = v.Species.SpeciesName,
                    Description = v.Species.Description
                },
                Company = v.Company == null ? null : new CompanyDTO
                {
                    CompanyId = v.Company.CompanyId,
                    CompanyName = v.Company.CompanyName,
                    Address = v.Company.Address,
                    ContactEmail = v.Company.ContactEmail
                },
                Merchandises = v.Merchandises?.Select(m => new MerchandiseDTO
                {
                    MerchandiseId = m.MerchandiseId,
                    VtuberId = m.VtuberId,
                    MerchandiseName = m.MerchandiseName,
                    Description = m.Description,
                    ImageUrl = m.ImageUrl,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    Products = m.Products?.Select(p => new ProductDTO
                    {
                        ProductId = p.ProductId,
                        MerchandiseId = p.MerchandiseId,
                        ProductName = p.ProductName,
                        Price = p.Price,
                        Stock = p.Stock,
                        ImageUrl = p.ImageUrl,
                        Description = p.Description,
                        CategoryId = p.CategoryId,
                        Category = p.Category == null ? null : new CategoryDTO
                        {
                            CategoryId = p.Category.CategoryId,
                            CategoryName = p.Category.CategoryName,
                            Description = p.Category.Description
                        }
                    }).ToList() ?? new List<ProductDTO>()
                }).ToList() ?? new List<MerchandiseDTO>()
            }).ToList();
        }

        public async Task<Vtuber> CreateVtuberAsync(Vtuber vtuber)
        {
            _context.Vtubers.Add(vtuber);
            await _context.SaveChangesAsync();
            return vtuber;
        }

        public async Task<Vtuber> UpdateVtuberAsync(Vtuber vtuber)
        {
            _context.Vtubers.Update(vtuber);
            await _context.SaveChangesAsync();
            return vtuber;
        }

        public async Task<bool> DeleteVtuberAsync(int id)
        {
            var vtuber = await _context.Vtubers.FindAsync(id);
            if (vtuber == null) return false;
            _context.Vtubers.Remove(vtuber);
            await _context.SaveChangesAsync();
            return true;
        }
    }

    // IMerchandiseRepository
    public interface IMerchandiseRepository
    {
        Task<Merchandise> GetMerchandiseByIdAsync(int id);
        Task<List<Merchandise>> GetAllMerchandisesAsync();
        Task<Merchandise> CreateMerchandiseAsync(Merchandise merchandise);
        Task<Merchandise> UpdateMerchandiseAsync(Merchandise merchandise);
        Task<bool> DeleteMerchandiseAsync(int id);
    }

    // MerchandiseRepository
    public class MerchandiseRepository : IMerchandiseRepository
    {
        private readonly VtuberMerchHubDbContext _context;

        public MerchandiseRepository(VtuberMerchHubDbContext context)
        {
            _context = context;
        }

        public async Task<Merchandise> GetMerchandiseByIdAsync(int id)
        {
            return await _context.Merchandises
                .Include(m => m.Vtuber)
                .Include(m => m.Products)
                .FirstOrDefaultAsync(m => m.MerchandiseId == id);
        }

        public async Task<List<Merchandise>> GetAllMerchandisesAsync()
        {
            return await _context.Merchandises.ToListAsync();
        }

        public async Task<Merchandise> CreateMerchandiseAsync(Merchandise merchandise)
        {
            _context.Merchandises.Add(merchandise);
            await _context.SaveChangesAsync();
            return merchandise;
        }

        public async Task<Merchandise> UpdateMerchandiseAsync(Merchandise merchandise)
        {
            _context.Merchandises.Update(merchandise);
            await _context.SaveChangesAsync();
            return merchandise;
        }

        public async Task<bool> DeleteMerchandiseAsync(int id)
        {
            var merchandise = await _context.Merchandises.FindAsync(id);
            if (merchandise == null) return false;
            _context.Merchandises.Remove(merchandise);
            await _context.SaveChangesAsync();
            return true;
        }
    }

    // IOrderRepository
    public interface IOrderRepository
    {
        Task<Order> GetOrderByIdAsync(int id);
        Task<List<Order>> GetAllOrdersAsync();
        Task<List<Order>> GetOrdersByCustomerIdAsync(int customerId);
        Task<Order> CreateOrderAsync(Order order);
        Task<Order> UpdateOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(int id);
    }

    // OrderRepository
    public class OrderRepository : IOrderRepository
    {
        private readonly VtuberMerchHubDbContext _context;

        public OrderRepository(VtuberMerchHubDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<List<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await _context.Orders
                .Where(o => o.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return false;
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }

    // ICartRepository
    public interface ICartRepository
    {
        Task<Cart> GetCartByIdAsync(int id);
        Task<Cart> GetCartByCustomerIdAsync(int customerId);
        Task<List<CartItem>> GetCartItemsByCartIdAsync(int cartId);
        Task<Cart> CreateCartAsync(Cart cart);
        Task<CartItem> AddItemToCartAsync(CartItem cartItem);
        Task<CartItem> UpdateCartItemAsync(CartItem cartItem);
        Task<bool> RemoveItemFromCartAsync(int cartItemId);
        Task<bool> ClearCartAsync(int cartId);
    }

    // CartRepository
    public class CartRepository : ICartRepository
    {
        private readonly VtuberMerchHubDbContext _context;

        public CartRepository(VtuberMerchHubDbContext context)
        {
            _context = context;
        }

        public async Task<Cart> GetCartByIdAsync(int id)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.CartId == id);
        }

        public async Task<Cart> GetCartByCustomerIdAsync(int customerId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }

        public async Task<List<CartItem>> GetCartItemsByCartIdAsync(int cartId)
        {
            return await _context.CartItems
                .Include(ci => ci.Product)
                .Where(ci => ci.CartId == cartId)
                .ToListAsync();
        }

        public async Task<Cart> CreateCartAsync(Cart cart)
        {
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<CartItem> AddItemToCartAsync(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }

        public async Task<CartItem> UpdateCartItemAsync(CartItem cartItem)
        {
            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
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
            var cartItems = await _context.CartItems.Where(ci => ci.CartId == cartId).ToListAsync();
            if (!cartItems.Any()) return false;
            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
            return true;
        }
    }

    // IProductRepository
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<List<Product>> GetAllProductsAsync();
        Task<List<Product>> GetProductsByMerchandiseIdAsync(int merchandiseId);
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
    }

    // ProductRepository
    public class ProductRepository : IProductRepository
    {
        private readonly VtuberMerchHubDbContext _context;

        public ProductRepository(VtuberMerchHubDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<List<Product>> GetProductsByMerchandiseIdAsync(int merchandiseId)
        {
            return await _context.Products
                .Where(p => p.MerchandiseId == merchandiseId)
                .ToListAsync();
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}