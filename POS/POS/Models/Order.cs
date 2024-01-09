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

        public OrderStatus status { get; set; }

        public float price { get; set; }

        [ForeignKey("Staff")]
        public int staffID { get; set; }

        public virtual Staff staff { get; set; }

        [ForeignKey("Customer")]
        public int customerID { get; set; }

        public virtual Customer customer { get; set; }

    }
}
