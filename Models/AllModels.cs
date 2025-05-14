namespace VtuberMerchHub.Models
{
    // User
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? AvatarUrl { get; set; }

        public Admin Admin { get; set; }
        public Customer Customer { get; set; }
        public Vtuber Vtuber { get; set; }
    }

    // Admin
    public class Admin
    {
        public int AdminId { get; set; }
        public int UserId { get; set; }
        public string AdminName { get; set; }
        public User User { get; set; }
    }

    // Customer
    public class Customer
    {
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string? Nickname { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? GenderId { get; set; }

        public User User { get; set; }
        public Gender Gender { get; set; }
        public List<Cart> Carts { get; set; }
        public List<Order> Orders { get; set; }
    }

    // Gender
    public class Gender
    {
        public int GenderId { get; set; }
        public string GenderType { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Vtuber> Vtubers { get; set; }
    }

    // Vtuber
    public class Vtuber
    {
        public int VtuberId { get; set; }
        public int UserId { get; set; }
        public string VtuberName { get; set; }
        public string? RealName { get; set; }
        public DateTime? DebutDate { get; set; }
        public string Channel { get; set; }
        public string? Description { get; set; }
        public int? VtuberGender { get; set; }
        public int? SpeciesId { get; set; }
        public int? CompanyId { get; set; }
        public string ModelUrl { get; set; }

        public User User { get; set; }
        public Gender Gender { get; set; }
        public Species Species { get; set; }
        public Company Company { get; set; }
        public List<Event> Events { get; set; }
        public List<Merchandise> Merchandises { get; set; }
    }

    // Species
    public class Species
    {
        public int SpeciesId { get; set; }
        public string SpeciesName { get; set; }
        public string? Description { get; set; }
        public List<Vtuber> Vtubers { get; set; }
    }

    // Companies
    public class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string? Address { get; set; }
        public string? ContactEmail { get; set; }
        public List<Vtuber> Vtubers { get; set; }
    }

    // Events
    public class Event
    {
        public int EventId { get; set; }
        public int VtuberId { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }

        public Vtuber Vtuber { get; set; }
    }

    // Categories
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string? Description { get; set; }
        public List<Product> Products { get; set; }
    }

    // Merchandises
    public class Merchandise
    {
        public int MerchandiseId { get; set; }
        public int VtuberId { get; set; }
        public string MerchandiseName { get; set; }
        public string? Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Vtuber Vtuber { get; set; }
        public List<Product> Products { get; set; }
    }

    // Products
    public class Product
    {
        public int ProductId { get; set; }
        public int MerchandiseId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int? Stock { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }

        public Merchandise Merchandise { get; set; }
        public Category Category { get; set; }
        public List<CartItem> CartItems { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }

    // Carts
    public class Cart
    {
        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }

        public Customer Customer { get; set; }
        public List<CartItem> CartItems { get; set; }
    }

    // Cart_Items
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }

    // Orders
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; }

        public Customer Customer { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }

    // Order_Details
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}