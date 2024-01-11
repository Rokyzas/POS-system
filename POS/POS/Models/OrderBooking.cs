using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Models
{
    public class OrderBooking
    {
        public int OrderId { get; set; }

        [ForeignKey("Service")]
        public int BookingId { get; set; }

    }

}
