namespace VtuberMerchHub.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; }
        public int Status { get; set; } // 0: Đã nhận đơn, 1: Đã chuẩn bị đơn, 2: Đang giao hàng,

        public Customer Customer { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}