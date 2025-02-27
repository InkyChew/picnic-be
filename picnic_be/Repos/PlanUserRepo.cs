﻿using Microsoft.EntityFrameworkCore;
using picnic_be.Data;
using picnic_be.Models;

namespace picnic_be.Repos
{
    public interface IPlanUserRepo
    {
        public Task<IEnumerable<PlanUser>> GetUserPlansAsync(int userId);
        public Task<PlanUser?> FindPlanUserAsync(int planId, int userId);
        public Task CreatePlanUserAsync(PlanUser e);
        public Task DeletePlanUserAsync(PlanUser e);
        public Task SaveChangesAsync();

    }
    public class PlanUserRepo : IPlanUserRepo
    {
        private readonly PicnicDbContext _db;

        public PlanUserRepo(PicnicDbContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<PlanUser>> GetUserPlansAsync(int userId)
        {
            return await _db.PlanUsers
                .Where(e => e.UserId == userId).Include(e => e.Plan).ToListAsync();
        }

        public async Task<PlanUser?> FindPlanUserAsync(int planId, int userId)
        {
            return await _db.PlanUsers.FindAsync([planId, userId]);
        }

        public async Task CreatePlanUserAsync(PlanUser e)
        {
            e.CreatedAt = DateTime.UtcNow;
            e.UpdatedAt = DateTime.UtcNow;
            await _db.PlanUsers.AddAsync(e);
            await SaveChangesAsync();
        }

        public async Task DeletePlanUserAsync(PlanUser e)
        {
            _db.PlanUsers.Remove(e);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
