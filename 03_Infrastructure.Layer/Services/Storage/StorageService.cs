using _02_Application.Layer.Abstraction.Storage;
using Microsoft.AspNetCore.Http;

namespace _03_Infrastructure.Layer.Services.Storage
{
    public class StorageService : IStorageService
    {
        private readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }

        public string StorageName { get => _storage.GetType().Name; }
        string IStorageService.StorageName { get ; set ; }

        public async Task DeleteAsync(string pathOrContainerName, string fileName) => await _storage.DeleteAsync(pathOrContainerName, fileName);

        public List<string> GetFiles(string pathOrContainerName) => _storage.GetFiles(pathOrContainerName);

        public bool HasFile(string pathOrContainerName, string fileName) => _storage.HasFile(pathOrContainerName, fileName);

        //public Task<string?> UploadAsync(IFormFile file) => _storage.UploadAsync(file);

        public Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFile files) => _storage.UploadAsync(pathOrContainerName, files);

    }
}
