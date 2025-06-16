using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CoachlyBackEnd.Models;
using CoachlyBackEnd.Models.DTOs.Specialization;
using CoachlyBackEnd.Services.CRUD.Interfaces;

namespace CoachlyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        private readonly ICrudService<Specialization> _specializationService;
        private readonly IMapper _mapper;

        public SpecializationController(ICrudService<Specialization> specializationService, IMapper mapper)
        {
            _specializationService = specializationService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecializationDto>>> GetAllSpecializations()
        {
            try
            {
                var specializations = await _specializationService.GetAllEntitiesAsync();

                return Ok(_mapper.Map<IEnumerable<SpecializationDto>>(specializations));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<SpecializationDto>> GetSpecialization(int id)
        {
            try
            {
                var specialization = await _specializationService.GetEntityByIdAsync(id);

                if (specialization == null)
                {
                    return NoContent();
                }

                return Ok(_mapper.Map<SpecializationDto>(specialization));
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error retrieving specialization: {e.Message}");
            }
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecialization(int id, SpecializationDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (id != dto.Id)
                {
                    return BadRequest("Ids must match");
                }

                return await _specializationService.UpdateEntityAsync(id, dto)
                    ? NoContent()
                    : NotFound($"Specialization with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating specialization: {ex.Message}");
            }
        }
        
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchSpecialization(int id, SpecializationUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return await _specializationService.PatchEntityAsync(id, dto)
                    ? NoContent()
                    : NotFound($"Specialization with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error patching specialization: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<SpecializationDto>> PostSpecialization(SpecializationCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var specialization = _mapper.Map<Specialization>(dto);
                return await _specializationService.CreateEntityAsync(specialization)
                    ? CreatedAtAction(nameof(GetSpecialization), new { id = specialization.Id },
                        _mapper.Map<SpecializationDto>(specialization))
                    : BadRequest("Failed to create specialization.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating specialization: {ex.Message}");
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecialization(int id)
        {
            try
            {
                return await _specializationService.DeleteEntityAsync(id)
                    ? NoContent()
                    : NotFound($"Specialization with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting specialization: {ex.Message}");
            }
        }
    }
}
