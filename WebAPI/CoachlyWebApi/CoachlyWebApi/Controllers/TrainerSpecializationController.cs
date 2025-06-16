using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CoachlyBackEnd.Models;
using CoachlyBackEnd.Models.DTOs.TrainerSpecialization;
using CoachlyBackEnd.Services.CRUD.Interfaces;

namespace CoachlyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerSpecializationController : ControllerBase
    {
        private readonly ICrudService<TrainerSpecialization> _trainerSpecializationService;
        private readonly IMapper _mapper;

        public TrainerSpecializationController(ICrudService<TrainerSpecialization> trainerSpecializationService,
            IMapper mapper)
        {
            _trainerSpecializationService = trainerSpecializationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainerSpecializationDto>>> GetAllTrainerSpecializations()
        {
            try
            {
                var trainerSpecializations = await _trainerSpecializationService.GetAllEntitiesAsync();

                return Ok(_mapper.Map<IEnumerable<TrainerSpecializationDto>>(trainerSpecializations));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrainerSpecializationDto>> GetTrainerSpecialization(int id)
        {
            try
            {
                var trainerSpecialization = await _trainerSpecializationService.GetEntityByIdAsync(id);

                if (trainerSpecialization == null)
                {
                    return NoContent();
                }

                return Ok(_mapper.Map<TrainerSpecializationDto>(trainerSpecialization));
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error retrieving trainer specialization: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainerSpecialization(int id, TrainerSpecializationDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (id != dto.Id)
                {
                    return BadRequest("Ids must match");
                }

                return await _trainerSpecializationService.UpdateEntityAsync(id, dto)
                    ? NoContent()
                    : NotFound($"Trainer specialization with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating Trainer specialization: {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchTrainerSpecialization(int id, TrainerSpecializationUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return await _trainerSpecializationService.PatchEntityAsync(id, dto)
                    ? NoContent()
                    : NotFound($"Trainer specialization with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error patching trainer specialization: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TrainerSpecializationDto>> PostTrainerSpecialization(
            TrainerSpecializationCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var trainerSpecialization = _mapper.Map<TrainerSpecialization>(dto);
                return await _trainerSpecializationService.CreateEntityAsync(trainerSpecialization)
                    ? CreatedAtAction(nameof(GetTrainerSpecialization), new { id = trainerSpecialization.Id },
                        _mapper.Map<TrainerSpecializationDto>(trainerSpecialization))
                    : BadRequest("Failed to create trainer specialization.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating trainer specialization: {ex.Message}");
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainerSpecialization(int id)
        {
            try
            {
                return await _trainerSpecializationService.DeleteEntityAsync(id)
                    ? NoContent()
                    : NotFound($"Trainer specialization with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting trainer specialization: {ex.Message}");
            }
        }
    }
}