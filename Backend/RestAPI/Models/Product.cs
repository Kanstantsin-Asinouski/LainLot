namespace RestAPI.Models
{
    public class Product
    {
        public int Id { get; set; }

        public int FkFabricTypes { get; set; }

        public int FkColors { get; set; }

        public int FkSizeOptions { get; set; }

        public int FkCurrencies { get; set; }

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public bool IsActive { get; set; }

        public bool IsCustomizable { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}