using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Models
{
    public enum OrderStatus
    {
        Pending,
        Completed,
        Cancelled
    }
    public class Order
    {
        public int Id { get; set; }

        public OrderStatus Status { get; set; }

        public float Price { get; set; }

        [ForeignKey("Staff")]
        public int StaffID { get; set; }

        [ForeignKey("Customer")]
        public int CustomerID { get; set; }

    }
}
