﻿using picnic_be.Dtos;
using picnic_be.Models;
using picnic_be.Repos;

namespace picnic_be.Services
{
    public interface IPlanService
    {
        public Task<IEnumerable<Plan>> GetPlansAsync(PlanSearchParam searchParam);
        public Task<Plan?> GetPlanAsync(int id);
        public Task CreatePlanAsync(Plan plan);
        public Task<Plan> UpdatePlanAsync(Plan plan);
        public Task DeletePlanAsync(int id);
    }

    public class PlanService : IPlanService
    {
        private readonly IPlanRepo _repo;

        public PlanService(IPlanRepo repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Plan>> GetPlansAsync(PlanSearchParam searchParam)
        {
            return await _repo.GetPlansAsync(searchParam);
        }

        public async Task<Plan?> GetPlanAsync(int id)
        {
            return await _repo.GetPlanAsync(id);
        }

        public async Task CreatePlanAsync(Plan plan)
        {
            await _repo.CreatePlanAsync(plan);
        }

        public async Task<Plan> UpdatePlanAsync(Plan plan)
        {
            var dbPlan = await _repo.FindPlanAsync(plan.Id)
                ?? throw new InvalidOperationException($"No entity found with id {plan.Id}.");

            dbPlan.Name = plan.Name;
            dbPlan.Description = plan.Description;
            dbPlan.PlaceId = plan.PlaceId;
            dbPlan.StartTime = plan.StartTime;
            dbPlan.EndTime = plan.EndTime;
            dbPlan.UpdatedAt = DateTime.UtcNow;

            await _repo.SaveChangesAsync();
            return dbPlan;
        }

        public async Task DeletePlanAsync(int id)
        {
            var plan = await _repo.FindPlanAsync(id) ?? throw new InvalidOperationException();
            await _repo.DeletePlanAsync(plan);
        }
    }
}