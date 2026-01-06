namespace RestAPI.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int FkProductOrders { get; set; }

        public int FkOrderStatus { get; set; }

        public int FkPayments { get; set; }

        public int FkShippingAddresses { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}