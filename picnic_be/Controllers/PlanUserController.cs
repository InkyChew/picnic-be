using Microsoft.AspNetCore.Mvc;
using picnic_be.Models;
using picnic_be.Services;
using Serilog;

namespace picnic_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanUserController : ControllerBase
    {
        private readonly IPlanUserService _service;

        public PlanUserController(IPlanUserService service)
        {
            _service = service;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserPlans(int userId)
        {
            return Ok(await _service.GetUserPlansAsync(userId));
        }

        [HttpPost]
        public async Task<ActionResult<PlanUser>> InviteUserAsync(PlanUser e)
        {
            try
            {
                await _service.InviteUserAsync(e);
                return Ok(e);
            }
            catch (InvalidOperationException ex)
            {
                Log.Error(ex, "An error occurred while creating the plan user");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while creating the plan user");
                return CreatedAtAction(null, e);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateStatusAsync(PlanUser e)
        {
            try
            {
                var updatedPlanUser = await _service.UpdateStatusAsync(e);
                return Ok(updatedPlanUser);
            }
            catch (InvalidOperationException ex)
            {
                Log.Error(ex, "An error occurred while updating the plan user");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while updating the plan user");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePlanUser(int planId, int userId)
        {
            try
            {
                await _service.DeletePlanUserAsync(planId, userId);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                Log.Error(ex, "An error occurred while deleting the plan user");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while deleting the plan user");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
