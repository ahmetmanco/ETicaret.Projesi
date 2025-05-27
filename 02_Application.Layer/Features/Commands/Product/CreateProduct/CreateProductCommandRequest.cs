using MediatR;
using Microsoft.AspNetCore.Http;

namespace _02_Application.Layer.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandRequest : IRequest<CreateProductCommandResponse>
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public IFormFile Image { get; set; }

    }
}
