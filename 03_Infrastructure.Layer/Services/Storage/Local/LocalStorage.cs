using _02_Application.Layer.Abstraction.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace _03_Infrastructure.Layer.Services.Storage.Local
{
    public class LocalStorage : Storage, ILocalStorage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        //public async Task DeleteAsync(string path, string fileName) => File.Delete($"{path}\\{fileName}");
        public async Task DeleteAsync(string path, string fileName) => File.Delete(Path.Combine(path, fileName));


        public List<string> GetFiles(string path)
        {
            DirectoryInfo directory = new(path);
            return directory.GetFiles().Select(x => x.Name).ToList();
        }

        public bool HasFile(string path, string fileName) => File.Exists($"{path}\\{fileName}");

        private async Task<bool> CopyFileAsync(string path, IFormFile formFile)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 960 * 960, useAsync: false);
                await formFile.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string path, IFormFile formFile)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            List<(string fileName, string path)> datas = new();

            string fileNewName = await FileRenameAsync(path, formFile.Name, HasFile);
            await CopyFileAsync($"{uploadPath}\\{fileNewName}", formFile);
            datas.Add((fileNewName, path));
            return datas;
        }


        //public async Task<string?> UploadAsync( IFormFile file)
        //{
        //    if (file == null) return null;

        //    string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads"); // resource/files
        //    if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

        //    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

        //    string filePath = Path.Combine(uploadPath, uniqueFileName);

        //    using var stream = new FileStream(filePath, FileMode.Create);
        //    await file.CopyToAsync(stream);

        //    return $"https://localhost:7275/uploads/{uniqueFileName}";
        //}
    }
}
