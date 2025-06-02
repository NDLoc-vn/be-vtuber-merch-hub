namespace VtuberMerchHub.DTOs
{
    public class MerchandiseDTO
    {
        public int MerchandiseId { get; set; }
        public int VtuberId { get; set; }
        public string MerchandiseName { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public VtuberDTO? Vtuber { get; set; }
        public List<ProductDTO> Products { get; set; } = new List<ProductDTO>();
    }
}