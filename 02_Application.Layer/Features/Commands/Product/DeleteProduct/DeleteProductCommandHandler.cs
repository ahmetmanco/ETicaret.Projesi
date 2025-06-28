
using _02_Application.Layer.Repositories;
using MediatR;

namespace _02_Application.Layer.Features.Commands.Product.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
    {
        private readonly IProductWriteRepository _repository;

        public DeleteProductCommandHandler(IProductWriteRepository repository)
        {
            _repository = repository;
        }

        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id);
            await _repository.SaveAsync();
            return new();
        }
    }
}
