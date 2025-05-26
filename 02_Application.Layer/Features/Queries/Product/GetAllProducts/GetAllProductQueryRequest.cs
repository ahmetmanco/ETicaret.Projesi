using MediatR;

namespace _02_Application.Layer.Features.Queries.Product.GetAllProducts
{
    public class GetAllProductQueryRequest : IRequest<GetAllProductQueryResponse>
    {
        public int Size { get; set; }
        public int Page { get; set; }
    }
}
