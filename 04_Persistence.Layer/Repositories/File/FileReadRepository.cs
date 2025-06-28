using _02_Application.Layer.Repositories;
using _04_Persistence.Layer.Context;

namespace _04_Persistence.Layer.Repositories
{
    public class FileReadRepository : ReadRepository<_01_Domain.Layer.Entities.File>, IFileReadRepository
    {
        public FileReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
