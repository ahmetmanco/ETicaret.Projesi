using _01_Domain.Layer.Base;
using Microsoft.EntityFrameworkCore;

namespace _02_Application.Layer.Repositories
{
    public interface IRepository <T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
    }
}
