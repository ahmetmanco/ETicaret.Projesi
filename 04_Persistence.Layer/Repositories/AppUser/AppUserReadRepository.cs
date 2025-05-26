using _01_Domain.Layer.Entities;
using _02_Application.Layer.Repositories.AppUser;
using _04_Persistence.Layer.Context;

namespace _04_Persistence.Layer.Repositories
{
    public class AppUserReadRepository : ReadRepository<AppUser>, IAppUserReadRepository
    {
        public AppUserReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
