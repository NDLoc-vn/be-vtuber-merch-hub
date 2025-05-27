namespace VtuberMerchHub.Models
{
    public class Species
    {
        public int SpeciesId { get; set; }
        public string SpeciesName { get; set; }
        public string? Description { get; set; }
        public List<Vtuber> Vtubers { get; set; }
    }
}