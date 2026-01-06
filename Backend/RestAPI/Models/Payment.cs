namespace RestAPI.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public int FkPaymentMethods { get; set; }

        public int FkCurrencies { get; set; }

        public int FkPaymentStatuses { get; set; }

        public decimal Price { get; set; }

        public DateTime PaymentDate { get; set; }

        public string? PaymentNumber { get; set; }
    }
}