namespace VtuberMerchHub.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? AvatarUrl { get; set; }

        public Admin Admin { get; set; }
        public Customer Customer { get; set; }
        public Vtuber Vtuber { get; set; }
    }
}