using _01_Domain.Layer.Entities;
using _02_Application.Layer.Repositories;
using _02_Application.Layer.Repositories.Costumer;
using _04_Persistence.Layer.Context;

namespace _04_Persistence.Layer.Repositories
{
    public class CustomerWriteRepository : WriteRepository<Customer>, ICustomerWriteRepository
    {
        public CustomerWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
