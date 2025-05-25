using Microsoft.AspNetCore.Http;

namespace _02_Application.Layer.Abstraction.Storage
{
    public interface IStorage
    {
        #region Table Per Hierarchy
        Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFile file);
        #endregion
        Task DeleteAsync(string pathOrContainerName,string fileName);
        List<string> GetFiles(string pathOrContainerName);
        bool HasFile(string pathOrContainerName, string fileName);
    }
}
