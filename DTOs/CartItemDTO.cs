namespace VtuberMerchHub.DTOs
{
    public class CartItemDTO
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public ProductDTO Product { get; set; }
    }
}