namespace VtuberMerchHub.Models
{
    public class Merchandise
    {
        public int MerchandiseId { get; set; }
        public int VtuberId { get; set; }
        public string MerchandiseName { get; set; }
        public string? Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Vtuber Vtuber { get; set; }
        public List<Product> Products { get; set; }
    }
}