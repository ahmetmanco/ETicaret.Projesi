
using _02_Application.Layer.Repositories;
using _04_Persistence.Layer.Context;

namespace _04_Persistence.Layer.Repositories
{
    public class FileWriteRepository : WriteRepository<_01_Domain.Layer.Entities.File>, IFileWriteRepository
    {
        public FileWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
