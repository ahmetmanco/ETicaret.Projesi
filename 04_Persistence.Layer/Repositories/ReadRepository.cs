using System.Linq.Expressions;
using _01_Domain.Layer.Base;
using _02_Application.Layer.Repositories;
using _04_Persistence.Layer.Context;
using Microsoft.EntityFrameworkCore;

namespace _04_Persistence.Layer.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : class, IBaseEntity
    {
        private readonly AppDbContext _context;

        public ReadRepository(AppDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking) query.AsNoTracking();

            return query;
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await Table.FindAsync(id) ?? throw new Exception("Entity not found");
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking) query.AsNoTracking();

            return await Table.SingleAsync(predicate);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate, bool tracking = true)
        {
            var query = Table.Where(predicate);
            if(!tracking) query = query.AsNoTracking();

            return query;
        }
    }
}
