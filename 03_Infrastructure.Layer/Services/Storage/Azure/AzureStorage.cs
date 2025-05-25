using _02_Application.Layer.Abstraction.Storage.Azure;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace _03_Infrastructure.Layer.Services.Storage
{
    public class AzureStorage : Storage, IAzureStorage
    {
        private readonly BlobServiceClient _blobServiceClient;
        BlobContainerClient _containerClient;
        public AzureStorage(IConfiguration configuration)
        {
            _blobServiceClient = new(configuration["Storage:Azure"]);
        }


        public async Task DeleteAsync(string pathOrContainerName, string fileName)
        {
            _containerClient = _blobServiceClient.GetBlobContainerClient(pathOrContainerName);
            BlobClient blobClient = _containerClient.GetBlobClient(fileName);
            await blobClient.DeleteAsync();
        }

        public List<string> GetFiles(string pathOrContainerName)
        {
            _containerClient = _blobServiceClient.GetBlobContainerClient(pathOrContainerName);
            return _containerClient.GetBlobs().Select(x => x.Name).ToList();
        }

        public bool HasFile(string pathOrContainerName, string fileName)
        {
            _containerClient = _blobServiceClient.GetBlobContainerClient(pathOrContainerName);
            return _containerClient.GetBlobs().Any(x => x.Name == fileName);
        }

        public Task<string?> UploadAsync(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainer, IFormFile file)
        {
            _containerClient = _blobServiceClient.GetBlobContainerClient(pathOrContainer);
            await _containerClient.CreateIfNotExistsAsync();
            await _containerClient.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);

            List<(string fileName, string pathOrContainerName)> datas = new();
            //foreach (IFormFile filex in file)
            //{
            //    if (filex.Length > 0)
            //    {
            //        string fileNewName = await FileRenameAsync(pathOrContainer, filex.FileName, HasFile);
            //        BlobClient blob = _containerClient.GetBlobClient(fileNewName);

            //        await blob.UploadAsync(filex.OpenReadStream(), overwrite: true);
            //        datas.Add((fileNewName, pathOrContainer));
            //    }
            //}
            if (file.Length > 0)
            {
                string fileNewName = await FileRenameAsync(file.FileName, pathOrContainer, HasFile);
                BlobClient blob = _containerClient.GetBlobClient(fileNewName);

                await blob.UploadAsync(file.OpenReadStream(), overwrite: true);
                datas.Add((fileNewName, pathOrContainer));
            }
            return datas;
        }

    }
}
