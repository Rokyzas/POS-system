using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Models
{
    public class Transaction
    {   
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
    }
}
