using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _02_Application.Layer.Repositories;
using MediatR;

namespace _02_Application.Layer.Features.Queries.Product.GetAllProducts
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly string _baseImageUrl = $"https://miniticaretstoragepublic.blob.core.windows.net/files/";
        public GetAllProductQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            var totalCount = _productReadRepository.GetAll(false).Count();

            var products = _productReadRepository.GetAll(false).Skip(request.Page * request.Size).Take(request.Size).Select(x => new
            {
                x.Id,
                x.Name,
                x.Stock,
                x.Price,
                Image = string.IsNullOrEmpty(x.Image) ? _baseImageUrl + x.Image : null,
                x.CreatedDate,
                x.UpdatedDate
            }).ToList();

            return new()
            {
              Products = products,
              TotalCount = totalCount
            };
        }
    }
}
