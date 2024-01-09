using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Models
{
    public class OrderBooking
    {
        public int OrderId { get; set; }
        public int BookingId { get; set; }

        public virtual Order Order { get; set; }
        public virtual Booking Booking { get; set; }

    }

}
