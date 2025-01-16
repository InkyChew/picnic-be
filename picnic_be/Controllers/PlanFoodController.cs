using Microsoft.AspNetCore.Mvc;
using picnic_be.Models;
using picnic_be.Services;
using Serilog;

namespace picnic_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanFoodController : ControllerBase
    {
        private readonly IPlanItemService<PlanFood> _service;

        public PlanFoodController(IPlanItemService<PlanFood> service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlanFood(int id)
        {
            var planFood = await _service.GetPlanItemsAsync(id);
            return planFood == null ? NotFound() : Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Plan>> CreatePlanFood(PlanFood planFood)
        {
            try
            {
                await _service.CreatePlanItemAsync(planFood);
                return Ok(planFood);
            }
            catch (InvalidOperationException ex)
            {
                Log.Error(ex, "An error occurred while creating the plan food");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while creating the plan food");
                return CreatedAtAction(nameof(GetPlanFood), new { id = planFood.Id }, planFood);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePlanFood(PlanFood planFood)
        {
            try
            {
                var updatedPlanFood = await _service.UpdatePlanItemAsync(planFood);
                return Ok(updatedPlanFood);
            }
            catch (InvalidOperationException ex)
            {
                Log.Error(ex, "An error occurred while updating the plan food");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while updating the plan food");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlanFood(int id)
        {
            try
            {
                await _service.DeletePlanItemAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                Log.Error(ex, "An error occurred while deleting the plan food");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while deleting the plan food");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
