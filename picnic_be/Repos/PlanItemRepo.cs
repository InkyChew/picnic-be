using Microsoft.EntityFrameworkCore;
using picnic_be.Data;
using picnic_be.Models;

namespace picnic_be.Repos
{
    public interface IPlanItemRepo<T> where T : PlanItem
    {
        public Task<IEnumerable<T>> GetPlanItemsAsync(int planId);
        public Task<T?> GetPlanItemAsync(int id);
        public Task<T?> FindPlanItemAsync(int id);
        public Task CreatePlanItemAsync(T planItem);
        public Task DeletePlanItemAsync(T planItem);
        public Task SaveChangesAsync();
    }
    public class PlanItemRepo<T> : IPlanItemRepo<T> where T : PlanItem
    {
        private readonly PicnicDbContext _db;
        private readonly DbSet<T> _entity;

        public PlanItemRepo(PicnicDbContext context)
        {
            _db = context;
            _entity = _db.Set<T>();
        }

        public async Task<IEnumerable<T>> GetPlanItemsAsync(int planId)
        {
            return await _entity.Where(pi => pi.PlanId == planId).ToListAsync();
        }

        public async Task<T?> GetPlanItemAsync(int id)
        {
            return await _entity.Include(t => t.Preparers).SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<T?> FindPlanItemAsync(int id)
        {
            return await _entity.FindAsync(id);
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
            await _db.SaveChangesAsync();
        }
    }
}
