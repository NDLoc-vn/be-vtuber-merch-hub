namespace VtuberMerchHub.Models
{
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
}