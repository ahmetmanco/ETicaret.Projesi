using System.Net.Http.Headers;
using _01_Domain.Layer.Base;
using _01_Domain.Layer.Enums;

namespace _01_Domain.Layer.Entities
{
    public class Order : BaseEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public ICollection<Product> Products { get; set; }
        public Customer? Customer { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int? StatusId { get; set; }
        public Status? Status { get; set; }
    }
}
