using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CoachlyBackEnd.Models;
using CoachlyBackEnd.Models.DTOs.UserDtos;
using CoachlyBackEnd.Services.CRUD.Interfaces;

namespace CoachlyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICrudService<User> _userService;
        private readonly IMapper _mapper;

        public UserController(ICrudService<User> userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllEntitiesAsync();

                return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            try
            {
                var user = await _userService.GetEntityByIdAsync(id);

                if (user == null)
                {
                    return NoContent();
                }

                return Ok(_mapper.Map<UserDto>(user));
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error retrieving user: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (id != dto.Id)
                {
                    return BadRequest("Ids must match");
                }

                return await _userService.UpdateEntityAsync(id, dto)
                    ? NoContent()
                    : NotFound($"User with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating user: {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUser(int id, UserUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return await _userService.PatchEntityAsync(id, dto)
                    ? NoContent()
                    : NotFound($"User with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error patching user: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> PostUser(UserCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = _mapper.Map<User>(dto);
                return await _userService.CreateEntityAsync(user)
                    ? CreatedAtAction(nameof(GetUser), new { id = user.Id }, _mapper.Map<UserDto>(user))
                    : BadRequest("Failed to create user.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating user: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                return await _userService.DeleteEntityAsync(id)
                    ? NoContent()
                    : NotFound($"User with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting user: {ex.Message}");
            }
        }
    }
}