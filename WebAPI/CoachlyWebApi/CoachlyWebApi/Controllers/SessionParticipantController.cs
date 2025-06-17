using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CoachlyBackEnd.Models;
using CoachlyBackEnd.Models.DTOs.SessionParticipants;
using CoachlyBackEnd.Services.CRUD.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace CoachlyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionParticipantController : ControllerBase
    {
        private readonly ICrudService<SessionParticipant> _sessionParticipantService;
        private readonly IMapper _mapper;

        public SessionParticipantController(ICrudService<SessionParticipant> sessionParticipantService, IMapper mapper)
        {
            _sessionParticipantService = sessionParticipantService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionParticipantDto>>> GetAllSessionParticipants()
        {
            try
            {
                var sessionParticipants = await _sessionParticipantService.GetAllEntitiesAsync();

                return Ok(_mapper.Map<IEnumerable<SessionParticipantDto>>(sessionParticipants));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<SessionParticipantDto>> GetSessionParticipant(int id)
        {
            try
            {
                var sessionParticipant = await _sessionParticipantService.GetEntityByIdAsync(id);

                if (sessionParticipant == null)
                {
                    return NoContent();
                }

                return Ok(_mapper.Map<SessionParticipantDto>(sessionParticipant));
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error retrieving session participants: {e.Message}");
            }
        }

        [Authorize(Roles = "Admin, Trainer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSessionParticipant(int id, SessionParticipantDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (id != dto.Id)
                {
                    return BadRequest("Ids must match");
                }

                return await _sessionParticipantService.UpdateEntityAsync(id, dto)
                    ? NoContent()
                    : NotFound($"Session participant with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating session participant: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin, Trainer")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchSessionParticipant(int id, SessionParticipantUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return await _sessionParticipantService.PatchEntityAsync(id, dto)
                    ? NoContent()
                    : NotFound($"Session participant with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error patching session participant: {ex.Message}");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<SessionParticipantDto>> PostSessionParticipant(
            SessionParticipantCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var sessionParticipant = _mapper.Map<SessionParticipant>(dto);
                return await _sessionParticipantService.CreateEntityAsync(sessionParticipant)
                    ? CreatedAtAction(nameof(GetSessionParticipant), new { id = sessionParticipant.Id },
                        _mapper.Map<SessionParticipantDto>(sessionParticipant))
                    : BadRequest("Failed to create session participant.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating session participant: {ex.Message}");
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSessionParticipant(int id)
        {
            try
            {
                return await _sessionParticipantService.DeleteEntityAsync(id)
                    ? NoContent()
                    : NotFound($"Session participant with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting session participant: {ex.Message}");
            }
        }
    }
}