namespace VtuberMerchHub.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        public int UserId { get; set; }
        public string AdminName { get; set; }
        public User User { get; set; }
    }
}