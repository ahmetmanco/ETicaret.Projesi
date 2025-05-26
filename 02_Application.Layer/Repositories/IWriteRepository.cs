using _01_Domain.Layer.Base;

namespace _02_Application.Layer.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(List<T> entities);
        bool UpdateAsync(T entity);
        bool Delete(T entity);
        bool DeleteRange(List<T> entities);
        Task<bool> DeleteAsync(int id);
        Task<int> SaveAsync();
    }
}
