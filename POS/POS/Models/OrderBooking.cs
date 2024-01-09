using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Models
{
    public class OrderBooking
    {
        public int orderId { get; set; }
        public int bookingId { get; set; }

        public virtual Order order { get; set; }
        public virtual Booking booking { get; set; }

    }

}
