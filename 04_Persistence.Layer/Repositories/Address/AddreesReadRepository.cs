using _02_Application.Layer.Repositories;
using _04_Persistence.Layer.Context;
using Microsoft.EntityFrameworkCore;

namespace _04_Persistence.Layer.Repositories
{
    public class AddreesReadRepository : ReadRepository<_01_Domain.Layer.Entities.Address>, IAddreesReadRepository
    {
        public AddreesReadRepository(AppDbContext appDbContext): base(appDbContext)
        {
            
        }

        
    }
}
