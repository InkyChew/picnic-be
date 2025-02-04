using picnic_be.Models;
using picnic_be.Repos;

namespace picnic_be.Services
{
    public interface IUserService
    {
        public Task<User?> GetUserAsync(int id);
        public Task CreateUserAsync(User user);
        public Task<User> UpdateUserAsync(User user);
        public Task<(byte[], string)> GetPictureAsync(string fileName);
        public Task<User> UpdatePictureAsync(int userId, IFormFile? file);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepo _repo;
        private readonly IFileService _imgService;

        public UserService(IUserRepo repo, IFileServiceFactory fileServiceFactory)
        {
            _repo = repo;
            _imgService = fileServiceFactory.CreateImagesFileService("User");
        }

        public async Task<User?> GetUserAsync(int id)
        {
            return await _repo.FindUserAsync(id);
        }

        public async Task<User> FindUserAsync(int id)
        {
            return await _repo.FindUserAsync(id)
                ?? throw new InvalidOperationException($"No user found with id {id}.");
        }

        public async Task CreateUserAsync(User user)
        {
            await _repo.CreateUserAsync(user);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var dbUser = await FindUserAsync(user.Id);
            dbUser.Name = user.Name;
            await _repo.SaveChangesAsync();
            return dbUser;
        }
        public async Task<(byte[], string)> GetPictureAsync(string fileName)
        {
            var bytes = await _imgService.GetFileBytesAsync(fileName);
            var type = _imgService.GetFileContentType(fileName);
            return (bytes, type);
        }

        public async Task<User> UpdatePictureAsync(int userId, IFormFile? file)
        {
            var dbUser = await FindUserAsync(userId);

            // delete old picture
            if (!string.IsNullOrEmpty(dbUser.Picture))
            {
                _imgService.DeleteFile(dbUser.Picture);
            }

            // upload new picture
            string? fileName = null;
            if(file != null && file.Length > 0)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                fileName = $"{DateTime.UtcNow:yyMMddHHmmss}_{dbUser.Id}{fileExtension}";
                await _imgService.CreateFileAsync(file, fileName);
            }
            dbUser.Picture = fileName;
            await _repo.SaveChangesAsync();
            return dbUser;
        }
    }
}
