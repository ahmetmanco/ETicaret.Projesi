using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using _01_Domain.Layer.Base;

namespace _02_Application.Layer.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T :class, IBaseEntity
    {
        IQueryable<T> GetAll(bool tracking = true);
        IQueryable<T> GetWhere(Expression<Func<T,bool>> predicate, bool tracking = true);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, bool tracking = true);
        Task<T> GetByIdAsync(object id);
    }
}
