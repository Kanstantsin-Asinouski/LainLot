namespace RestAPI.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public int FkProductOrders { get; set; }

        public int FkCurrencies { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}