namespace VtuberMerchHub.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string? Address { get; set; }
        public string? ContactEmail { get; set; }
        public List<Vtuber> Vtubers { get; set; }
    }
}