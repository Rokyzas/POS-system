using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Models
{
    public enum TimeSlotStatus
    {
        Available,
        Unavailable
    }

    public class TimeSlot
    {
        public int id { get; set; } // Corresponds to SERIAL PRIMARY KEY

        public DateTime StartDate { get; set; } // TIMESTAMP NOT NULL

        public DateTime EndDate { get; set; } // TIMESTAMP NOT NULL

        [ForeignKey("Staff")]
        public int StaffID { get; set; } // INT NOT NULL
        public TimeSlotStatus Status { get; set; }
    }
}

