using picnic_be.Models;
using picnic_be.Repos;

namespace picnic_be.Services
{
    public interface IUserService
    {
        public Task<User?> GetUserAsync(int id);
        public Task CreateUserAsync(User user);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepo _repo;

        public UserService(IUserRepo repo)
        {
            _repo = repo;
        }

        public async Task<User?> GetUserAsync(int id)
        {
            return await _repo.FindUserAsync(id);
        }

        public async Task CreateUserAsync(User user)
        {
            await _repo.CreateUserAsync(user);
        }
    }
}
