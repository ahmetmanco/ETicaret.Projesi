using System.ComponentModel.DataAnnotations.Schema;
using _01_Domain.Layer.Base;

namespace _01_Domain.Layer.Entities
{
    public class File : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public override DateTime? UpdatedDate { get => base.UpdatedDate; set=> base.UpdatedDate = value; }
        public string Storage { get; set; }
        public string Path { get; set; }
    }
//    override: Türetilen sınıfta, base (temel) sınıftaki sanal(virtual) özelliği yeniden tanımlar.
//    get => base.UpdatedDate: Özellik okunduğunda, base sınıftaki değeri döner.
//    set => base.UpdatedDate = value: Özellik yazıldığında, base sınıftaki değere atama yapar.
}
