namespace VtuberMerchHub.Models
{
    public class Gender
    {
        public int GenderId { get; set; }
        public string GenderType { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Vtuber> Vtubers { get; set; }
    }
}