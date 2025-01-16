using Microsoft.EntityFrameworkCore;
using picnic_be.Data;
using picnic_be.Models;

namespace picnic_be.Repos
{
    public interface IPlanItemRepo<T> where T : PlanItem
    {
        public Task<IEnumerable<T>> GetPlanItemsAsync(int planId);
        public Task<T?> FindPlanItemAsync(int id);
        public Task CreatePlanItemAsync(T planItem);
        public Task DeletePlanItemAsync(T planItem);
        public Task SaveChangesAsync();
    }
    public class PlanItemRepo<T> : IPlanItemRepo<T> where T : PlanItem
    {
        private readonly PicnicDbContext _context;
        private readonly DbSet<T> _db;

        public PlanItemRepo(PicnicDbContext context)
        {
            _context = context;
            _db =  _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetPlanItemsAsync(int planId)
        {
            return await _db.Where(pi => pi.PlanId == planId).ToListAsync();
        }

        public async Task<T?> FindPlanItemAsync(int id)
        {
            return await _db.FindAsync(id);
        }

        public async Task CreatePlanItemAsync(T planItem)
        {
            planItem.CreatedAt = DateTime.UtcNow;
            planItem.UpdatedAt = DateTime.UtcNow;
            await _db.AddAsync(planItem);
            await SaveChangesAsync();
        }

        public async Task DeletePlanItemAsync(T planItem)
        {
            _db.Remove(planItem);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
