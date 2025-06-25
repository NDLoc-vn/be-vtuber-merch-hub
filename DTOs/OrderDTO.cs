namespace VtuberMerchHub.DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; }
        public int Status { get; set; }
        public List<OrderDetailDTO> OrderItems { get; set; }
    }
}