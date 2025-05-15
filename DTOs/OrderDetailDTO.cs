namespace VtuberMerchHub.DTOs
{
    public class OrderDetailDTO
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public ProductDTO Product { get; set; }
    }
}