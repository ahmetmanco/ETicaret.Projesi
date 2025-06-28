using _02_Application.Layer.Repositories;
using MediatR;

namespace _02_Application.Layer.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;

        public GetByIdProductQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            _01_Domain.Layer.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id);
            return new()
            {
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                ProductImage = product.Image
            };
        }
    }
}
