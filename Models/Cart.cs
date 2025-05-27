namespace VtuberMerchHub.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }

        public Customer Customer { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}