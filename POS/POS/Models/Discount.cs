namespace POS.Models
{
    public class Discount
    {
        public int Id { get; set; }

        public int Percentage { get; set; }

        public string Description { get; set; }

        public DateOnly ValidFrom { get; set; }

        public DateOnly ValidUntil { get; set; }
    }
}
