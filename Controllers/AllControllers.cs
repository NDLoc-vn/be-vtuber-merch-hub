using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VtuberMerchHub.Models;
using VtuberMerchHub.Services;

namespace VtuberMerchHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var user = await _userService.RegisterUserAsync(request.Email, request.Password, request.Role);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _userService.LoginUserAsync(request.Email, request.Password);
            return Ok(new { Token = token });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            await _userService.ForgotPasswordAsync(request.Email);
            return Ok(new { Message = "Mật khẩu mới đã được gửi (giả lập)" });
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromForm] UpdateUserRequest request)
        {
            var updatedUser = await _userService.UpdateUserAsync(id, request.Email, request.Avatar);
            return Ok(updatedUser);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            return Ok(new { Success = result });
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class VtubersController : ControllerBase
    {
        private readonly IVtuberService _vtuberService;

        public VtubersController(IVtuberService vtuberService)
        {
            _vtuberService = vtuberService;
        }

        // [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllVtubers()
        {
            var vtubers = await _vtuberService.GetAllVtubersAsync();
            return Ok(vtubers);
        }

        // [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVtuber(int id)
        {
            var vtuber = await _vtuberService.GetVtuberByIdAsync(id);
            return Ok(vtuber);
        }

        [Authorize(Roles = "Vtuber")]
        [HttpPost]
        public async Task<IActionResult> CreateVtuber([FromForm] CreateVtuberRequest request)
        {
            var createdVtuber = await _vtuberService.CreateVtuberAsync(
                request.UserId,
                request.VtuberName,
                request.RealName,
                request.DebutDate,
                request.Channel,
                request.Description,
                request.VtuberGender,
                request.SpeciesId,
                request.CompanyId,
                request.ModelFile
            );
            return Ok(createdVtuber);
        }

        [Authorize(Roles = "Vtuber")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVtuber(int id, [FromForm] UpdateVtuberRequest request)
        {
            var updatedVtuber = await _vtuberService.UpdateVtuberAsync(
                id,
                request.VtuberName,
                request.RealName,
                request.DebutDate,
                request.Channel,
                request.Description,
                request.VtuberGender,
                request.SpeciesId,
                request.CompanyId,
                request.ModelFile
            );
            return Ok(updatedVtuber);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVtuber(int id)
        {
            var result = await _vtuberService.DeleteVtuberAsync(id);
            return Ok(new { Success = result });
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        // [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return Ok(product);
        }

        // [Authorize]
        [HttpGet("merchandise/{merchandiseId}")]
        public async Task<IActionResult> GetProductsByMerchandise(int merchandiseId)
        {
            var products = await _productService.GetProductsByMerchandiseIdAsync(merchandiseId);
            return Ok(products);
        }

        [Authorize(Roles = "Vtuber")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductRequest request)
        {
            var createdProduct = await _productService.CreateProductAsync(
                request.MerchandiseId,
                request.ProductName,
                request.ImageFile,
                request.Price,
                request.Stock,
                request.Description,
                request.CategoryId
            );
            return Ok(createdProduct);
        }

        [Authorize(Roles = "Vtuber")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] UpdateProductRequest request)
        {
            var updatedProduct = await _productService.UpdateProductAsync(
                id,
                request.ProductName,
                request.ImageFile,
                request.Price,
                request.Stock,
                request.Description,
                request.CategoryId
            );
            return Ok(updatedProduct);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            return Ok(new { Success = result });
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class MerchandisesController : ControllerBase
    {
        private readonly IMerchandiseService _merchandiseService;

        public MerchandisesController(IMerchandiseService merchandiseService)
        {
            _merchandiseService = merchandiseService;
        }

        // [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllMerchandises()
        {
            var merchandises = await _merchandiseService.GetAllMerchandisesAsync();
            return Ok(merchandises);
        }

        // [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMerchandise(int id)
        {
            var merchandise = await _merchandiseService.GetMerchandiseByIdAsync(id);
            return Ok(merchandise);
        }

        [Authorize(Roles = "Vtuber")]
        [HttpPost]
        public async Task<IActionResult> CreateMerchandise([FromBody] CreateMerchandiseRequest merchandise)
        {
            var createdMerchandise = await _merchandiseService.CreateMerchandiseAsync(
                merchandise.VtuberId,
                merchandise.MerchandiseName,
                merchandise.ImageUrl,
                merchandise.StartDate,
                merchandise.EndDate,
                merchandise.Description
            );
            return Ok(createdMerchandise);
        }

        [Authorize(Roles = "Vtuber")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMerchandise(int id, [FromBody] UpdateMerchandiseRequest merchandise)
        {
            // var updatedMerchandise = await _merchandiseService.UpdateMerchandiseAsync(merchandise);
            var updatedMerchandise = await _merchandiseService.UpdateMerchandiseAsync(
                id,
                merchandise.MerchandiseName,
                merchandise.ImageUrl,
                merchandise.StartDate,
                merchandise.EndDate,
                merchandise.Description,
                merchandise.VtuberId
            );
            return Ok(updatedMerchandise);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMerchandise(int id)
        {
            var result = await _merchandiseService.DeleteMerchandiseAsync(id);
            return Ok(new { Success = result });
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            return Ok(order);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            var createdOrder = await _orderService.CreateOrderAsync(order);
            return Ok(createdOrder);
        }

        [Authorize(Roles = "Customer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order order)
        {
            order.OrderId = id;
            var updatedOrder = await _orderService.UpdateOrderAsync(order);
            return Ok(updatedOrder);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteOrderAsync(id);
            return Ok(new { Success = result });
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetOrdersByCustomer(int customerId)
        {
            var orders = await _orderService.GetOrdersByCustomerIdAsync(customerId);
            return Ok(orders);
        }
    }

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

    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ForgotPasswordRequest
    {
        public string Email { get; set; }
    }

    public class UpdateUserRequest
    {
        public string? Email { get; set; }
        public IFormFile? Avatar { get; set; }
    }

    public class CreateVtuberRequest
    {
        public int UserId { get; set; }
        public string VtuberName { get; set; }
        public string? RealName { get; set; }
        public DateTime? DebutDate { get; set; }
        public string? Channel { get; set; }
        public string? Description { get; set; }
        public int? VtuberGender { get; set; }
        public int? SpeciesId { get; set; }
        public int? CompanyId { get; set; }
        public IFormFile ModelFile { get; set; }
    }

    public class UpdateVtuberRequest
    {
        public string? VtuberName { get; set; }
        public string? RealName { get; set; }
        public DateTime? DebutDate { get; set; }
        public string? Channel { get; set; }
        public string? Description { get; set; }
        public int? VtuberGender { get; set; }
        public int? SpeciesId { get; set; }
        public int? CompanyId { get; set; }
        public IFormFile? ModelFile { get; set; }
    }

    public class CreateProductRequest
    {
        public int MerchandiseId { get; set; }
        public string ProductName { get; set; }
        public IFormFile ImageFile { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
    }

    public class UpdateProductRequest
    {
        public string? ProductName { get; set; }
        public IFormFile? ImageFile { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
    }

    public class CreateMerchandiseRequest
    {
        public string MerchandiseName { get; set; }
        public IFormFile ImageUrl { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Description { get; set; }
        public int VtuberId { get; set; }
    }

    public class UpdateMerchandiseRequest
    {
        public string? MerchandiseName { get; set; }
        public IFormFile? ImageUrl { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Description { get; set; }
        public int? VtuberId { get; set; }
    }

    public class AddCartItemRequest
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateCartItemRequest
    {
        public int Quantity { get; set; }
    }
}