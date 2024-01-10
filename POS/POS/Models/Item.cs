using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Price { get; set; }

        [ForeignKey("Discount")]
        public int DiscountId { get; set; }

        public float Tax { get; set; }

    }
}
