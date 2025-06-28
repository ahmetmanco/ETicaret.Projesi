using _02_Application.Layer.Repositories;
using MediatR;

namespace _02_Application.Layer.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        private readonly IProductWriteRepository _wRepository;
        private readonly IProductReadRepository _rRepository;

        public UpdateProductCommandHandler(IProductWriteRepository wRepository, IProductReadRepository rRepository)
        {
            _wRepository = wRepository;
            _rRepository = rRepository;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            _01_Domain.Layer.Entities.Product product = await _rRepository.GetByIdAsync(request.Id);
            product.Name = request.Name;
            product.Stock = request.Stock;
            product.Price = request.Price;
            await _wRepository.SaveAsync();
            return new();
        }
    }
}
