namespace VtuberMerchHub.DTOs
{
    public class CartDTO
    {
        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<CartItemDTO> CartItems { get; set; }
    }
}