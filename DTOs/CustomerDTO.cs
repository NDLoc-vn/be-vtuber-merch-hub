namespace VtuberMerchHub.DTOs
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string? Nickname { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? GenderId { get; set; }

        public UserDTO User { get; set; }
        public GenderDTO Gender { get; set; }
        public List<CartDTO> Carts { get; set; }
        public List<OrderDTO> Orders { get; set; }
    }
}