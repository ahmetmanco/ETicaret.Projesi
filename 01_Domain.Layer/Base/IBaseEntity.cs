namespace _01_Domain.Layer.Base
{
    public interface IBaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
