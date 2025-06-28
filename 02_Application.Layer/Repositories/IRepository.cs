using _01_Domain.Layer.Base;
using Microsoft.EntityFrameworkCore;

namespace _02_Application.Layer.Repositories
{
    public interface IRepository <T> where T :class, IBaseEntity
    {
        DbSet<T> Table { get; }
    }
}
