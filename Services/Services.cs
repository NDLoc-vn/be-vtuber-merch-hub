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
    // IUserService
    public interface IUserService
    {
        Task<User> RegisterUserAsync(string email, string password, string role);
        Task<string> LoginUserAsync(string email, string password);
        Task<bool> ForgotPasswordAsync(string email);
        Task<User> GetUserByIdAsync(int id);
        Task<List<User>> GetAllUsersAsync();
        Task<User> UpdateUserAsync(int id, string email, IFormFile avatar);
        Task<bool> DeleteUserAsync(int id);
    }

    // UserService
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly ICloudinaryService _cloudinaryService;

        public UserService(IUserRepository userRepository, IConfiguration configuration, ICloudinaryService cloudinaryService)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<User> RegisterUserAsync(string email, string password, string role)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(email);
            if (existingUser != null)
                throw new Exception("Người dùng đã tồn tại");

            var user = new User
            {
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                Role = role
            };
            return await _userRepository.CreateUserAsync(user);
        }

        public async Task<string> LoginUserAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
                throw new Exception("Email không tồn tại");

            Console.WriteLine($"Hashed Password: {user.Password}");

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                throw new Exception("Mật khẩu không đúng");

            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpiryInMinutes"])),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> ForgotPasswordAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
                throw new Exception("Email không tồn tại");

            string newPassword = Guid.NewGuid().ToString().Substring(0, 8);
            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _userRepository.UpdateUserAsync(user);
            return true;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id) ?? throw new Exception("Người dùng không tìm thấy");
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User> UpdateUserAsync(int id, string email, IFormFile avatar)
        {
            var user = await _userRepository.GetUserByIdAsync(id) ?? throw new Exception("Người dùng không tìm thấy");
            user.Email = email ?? user.Email;
            if (avatar != null)
            {
                user.AvatarUrl = await _cloudinaryService.UploadImageAsync(avatar);
            }
            return await _userRepository.UpdateUserAsync(user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }
    }

    // IVtuberService
    public interface IVtuberService
    {
        Task<VtuberDTO> GetVtuberByIdAsync(int id);
        Task<List<VtuberDTO>> GetAllVtubersAsync();
        Task<Vtuber> CreateVtuberAsync(int userId, string vtuberName, string realName, DateTime? debutDate, string channel, string description, int? vtuberGender, int? speciesId, int? companyId, IFormFile modelFile);
        Task<Vtuber> UpdateVtuberAsync(int id, string vtuberName, string realName, DateTime? debutDate, string channel, string description, int? vtuberGender, int? speciesId, int? companyId, IFormFile modelFile);
        Task<bool> DeleteVtuberAsync(int id);
    }

    // VtuberService
    public class VtuberService : IVtuberService
    {
        private readonly IVtuberRepository _vtuberRepository;
        private readonly ICloudinaryService _cloudinaryService;

        public VtuberService(IVtuberRepository vtuberRepository, ICloudinaryService cloudinaryService)
        {
            _vtuberRepository = vtuberRepository;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<VtuberDTO> GetVtuberByIdAsync(int id)
        {
            return await _vtuberRepository.GetVtuberByIdAsync(id) ?? throw new Exception("Vtuber không tìm thấy");
        }

        public async Task<List<VtuberDTO>> GetAllVtubersAsync()
        {
            return await _vtuberRepository.GetAllVtubersAsync();
        }

        public async Task<Vtuber> CreateVtuberAsync(int userId, string vtuberName, string realName, DateTime? debutDate, string channel, string description, int? vtuberGender, int? speciesId, int? companyId, IFormFile modelFile)
        {
            var vtuber = new Vtuber
            {
                UserId = userId,
                VtuberName = vtuberName,
                RealName = realName,
                DebutDate = debutDate,
                Channel = channel,
                Description = description,
                VtuberGender = vtuberGender,
                SpeciesId = speciesId,
                CompanyId = companyId,
                ModelUrl = await _cloudinaryService.UploadImageAsync(modelFile)
            };
            return await _vtuberRepository.CreateVtuberAsync(vtuber);
        }


        // This is shit code
        public async Task<Vtuber> UpdateVtuberAsync(int id, string vtuberName, string realName, DateTime? debutDate, string channel, string description, int? vtuberGender, int? speciesId, int? companyId, IFormFile modelFile)
        {
            var vtuber = await _vtuberRepository.GetVtuberByIdAsync(id) ?? throw new Exception("Vtuber không tìm thấy");
            var vtuberUpdate = new Vtuber
            {
                VtuberId = id,
                VtuberName = vtuberName,
                RealName = realName,
                DebutDate = debutDate,
                Channel = channel,
                Description = description,
                VtuberGender = vtuberGender,
                SpeciesId = speciesId,
                CompanyId = companyId
            };
            vtuber.VtuberName = vtuberName ?? vtuber.VtuberName;
            vtuber.RealName = realName ?? vtuber.RealName;
            vtuber.DebutDate = debutDate ?? vtuber.DebutDate;
            vtuber.Channel = channel ?? vtuber.Channel;
            vtuber.Description = description ?? vtuber.Description;
            vtuber.VtuberGender = vtuberGender ?? vtuber.VtuberGender;
            vtuber.SpeciesId = speciesId ?? vtuber.SpeciesId;
            vtuber.CompanyId = companyId ?? vtuber.CompanyId;
            if (modelFile != null)
            {
                vtuber.ModelUrl = await _cloudinaryService.UploadImageAsync(modelFile);
            }
            return await _vtuberRepository.UpdateVtuberAsync(vtuberUpdate);
        }

        public async Task<bool> DeleteVtuberAsync(int id)
        {
            return await _vtuberRepository.DeleteVtuberAsync(id);
        }
    }

    // IMerchandiseService
    public interface IMerchandiseService
    {
        Task<Merchandise> GetMerchandiseByIdAsync(int id);
        Task<List<Merchandise>> GetAllMerchandisesAsync();
        Task<Merchandise> CreateMerchandiseAsync(int vtuberId, string merchandiseName, IFormFile imageUrl, DateTime? startDate, DateTime? endDate, string? description);
        Task<Merchandise> UpdateMerchandiseAsync(int merchandiseId, string merchandiseName, IFormFile imageUrl, DateTime? startDate, DateTime? endDate, string newDescription, int? vtuberId);
        Task<bool> DeleteMerchandiseAsync(int id);
    }

    // MerchandiseService
    public class MerchandiseService : IMerchandiseService
    {
        private readonly IMerchandiseRepository _merchandiseRepository;
        private readonly ICloudinaryService _cloudinaryService;

        public MerchandiseService(IMerchandiseRepository merchandiseRepository, ICloudinaryService cloudinaryService)
        {
            _merchandiseRepository = merchandiseRepository;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<Merchandise> GetMerchandiseByIdAsync(int id)
        {
            return await _merchandiseRepository.GetMerchandiseByIdAsync(id) ?? throw new Exception("Merchandise không tìm thấy");
        }

        public async Task<List<Merchandise>> GetAllMerchandisesAsync()
        {
            return await _merchandiseRepository.GetAllMerchandisesAsync();
        }

        public async Task<Merchandise> CreateMerchandiseAsync(int vtuberId, string merchandiseName, IFormFile imageUrl, DateTime? startDate, DateTime? endDate, string? description)
        {
            var merchandise = new Merchandise
            {
                VtuberId = vtuberId,
                MerchandiseName = merchandiseName,
                ImageUrl = await _cloudinaryService.UploadImageAsync(imageUrl),
                StartDate = startDate,
                EndDate = endDate,
                Description = description
            };
            return await _merchandiseRepository.CreateMerchandiseAsync(merchandise);
        }

        public async Task<Merchandise> UpdateMerchandiseAsync(int merchandiseId, string merchandiseName, IFormFile imageUrl, DateTime? startDate, DateTime? endDate, string newDescription, int? vtuberId)
        {
            var merchandise = await _merchandiseRepository.GetMerchandiseByIdAsync(merchandiseId) ?? throw new Exception("Merchandise không tìm thấy");
            merchandise.MerchandiseName = merchandiseName ?? merchandise.MerchandiseName;
            merchandise.StartDate = startDate != default ? startDate : merchandise.StartDate;
            merchandise.EndDate = endDate != default ? endDate : merchandise.EndDate;
            merchandise.Description = newDescription ?? merchandise.Description;
            merchandise.VtuberId = vtuberId ?? merchandise.VtuberId;
            if (imageUrl != null)
            {
                merchandise.ImageUrl = await _cloudinaryService.UploadImageAsync(imageUrl);
            }
            return await _merchandiseRepository.UpdateMerchandiseAsync(merchandise);
        }

        public async Task<bool> DeleteMerchandiseAsync(int id)
        {
            return await _merchandiseRepository.DeleteMerchandiseAsync(id);
        }
    }

    // IOrderService
    public interface IOrderService
    {
        Task<Order> GetOrderByIdAsync(int id);
        Task<List<Order>> GetAllOrdersAsync();
        Task<List<Order>> GetOrdersByCustomerIdAsync(int customerId);
        Task<Order> CreateOrderAsync(Order order);
        Task<Order> UpdateOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(int id);
    }

    // OrderService
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetOrderByIdAsync(id) ?? throw new Exception("Đơn hàng không tìm thấy");
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }

        public async Task<List<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await _orderRepository.GetOrdersByCustomerIdAsync(customerId);
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            return await _orderRepository.CreateOrderAsync(order);
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            return await _orderRepository.UpdateOrderAsync(order);
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            return await _orderRepository.DeleteOrderAsync(id);
        }
    }

    // ICartService
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

    // IProductService
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<List<Product>> GetAllProductsAsync();
        Task<List<Product>> GetProductsByMerchandiseIdAsync(int merchandiseId);
        Task<Product> CreateProductAsync(int merchandiseId, string productName, IFormFile imageFile, decimal price, int stock, string description, int? categoryId);
        Task<Product> UpdateProductAsync(int id, string productName, IFormFile imageFile, decimal price, int stock, string description, int? categoryId);
        Task<bool> DeleteProductAsync(int id);
    }

    // ProductService
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICloudinaryService _cloudinaryService;

        public ProductService(IProductRepository productRepository, ICloudinaryService cloudinaryService)
        {
            _productRepository = productRepository;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetProductByIdAsync(id) ?? throw new Exception("Sản phẩm không tìm thấy");
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<List<Product>> GetProductsByMerchandiseIdAsync(int merchandiseId)
        {
            return await _productRepository.GetProductsByMerchandiseIdAsync(merchandiseId);
        }

        public async Task<Product> CreateProductAsync(int merchandiseId, string productName, IFormFile imageFile, decimal price, int stock, string description, int? categoryId)
        {
            var product = new Product
            {
                MerchandiseId = merchandiseId,
                ProductName = productName,
                ImageUrl = await _cloudinaryService.UploadImageAsync(imageFile),
                Price = price,
                Stock = stock,
                Description = description,
                CategoryId = categoryId
            };
            return await _productRepository.CreateProductAsync(product);
        }

        public async Task<Product> UpdateProductAsync(int id, string productName, IFormFile imageFile, decimal price, int stock, string description, int? categoryId)
        {
            var product = await _productRepository.GetProductByIdAsync(id) ?? throw new Exception("Sản phẩm không tìm thấy");
            product.ProductName = productName ?? product.ProductName;
            product.Price = price != 0 ? price : product.Price;
            product.Stock = stock != 0 ? stock : product.Stock;
            product.Description = description ?? product.Description;
            product.CategoryId = categoryId ?? product.CategoryId;
            if (imageFile != null)
            {
                product.ImageUrl = await _cloudinaryService.UploadImageAsync(imageFile);
            }
            return await _productRepository.UpdateProductAsync(product);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            return await _productRepository.DeleteProductAsync(id);
        }
    }
}