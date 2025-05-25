using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Domain.Layer.Base;

namespace _01_Domain.Layer.Entities
{
    public class Address :BaseEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int DistrictId { get; set; }
        public District District { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }

    }
}
