using _01_Domain.Layer.Entities;

namespace _02_Application.Layer.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQueryResponse
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string? ProductImage { get; set; }
        //public ICollection<Order>? Orders { get; set; }
        //public ProductImageFile ProductImageFile { get; set; }
        //public int CustomerId { get; set; }
        //public Customer? Customer { get; set; }
        //public string AppUserId { get; set; }
        //public AppUser AppUser { get; set; }
    }
}
