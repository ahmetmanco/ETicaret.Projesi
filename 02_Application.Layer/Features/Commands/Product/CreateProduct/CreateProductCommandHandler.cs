using _02_Application.Layer.Repositories;
using Azure.Storage.Blobs;
using MediatR;
using static System.Net.Mime.MediaTypeNames;

namespace _02_Application.Layer.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IProductWriteRepository _repository;
        private readonly BlobServiceClient _blobServiceClient;

        public CreateProductCommandHandler(IProductWriteRepository repository, BlobServiceClient blobServiceClient)
        {
            _repository = repository;
            _blobServiceClient = blobServiceClient;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            string imageUrl = null;

            if (request.Image != null)
            {
                var containerClient = _blobServiceClient.GetBlobContainerClient("files");
                await containerClient.CreateIfNotExistsAsync();

                var blobClient = containerClient.GetBlobClient(request.Image.FileName);

                using (var stream = request.Image.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, true);
                }

                imageUrl = blobClient.Uri.ToString(); // Tam URL'yi alıyoruz
            }

            await _repository.AddAsync(new()
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock,
                Image = imageUrl
            });

            await _repository.SaveAsync();
            return new();
        }
    }
    
}
