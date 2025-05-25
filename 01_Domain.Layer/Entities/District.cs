using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Domain.Layer.Base;

namespace _01_Domain.Layer.Entities
{
    public class District : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}
