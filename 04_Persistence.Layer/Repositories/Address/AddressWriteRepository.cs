using _02_Application.Layer.Repositories;
using _04_Persistence.Layer.Context;

namespace _04_Persistence.Layer.Repositories.Address
{
    public class AddressWriteRepository : WriteRepository<_01_Domain.Layer.Entities.Address>, IAddressWriteRepository
    {
        public AddressWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
