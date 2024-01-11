using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Models
{
    public class Tip
    {
        public int Id { get; set; }
        public float Amount { get; set; }

        // Foreign key relationship with Transaction
        [ForeignKey("Order")]
        public int OrderId { get; set; }
    }
}
