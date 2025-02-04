using Microsoft.AspNetCore.Mvc;
using picnic_be.Models;
using picnic_be.Services;
using Serilog;

namespace picnic_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _service.GetUserAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpGet("Image/{fileName}")]
        public async Task<IActionResult> GetAsync(string fileName)
        {
            try
            {
                var (bytes, type) = await _service.GetPictureAsync(fileName);
                return File(bytes, type);
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while getting the user's picture");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            try
            {
                await _service.CreateUserAsync(user);
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }
            catch (InvalidOperationException ex)
            {
                Log.Error(ex, "An error occurred while creating the user");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while creating the user");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<User>> UpdateUser(User user)
        {
            try
            {
                await _service.CreateUserAsync(user);
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }
            catch (InvalidOperationException ex)
            {
                Log.Error(ex, "An error occurred while creating the user");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while creating the user");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
