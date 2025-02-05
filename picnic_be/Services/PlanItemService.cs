using picnic_be.Models;
using picnic_be.Repos;

namespace picnic_be.Services
{
    public interface IPlanItemService<T> where T : PlanItem
    {
        public Task<IEnumerable<T>> GetPlanItemsAsync(int planId);
        public Task<T?> GetPlanItemAsync(int id);
        public Task CreatePlanItemAsync(T planItem);
        public Task<T> UpdatePlanItemAsync(T planItem);
        public Task DeletePlanItemAsync(int id);
    }
    public class PlanItemService<T> : IPlanItemService<T> where T : PlanItem
    {
        private readonly IPlanItemRepo<T> _repo;

        public PlanItemService(IPlanItemRepo<T> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<T>> GetPlanItemsAsync(int planId)
        {
            return await _repo.GetPlanItemsAsync(planId);
        }

        public async Task<T?> GetPlanItemAsync(int id)
        {
            return await _repo.FindPlanItemAsync(id);
        }

        public async Task<T> FindPlanItemAsync(int id)
        {
            return await _repo.FindPlanItemAsync(id)
                ?? throw new InvalidOperationException($"No planItem found with id {id}.");
        }

        public async Task CreatePlanItemAsync(T planItem)
        {
            await _repo.CreatePlanItemAsync(planItem);
        }

        public async Task<T> UpdatePlanItemAsync(T planItem)
        {
            var dbPlanItem = await FindPlanItemAsync(planItem.Id);
            dbPlanItem.Name = planItem.Name;
            dbPlanItem.Note = planItem.Note;
            dbPlanItem.Prepared = planItem.Prepared;
            dbPlanItem.UpdatedAt = DateTime.UtcNow;
            await _repo.SaveChangesAsync();
            return dbPlanItem;
        }
        public async Task DeletePlanItemAsync(int id)
        {
            var planItem = await FindPlanItemAsync(id);
            await _repo.DeletePlanItemAsync(planItem);
        }
    }
}
