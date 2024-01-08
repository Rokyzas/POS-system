namespace POS.Models
{
    public class TimeSlot
    {
        public int id { get; set; } // Corresponds to SERIAL PRIMARY KEY
        public DateTime StartDate { get; set; } // TIMESTAMP NOT NULL
        public DateTime EndDate { get; set; } // TIMESTAMP NOT NULL
        public int StaffID { get; set; } // INT NOT NULL
        public string Status { get; set; } // VARCHAR(255) with CHECK constraint
    }
}
