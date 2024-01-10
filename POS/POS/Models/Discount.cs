namespace POS.Models
{
    public class Discount
    {
        public int Id { get; set; }

        public int Percentage { get; set; }

        public string Description { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidUntil { get; set; }
    }
}
