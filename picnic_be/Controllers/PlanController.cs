using Microsoft.AspNetCore.Mvc;
using picnic_be.Dtos;
using picnic_be.Models;
using picnic_be.Services;
using Serilog;

namespace picnic_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly IPlanService _service;

        public PlanController(IPlanService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlans([FromQuery] PlanSearchParam searchParam)
        {
            var plans = await _service.GetPlansAsync(searchParam);
            return Ok(plans);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlan(int id)
        {
            var plan = await _service.GetPlanAsync(id);
            return plan == null ? NotFound() : Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Plan>> CreatePlan(Plan plan)
        {
            try
            {
                await _service.CreatePlanAsync(plan);
                return Ok(plan);
            }
            catch (InvalidOperationException ex)
            {
                Log.Error(ex, "An error occurred while creating the plan");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while creating the plan");
                return CreatedAtAction(nameof(GetPlan), new { id = plan.Id }, plan);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePlan(Plan plan)
        {
            try
            {
                var updatedPlan = await _service.UpdatePlanAsync(plan);
                return Ok(updatedPlan);
            }
            catch (InvalidOperationException ex)
            {
                Log.Error(ex, "An error occurred while updating the plan");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while updating the plan");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlan(int id)
        {
            try
            {
                await _service.DeletePlanAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                Log.Error(ex, "An error occurred while deleting the plan");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while deleting the plan");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
