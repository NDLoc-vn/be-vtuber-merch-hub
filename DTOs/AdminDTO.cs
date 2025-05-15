namespace VtuberMerchHub.DTOs
{
    public class AdminDTO
    {
        public int AdminId { get; set; }
        public int UserId { get; set; }
        public string AdminName { get; set; }
        public UserDTO User { get; set; }
    }
}