using _01_Domain.Layer.Entities;
using _02_Application.Layer.Repositories.Costumer;
using _04_Persistence.Layer.Context;

namespace _04_Persistence.Layer.Repositories
{
    public class CustomerReadRepository : ReadRepository<Customer> , ICustomerReadRepository
    {
        public CustomerReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
