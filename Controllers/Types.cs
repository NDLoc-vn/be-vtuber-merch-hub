using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VtuberMerchHub.Models;
using VtuberMerchHub.Services;

namespace VtuberMerchHub.Controllers
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
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

    public class UpdateCustomerRequest
    {
        public IFormFile? Avatar { get; set; }
        public string? FullName { get; set; }
        public string? Nickname { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? GenderId { get; set; }
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
        public int? Stock { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
    }

    public class UpdateProductRequest
    {
        public string? ProductName { get; set; }
        public IFormFile? ImageFile { get; set; }
        public decimal Price { get; set; }
        public int? Stock { get; set; }
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

    // OrderCreateDTO: dữ liệu KH gửi lên khi checkout
    public class OrderCreateDTO
    {
        public int CustomerId { get; set; }
        public string ShippingAddress { get; set; } = string.Empty;
        public List<OrderItemCreateDTO> Items { get; set; } = new();
    }

    public class OrderItemCreateDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    /* ----- DTO trả về cho client ----- */
    public class OrderReadDTO       // (giống OrderDTO bạn đã có nhưng KHÔNG có navigation object)
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public CustomerBriefDTO Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; } = string.Empty;
        public List<OrderItemReadDTO> Items { get; set; } = new();
    }

    public class CustomerBriefDTO
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string Nickname { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class OrderItemReadDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}