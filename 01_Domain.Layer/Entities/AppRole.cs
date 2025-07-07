using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace _01_Domain.Layer.Entities
{
    public class AppRole : IdentityRole<string>
    {
        public AppRole() : base()
        {
            Id = Guid.NewGuid().ToString(); 
            ConcurrencyStamp = Guid.NewGuid().ToString();
        }
        public AppRole(string roleName) : this()
        {
            Name = roleName;
            NormalizedName = roleName.ToUpperInvariant();
        }

    }
}
