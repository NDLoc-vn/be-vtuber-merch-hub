namespace VtuberMerchHub.DTOs
{
    public class VtuberDTO
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
        public string? ModelUrl { get; set; }

        public UserDTO User { get; set; }
        public GenderDTO Gender { get; set; }
        public SpeciesDTO Species { get; set; }
        public CompanyDTO Company { get; set; }
        public List<MerchandiseDTO> Merchandises { get; set; }
    }
}