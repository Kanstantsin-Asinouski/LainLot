namespace RestAPI.Models
{
    public class ProductImage
    {
        public int Id { get; set; }

        public int FkProducts { get; set; }

        public byte[] ImageData { get; set; } = null!;
    }
}