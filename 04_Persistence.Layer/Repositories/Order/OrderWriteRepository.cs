using _01_Domain.Layer.Entities;
using _02_Application.Layer.Repositories;
using _04_Persistence.Layer.Context;

namespace _04_Persistence.Layer.Repositories
{
    public class OrderWriteRepository : WriteRepository<Order>, IOrderWriteRepository
    {
        public OrderWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
