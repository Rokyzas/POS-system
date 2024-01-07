namespace POS.Models
{
    public class Service
    {
        public int id { get; set; } 
        public int? DiscountID { get; set; } 
        public decimal Price { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; } 
    }
}
