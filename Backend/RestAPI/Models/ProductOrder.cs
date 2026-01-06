namespace RestAPI.Models
{
    public class ProductOrder
    {
        public int Id { get; set; }

        public int? FkProducts { get; set; }

        public int? FkCustomizableProducts { get; set; }
    }
}