namespace VtuberMerchHub.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public int VtuberId { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }

        public Vtuber Vtuber { get; set; }
    }
}