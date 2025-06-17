using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CoachlyBackEnd.Models;
using CoachlyBackEnd.Models.DTOs.Session;
using CoachlyBackEnd.Services.CRUD.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace CoachlyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ICrudService<Session> _sessionService;
        private readonly IMapper _mapper;

        public SessionController(ICrudService<Session> sessionService, IMapper mapper)
        {
            _sessionService = sessionService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionDto>>> GetAllSessions()
        {
            try
            {
                var sessions = await _sessionService.GetAllEntitiesAsync();

                return Ok(_mapper.Map<IEnumerable<SessionDto>>(sessions));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<SessionDto>> GetSession(int id)
        {
            try
            {
                var session = await _sessionService.GetEntityByIdAsync(id);
                
                return Ok(_mapper.Map<SessionDto>(session));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving session: {ex.Message}");
            }
        }
        
        [Authorize(Roles = "Admin, Trainer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSession(int id, SessionDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (id != dto.Id)
                {
                    return BadRequest("Ids must match");
                }

                return await _sessionService.UpdateEntityAsync(id, dto)
                    ? NoContent()
                    : NotFound($"Session with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating Session: {ex.Message}");
            }
        }
        
        [Authorize(Roles = "Admin, Trainer")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchSession(int id, SessionUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return await _sessionService.PatchEntityAsync(id, dto)
                    ? NoContent()
                    : NotFound($"Session with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error patching session: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin, Trainer")]
        [HttpPost]
        public async Task<ActionResult<SessionDto>> PostSession(SessionCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var session = _mapper.Map<Session>(dto);
                return await _sessionService.CreateEntityAsync(session)
                    ? CreatedAtAction(nameof(GetSession), new { id = session.Id }, _mapper.Map<SessionDto>(session))
                    : BadRequest("Failed to create session.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating session: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin, Trainer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession(int id)
        {
            try
            {
                return await _sessionService.DeleteEntityAsync(id)
                    ? NoContent()
                    : NotFound($"Session with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting session: {ex.Message}");
            }
        }
    }
}