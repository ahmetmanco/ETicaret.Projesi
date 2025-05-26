using _01_Domain.Layer.Base;
using Microsoft.AspNetCore.Identity;

namespace _01_Domain.Layer.Entities
{
    public class AppUser :  /*IdentityUser<Guid>,*/ BaseEntity
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool Cinsiyet { get; set; }

        public ICollection<Product> Products { get; set; }
        public ICollection<Order> Orders { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
