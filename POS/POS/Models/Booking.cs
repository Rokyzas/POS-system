namespace POS.Models
{
    public class Booking
    {
        public int Id { get; set; } //corresponds to SERIAL PRIMARY KEY
        public int TimeSlotID { get; set; } // INT NOT NULL
        public int ServiceID { get; set; } // INT NOT NULL
        public string Status { get; set; } // VARCHAR(255) with CHECK constraint

    }
}
