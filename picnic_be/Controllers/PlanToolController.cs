using Microsoft.AspNetCore.Mvc;
using picnic_be.Models;
using picnic_be.Services;
using Serilog;

namespace picnic_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanToolController : ControllerBase
    {
        private readonly IPlanItemService<PlanTool> _service;

        public PlanToolController(IPlanItemService<PlanTool> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlanTools(int planId)
        {
            try
            {
                var planTools = await _service.GetPlanItemsAsync(planId);
                return Ok(planTools);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while getting the plan tools");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlanTool(int id)
        {
            try
            {
                var planTool = await _service.FindPlanItemAsync(id);
                return Ok(planTool);
            }
            catch (InvalidOperationException ex)
            {
                Log.Error(ex, "An error occurred while getting the plan tool");
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Plan>> CreatePlanFood(PlanTool planTool)
        {
            try
            {
                await _service.CreatePlanItemAsync(planTool);
                return Ok(planTool);
            }
            catch (InvalidOperationException ex)
            {
                Log.Error(ex, "An error occurred while creating the plan tool");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while creating the plan tool");
                return CreatedAtAction(nameof(GetPlanTool), new { id = planTool.Id }, planTool);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePlanTool(PlanTool planTool)
        {
            try
            {
                var updatedPlanTool = await _service.UpdatePlanItemAsync(planTool);
                return Ok(updatedPlanTool);
            }
            catch (InvalidOperationException ex)
            {
                Log.Error(ex, "An error occurred while updating the plan tool");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while updating the plan tool");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlanTool(int id)
        {
            try
            {
                await _service.DeletePlanItemAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                Log.Error(ex, "An error occurred while deleting the plan tool");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while deleting the plan tool");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
