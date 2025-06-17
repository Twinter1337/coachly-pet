using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CoachlyBackEnd.Models;
using CoachlyBackEnd.Models.DTOs.Subscribtion;
using CoachlyBackEnd.Services.CRUD.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace CoachlyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribtionController : ControllerBase
    {
        private readonly ICrudService<Subscribtion> _subscriptionService;
        private readonly IMapper _mapper;

        public SubscribtionController(ICrudService<Subscribtion> subscriptionService, IMapper mapper)
        {
            _subscriptionService = subscriptionService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubscribtionDto>>> GetAllSubscribtions()
        {
            try
            {
                var subscribtions = await _subscriptionService.GetAllEntitiesAsync();

                return Ok(_mapper.Map<IEnumerable<SubscribtionDto>>(subscribtions));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubscribtionDto>> GetSubscribtion(int id)
        {
            try
            {
                var subscribtion = await _subscriptionService.GetEntityByIdAsync(id);

                if (subscribtion == null)
                {
                    return NoContent();
                }

                return Ok(_mapper.Map<SubscribtionDto>(subscribtion));
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error retrieving subscribtion: {e.Message}");
            }
        }

        [Authorize(Roles = "Admin, Trainer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubscribtion(int id, SubscribtionDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (id != dto.Id)
                {
                    return BadRequest("Ids must match");
                }

                return await _subscriptionService.UpdateEntityAsync(id, dto)
                    ? NoContent()
                    : NotFound($"Subscribtion with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating subscribtion: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin, Trainer")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchSubscribtion(int id, SubscribtionUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return await _subscriptionService.PatchEntityAsync(id, dto)
                    ? NoContent()
                    : NotFound($"Subscribtion with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error patching subscribtion: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin, Trainer")]
        [HttpPost]
        public async Task<ActionResult<SubscribtionDto>> PostSubscribtion(SubscribtionCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var subscribtion = _mapper.Map<Subscribtion>(dto);
                return await _subscriptionService.CreateEntityAsync(subscribtion)
                    ? CreatedAtAction(nameof(GetSubscribtion), new { id = subscribtion.Id },
                        _mapper.Map<SubscribtionDto>(subscribtion))
                    : BadRequest("Failed to create subscribtion.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating subscribtion: {ex.Message}");
            }
        }
        
        [Authorize(Roles = "Admin, Trainer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubscribtion(int id)
        {
            try
            {
                return await _subscriptionService.DeleteEntityAsync(id)
                    ? NoContent()
                    : NotFound($"Subscribtion with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting subscribtion: {ex.Message}");
            }
        }
    }
}