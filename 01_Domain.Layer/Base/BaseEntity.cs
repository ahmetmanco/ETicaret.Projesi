using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Domain.Layer.Base
{
    public class BaseEntity : IBaseEntity
    {
        public virtual DateTime? UpdatedDate { get; set; }
        public DateTime CreatedDate { get ; set ; }
        public DateTime? DeletedDate { get ; set; }
    }
}
