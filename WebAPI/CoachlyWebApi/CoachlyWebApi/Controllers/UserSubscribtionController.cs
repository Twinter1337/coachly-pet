using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CoachlyBackEnd.Models;
using CoachlyBackEnd.Models.DTOs.UserSubscibtion;
using CoachlyBackEnd.Services.CRUD.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace CoachlyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSubscribtionController : ControllerBase
    {
        private readonly ICrudService<UserSubscribtion> _userSubscribtionService;
        private readonly IMapper _mapper;

        public UserSubscribtionController(ICrudService<UserSubscribtion> _userSubscribtion, IMapper mapper)
        {
            _userSubscribtionService = _userSubscribtion;
            _mapper = mapper;
        }
        
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserSubscribtionDto>>> GetAllUserSubscribtions()
        {
            try
            {
                var userSubscribtions = await _userSubscribtionService.GetAllEntitiesAsync();

                return Ok(_mapper.Map<IEnumerable<UserSubscribtionDto>>(userSubscribtions));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserSubscribtionDto>> GetUserSubscribtion(int id)
        {
            try
            {
                var userSubscribtion = await _userSubscribtionService.GetEntityByIdAsync(id);

                if (userSubscribtion == null)
                {
                    return NoContent();
                }

                return Ok(_mapper.Map<UserSubscribtionDto>(userSubscribtion));
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error retrieving user subscribtion: {e.Message}");
            }
        }

        [Authorize(Roles = "Admin, Trainer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserSubscribtion(int id, UserSubscribtionDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (id != dto.Id)
                {
                    return BadRequest("Ids must match");
                }

                return await _userSubscribtionService.UpdateEntityAsync(id, dto)
                    ? NoContent()
                    : NotFound($"User subscribtion with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating user subscribtion: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin, Trainer")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUserSubscribtion(int id, UserSubscribtionUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return await _userSubscribtionService.PatchEntityAsync(id, dto)
                    ? NoContent()
                    : NotFound($"User subscribtion with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error patching user subscribtion: {ex.Message}");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<UserSubscribtionDto>> PostUserSubscribtion(UserSubscribtionCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var userSubscribtion = _mapper.Map<UserSubscribtion>(dto);
                return await _userSubscribtionService.CreateEntityAsync(userSubscribtion)
                    ? CreatedAtAction(nameof(GetUserSubscribtion), new { id = userSubscribtion.Id },
                        _mapper.Map<UserSubscribtionDto>(userSubscribtion))
                    : BadRequest("Failed to create user subscribtion.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating user subscribtion: {ex.Message}");
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserSubscribtion(int id)
        {
            try
            {
                return await _userSubscribtionService.DeleteEntityAsync(id)
                    ? NoContent()
                    : NotFound($"User subscribtion with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting user subscribtion: {ex.Message}");
            }
        }
    }
}