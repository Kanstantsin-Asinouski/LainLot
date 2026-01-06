namespace RestAPI.Models
{
    public class Color
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public byte[] ImageData { get; set; } = null!;
    }
}