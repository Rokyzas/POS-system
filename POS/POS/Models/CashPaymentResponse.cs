namespace POS.Models
{
    public class CashPaymentResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public float Change { get; set; } // Change to be given to the customer
    }
}
