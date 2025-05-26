using _01_Domain.Layer.Entities;
using _02_Application.Layer.Repositories;
using _04_Persistence.Layer.Context;

namespace _04_Persistence.Layer.Repositories
{
    public class ProductImageReadRepository : ReadRepository<ProductImageFile>, IProductImageFileReadRepository
    {
        public ProductImageReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
