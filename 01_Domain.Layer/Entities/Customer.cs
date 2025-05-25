using _01_Domain.Layer.Base;

namespace _01_Domain.Layer.Entities
{
    public class Customer :BaseEntity 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public int ProductId { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
