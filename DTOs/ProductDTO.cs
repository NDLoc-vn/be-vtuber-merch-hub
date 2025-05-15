namespace VtuberMerchHub.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public int MerchandiseId { get; set; }
        public string ProductName { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int? Stock { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }

        public CategoryDTO Category { get; set; }
    }
}