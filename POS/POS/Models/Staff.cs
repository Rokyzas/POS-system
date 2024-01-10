namespace POS.Models
{
    public class Staff
    {
        public int StaffID { get; set; } // Corresponds to SERIAL PRIMARY KEY

        public string Name { get; set; } // VARCHAR(100) NOT NULL

        public DateTime StartDate { get; set; } // DATE NOT NULL

        public int RoleID { get; set; } // INT NOT NULL

    }
}
