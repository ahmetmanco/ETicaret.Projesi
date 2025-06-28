using _01_Domain.Layer.Base;

namespace _01_Domain.Layer.Entities
{
    public class Product : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ProductImageFile ProductImageFile { get; set; }
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
