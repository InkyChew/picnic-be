﻿using picnic_be.Models;
using picnic_be.Repos;

namespace picnic_be.Services
{
    public interface IPlanUserService
    {
        public Task<IEnumerable<PlanUser>> GetUserPlansAsync(int userId);
        public PlanUser CreateHost(int userId);
        public Task<PlanUser> InviteUserAsync(PlanUser e);
        public Task<PlanUser> UpdateStatusAsync(PlanUser e);
        public Task DeletePlanUserAsync(int planId, int userId);
    }
    public class PlanUserService : IPlanUserService
    {
        private readonly IPlanUserRepo _repo;

        public PlanUserService(IPlanUserRepo repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<PlanUser>> GetUserPlansAsync(int userId)
        {
            return await _repo.GetUserPlansAsync(userId);
        }

        public async Task<PlanUser> FindPlanUserAsync(int planId, int userId)
        {
            return await _repo.FindPlanUserAsync(planId, userId)
                ?? throw new InvalidOperationException($"No planUser found with planId, userId ({planId}, {userId}).");
        }

        public PlanUser CreateHost(int userId)
        {
            return new PlanUser
            {
                UserId = userId,
                IsHost = true,
                Status = InvitationStatus.Accept,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        public async Task<PlanUser> InviteUserAsync(PlanUser e)
        {
            await _repo.CreatePlanUserAsync(e);
            // Send invitaion
            return e;
        }

        public async Task<PlanUser> UpdateStatusAsync(PlanUser e)
        {
            var dbPlanUser = await FindPlanUserAsync(e.PlanId, e.UserId);
            dbPlanUser.Status = e.Status;
            dbPlanUser.UpdatedAt = DateTime.UtcNow;
            await _repo.SaveChangesAsync();
            return dbPlanUser;
        }

        public async Task DeletePlanUserAsync(int planId, int userId)
        {
            var planUser = await FindPlanUserAsync(planId, userId);
            await _repo.DeletePlanUserAsync(planUser);
        }
    }
}
