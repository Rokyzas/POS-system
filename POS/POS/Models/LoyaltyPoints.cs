using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Models
{
    public class LoyaltyPoints
    {
        public int Id { get; set; }

        [ForeignKey("Discount")]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public int Points { get; set; }
    }
}
