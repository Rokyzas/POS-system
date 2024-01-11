namespace POS.Models
{
    public class PaymentTransaction
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public float AmountPaid { get; set; }
        public float Change { get; set; }
        public DateTime Timestamp { get; set; }

    }
}
