using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CoachlyBackEnd.Models;
using CoachlyBackEnd.Models.DTOs.TrainerDtos;
using CoachlyBackEnd.Models.Enums;
using CoachlyBackEnd.Services.CRUD.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace CoachlyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerController : ControllerBase
    {
        private readonly ICrudService<Trainer> _trainerService;
        private readonly IMapper _mapper;

        public TrainerController(ICrudService<Trainer> trainerService, IMapper mapper)
        {
            _trainerService = trainerService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainerDto>>> GetAllTrainers()
        {
            try
            {
                var trainers = await _trainerService.GetAllEntitiesAsync();

                return Ok(_mapper.Map<IEnumerable<TrainerDto>>(trainers));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainerDto>> GetTrainer(int id)
        {
            try
            {
                var trainer = await _trainerService.GetEntityByIdAsync(id);

                if (trainer == null)
                {
                    return NoContent();
                }

                return Ok(_mapper.Map<TrainerDto>(trainer));
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error retrieving trainer: {e.Message}");
            }
        }
        
        [Authorize(Roles = "Admin, Trainer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainer(int id, TrainerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (id != dto.Id)
                {
                    return BadRequest("Ids must match");
                }

                return await _trainerService.UpdateEntityAsync(id, dto)
                    ? NoContent()
                    : NotFound($"Trainer with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating Trainer: {ex.Message}");
            }
        }
        
        [Authorize(Roles = "Admin, Trainer")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchTrainer(int id, TrainerUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return await _trainerService.PatchEntityAsync(id, dto)
                    ? NoContent()
                    : NotFound($"Trainer with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error patching trainer: {ex.Message}");
            }
        }
        
        [Authorize(Roles = "Admin, Client")]
        [HttpPost]
        public async Task<ActionResult<TrainerDto>> PostTrainer(TrainerCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var trainer = _mapper.Map<Trainer>(dto);
                trainer.UserId = dto.UserId;
                _trainerService.Context.Users.FirstOrDefault(e => e.Id == dto.UserId)!.Role = UserRole.Trainer; 

                Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(trainer));
                
                return await _trainerService.CreateEntityAsync(trainer)
                    ? CreatedAtAction(nameof(GetTrainer), new { id = trainer.Id }, _mapper.Map<TrainerDto>(trainer))
                    : BadRequest("Failed to create trainer.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating trainer: {ex.Message}");
            }
        }
        
        [Authorize(Roles = "Admin, Trainer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainer(int id)
        {
            try
            {
                return await _trainerService.DeleteEntityAsync(id)
                    ? NoContent()
                    : NotFound($"Trainer with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting trainer: {ex.Message}");
            }
        }
    }
}
