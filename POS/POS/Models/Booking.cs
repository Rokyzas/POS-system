using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Models
{
    public enum BookingStatus
    {
        Pending,
        InProgress,
        Completed,
        Cancelled
    }
    public class Booking
    {
        public int id { get; set; } // Corresponds to SERIAL PRIMARY KEY

        [ForeignKey("TimeSlot")]
        public int TimeSlotID { get; set; } // INT NOT NULL

        [ForeignKey("Service")]
        public int ServiceID { get; set; } // INT NOT NULL
        public BookingStatus Status { get; set; }

    }
}
