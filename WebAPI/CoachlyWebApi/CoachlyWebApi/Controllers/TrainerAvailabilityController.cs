using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CoachlyBackEnd.Models;
using CoachlyBackEnd.Models.DTOs.TrainerAvailability;
using CoachlyBackEnd.Services.CRUD.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace CoachlyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerAvailabilityController : ControllerBase
    {
        private readonly ICrudService<TrainerAvailability> _trainerAvailabilityService;
        private readonly IMapper _mapper;

        public TrainerAvailabilityController(ICrudService<TrainerAvailability> trainerAvailabilityService,
            IMapper mapper)
        {
            _trainerAvailabilityService = trainerAvailabilityService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainerAvailabilityDto>>> GetAllTrainerAvailabilities()
        {
            try
            {
                var trainerAvailabilities = await _trainerAvailabilityService.GetAllEntitiesAsync();

                return Ok(_mapper.Map<IEnumerable<TrainerAvailabilityDto>>(trainerAvailabilities));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrainerAvailabilityDto>> GetTrainerAvailability(int id)
        {
            try
            {
                var trainerAvailability = await _trainerAvailabilityService.GetEntityByIdAsync(id);

                if (trainerAvailability == null)
                {
                    return NoContent();
                }

                return Ok(_mapper.Map<TrainerAvailabilityDto>(trainerAvailability));
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error retrieving trainer availability: {e.Message}");
            }
        }

        [Authorize(Roles = "Admin, Trainer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainerAvailability(int id, TrainerAvailabilityDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (id != dto.Id)
                {
                    return BadRequest("Ids must match");
                }

                return await _trainerAvailabilityService.UpdateEntityAsync(id, dto)
                    ? NoContent()
                    : NotFound($"Trainer availability with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating Trainer availability: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin, Trainer")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchTrainerAvailability(int id, TrainerAvailabilityUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return await _trainerAvailabilityService.PatchEntityAsync(id, dto)
                    ? NoContent()
                    : NotFound($"Trainer availability with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error patching trainer availability: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin, Trainer")]
        [HttpPost]
        public async Task<ActionResult<TrainerAvailability>> PostTrainerAvailability(TrainerAvailabilityCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var trainerAvailability = _mapper.Map<TrainerAvailability>(dto);
                return await _trainerAvailabilityService.CreateEntityAsync(trainerAvailability)
                    ? CreatedAtAction(nameof(GetTrainerAvailability), new { id = trainerAvailability.Id },
                        _mapper.Map<TrainerAvailabilityDto>(trainerAvailability))
                    : BadRequest("Failed to create trainer availability.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating trainer availability: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin, Trainer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainerAvailability(int id)
        {
            try
            {
                return await _trainerAvailabilityService.DeleteEntityAsync(id)
                    ? NoContent()
                    : NotFound($"Trainer availability with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting Trainer availability: {ex.Message}");
            }
        }
    }
}