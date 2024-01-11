namespace POS.Models
{
    public class PaymentRequest
    {
        public int OrderId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public float CashTendered { get; set; } // For cash payments
    }

    public enum PaymentMethod
    {
        Card,
        Cash
    }
}
