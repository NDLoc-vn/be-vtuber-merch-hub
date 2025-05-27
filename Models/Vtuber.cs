namespace VtuberMerchHub.Models
{
    public class Vtuber
    {
        public int VtuberId { get; set; }
        public int UserId { get; set; }
        public string VtuberName { get; set; }
        public string? RealName { get; set; }
        public DateTime? DebutDate { get; set; }
        public string Channel { get; set; }
        public string? Description { get; set; }
        public int? VtuberGender { get; set; }
        public int? SpeciesId { get; set; }
        public int? CompanyId { get; set; }
        public string ModelUrl { get; set; }

        public User User { get; set; }
        public Gender Gender { get; set; }
        public Species Species { get; set; }
        public Company Company { get; set; }
        public List<Event> Events { get; set; }
        public List<Merchandise> Merchandises { get; set; }
    }
}