using Microsoft.EntityFrameworkCore;
using picnic_be.Data;
using picnic_be.Dtos;
using picnic_be.Models;

namespace picnic_be.Repos
{
    public interface IPlanRepo
    {
        public Task<IEnumerable<Plan>> GetPlansAsync(PlanSearchParam searchParam);
        public Task<Plan?> GetPlanAsync(int id);
        public Task<Plan?> FindPlanAsync(int id);
        public Task CreatePlanAsync(Plan plan);
        public Task DeletePlanAsync(Plan plan);
        public Task SaveChangesAsync();
    }

    public class PlanRepo : IPlanRepo
    {
        private readonly PicnicDbContext _context;

        public PlanRepo(PicnicDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Plan>> GetPlansAsync(PlanSearchParam searchParam)
        {
            IQueryable<Plan> query = _context.Plans;

            if (!string.IsNullOrEmpty(searchParam.Name))
            {
                query = query.Where(p => p.Name.Contains(searchParam.Name));
            }

            if (searchParam.PlaceId.HasValue)
            {
                query = query.Where(p => p.PlaceId == searchParam.PlaceId);
            }

            return await query.ToListAsync();
        }

        public async Task<Plan?> GetPlanAsync(int id)
        {
            return await _context.Plans
                .Include(p => p.Foods)
                .Include(p => p.Tools)
                .Include(p => p.Users)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Plan?> FindPlanAsync(int id)
        {
            return await _context.Plans.FindAsync(id);
        }

        public async Task CreatePlanAsync(Plan plan)
        {
            plan.CreatedAt = DateTime.UtcNow;
            plan.UpdatedAt = DateTime.UtcNow;
            await _context.Plans.AddAsync(plan);
            await SaveChangesAsync();
        }

        public async Task DeletePlanAsync(Plan plan)
        {
            _context.Plans.Remove(plan);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
