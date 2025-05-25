namespace _01_Domain.Layer.Entities
{
    public class ProductImageFile : File
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
