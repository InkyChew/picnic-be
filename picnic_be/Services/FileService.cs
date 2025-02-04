using Microsoft.AspNetCore.StaticFiles;

namespace picnic_be.Services
{
    public interface IFileService
    {
        public Task<byte[]> GetFileBytesAsync(string fileName);
        public string GetFileContentType(string fileName);
        public Task CreateFileAsync(IFormFile file, string fileName);
        public void DeleteFile(string path);
    }

    public class FileService : IFileService
    {
        private readonly string _root;

        public FileService(string dirName)
        {
            _root = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dirName);
            if (!Directory.Exists(_root)) Directory.CreateDirectory(_root);
        }

        public string GetFileContentType(string fileName)
        {
            const string DefaultContentType = "application/octet-stream";

            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = DefaultContentType;
            }

            return contentType;
        }

        public string GetFilePath(string fileName)
        {
            var path = Path.Combine(_root, fileName);
            if (!File.Exists(path)) throw new FileNotFoundException();
            return path;
        }

        public async Task<byte[]> GetFileBytesAsync(string fileName)
        {
            var path = GetFilePath(fileName);
            return await File.ReadAllBytesAsync(path);
        }

        public async Task CreateFileAsync(IFormFile file, string fileName)
        {
            var path = Path.Combine(_root, fileName);
            using FileStream fs = new(path, FileMode.Create);
            await file.CopyToAsync(fs);
        }

        public void DeleteFile(string fileName)
        {
            var path = GetFilePath(fileName);
            File.Delete(path);
        }
    }
}
