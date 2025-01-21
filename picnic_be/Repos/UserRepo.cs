using picnic_be.Data;
using picnic_be.Dtos;
using picnic_be.Models;

namespace picnic_be.Repos
{
    public interface IUserRepo
    {
        public Task<IEnumerable<User>> GetUserAsync(PlanSearchParam searchParam);
        public Task<User?> FindUserAsync(int id);
        public Task CreateUserAsync(User user);
        public Task SaveChangesAsync();
    }

    public class UserRepo : IUserRepo
    {
        private readonly PicnicDbContext _context;

        public UserRepo(PicnicDbContext context)
        {
            _context = context;
        }

        public async Task<User?> FindUserAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public Task<IEnumerable<User>> GetUserAsync(PlanSearchParam searchParam)
        {
            throw new NotImplementedException();
        }

        public async Task CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
